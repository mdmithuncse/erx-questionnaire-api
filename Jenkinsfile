#!groovy

node('master') {

	try {
		properties([buildDiscarder(logRotator(numToKeepStr: '2'))])

		stage('Checkout') {
		  checkout scm
		}
		
		docker.image("mcr.microsoft.com/dotnet/core/sdk:5.0").inside("--user root -m 2g -v ${WORKSPACE}:/home/erx-questionnaire -v /var/lib/jenkins/caches:/var/lib/jenkins/caches ") {
			stage('Restore'){
				milestone()
				sh 'dotnet restore src/Erx.Questionnaire.Api' 
			}
			
			stage('Test'){

			}
			
			stage('Release'){
				milestone()
				sh 'rm src/Erx.Questionnaire.Api/appsettings.Local.json'

				// DEV - Keep 'DEV' configuration
				if (env.BRANCH_NAME == 'develop') {
					sh 'rm src/Erx.Questionnaire.Api/appsettings.UAT.json'
					sh 'rm src/Erx.Questionnaire.Api/appsettings.Production.json'
				}

				// UAT - Keep 'UAT' configuration
				if (env.BRANCH_NAME == 'release/uat') {
					sh 'rm src/Erx.Questionnaire.Api/appsettings.Development.json'
					sh 'rm src/Erx.Questionnaire.Api/appsettings.Production.json'
				}

				// PROD - Keep 'Production' configuration
				if (env.BRANCH_NAME == 'master') {
					sh 'rm src/Erx.Questionnaire.Api/appsettings.Development.json'
					sh 'rm src/Erx.Questionnaire.Api/appsettings.UAT.json'
				}

				sh 'dotnet publish src/Erx.Questionnaire.Api -c Release -f netcoreapp5.0 -o /home/erx-questionnaire/erx-questionnaire-api'
				sh 'chmod -R 777 /home/erx-questionnaire'
				sh 'chmod -R 777 /home/erx-questionnaire/erx-questionnaire-api'
			}
		}

		stage('Deploy'){
			milestone()

			// zip the output directory
			sh 'cp -v aws-resources/aws-windows-deployment-manifest.json erx-questionnaire-api/'
			sh 'cd erx-questionnaire-api; zip -r ../erx-questionnaire-api.zip .'

			// ERX-DEV
			if (env.BRANCH_NAME == 'develop') {
				// upload zip file to S3
				withAWS(credentials:'AWS-ERX-DEV', region: 'ap-southeast-1') {
					s3Upload(bucket: 'erx-questionnaire-configuration-dev-sg01', 
						file: "erx-questionnaire-api.zip", 
						path: "api/erx-questionnaire-api.zip")
				}

				// use docker registry
				docker.withRegistry('https://xyzci.azurecr.io', 'docker-build-server') {
					docker.build("aws-cli-erx-questionnaire:dev-${BUILD_ID}", 
						""" -f ${WORKSPACE}/aws-resources/aws-cli.Dockerfile \
							--build-arg versionLabel=develop-$BUILD_ID \
							--build-arg appName=Erx-Questionnaire-Api-Dev-SG \
							--build-arg envName=Erx-Questionnaire-Api-Dev-SG01 \
							--build-arg S3BucketName=erx-questionnaire-configuration-dev-sg01 \
							--build-arg S3FileKey=api/erx-questionnaire-api.zip \
							--build-arg profile=erx-questionnaire-dev --no-cache . """)
				}

				// remove the image
				sh 'docker image rmi -f aws-cli-erx-questionnaire:dev-$BUILD_ID'
			}	
			
			// ERX-UAT
			if (env.BRANCH_NAME == 'release/uat') {
				// upload zip file to S3
				withAWS(credentials:'AWS-ERX-UAT', region: 'ap-southeast-1') {
					s3Upload(bucket: 'erx-configuration-uat-sg01', 
						file: "erx-questionnaire-api.zip", 
						path: "api/erx-questionnaire-api_${BUILD_ID}.zip")
				}

				// use docker registry
				docker.withRegistry('https://xyzci.azurecr.io', 'docker-build-server') {
					docker.build("aws-cli-erx-questionnaire:uat-${BUILD_ID}", 
						"""-f ${WORKSPACE}/aws-resources/aws-cli.Dockerfile \
							--build-arg versionLabel=erx-questionnaire-uat-new-$BUILD_ID \
							--build-arg appName=Erx-Questionnaire-Api-UAT-SG \
							--build-arg envName=Erx-Questionnaire-Api-UAT-SG01 \
							--build-arg S3BucketName=erx-configuration-uat-sg01 \
							--build-arg S3FileKey=api/erx-questionnaire-api_${BUILD_ID}.zip \
							--build-arg profile=erx-questionnaire-uat \
							--no-cache . """)
				}

				// remove the image
				sh 'docker image rmi -f aws-cli-erx-questionnaire:uat-$BUILD_ID'
			}

			// ERX-PROD
			if (env.BRANCH_NAME == 'master') {
				// upload zip file to S3
				withAWS(credentials:'AWS-ERX-PROD', region: 'ap-southeast-1') {
					s3Upload(bucket: 'erx-questionnaire-configuration-prod-sg01', 
						file: "erx-questionnaire-api.zip", 
						path: "api/erx-questionnaire-api_${BUILD_ID}.zip")
				}

				// use docker registry
				docker.withRegistry('https://xyzci.azurecr.io', 'docker-build-server') {
					docker.build("aws-cli-erx-questionnaire:prod-${BUILD_ID}", 
						""" -f ${WORKSPACE}/aws-resources/aws-cli.Dockerfile \
							--build-arg versionLabel=erx-questionnaire-prod-new-$BUILD_ID \
							--build-arg appName=Erx-Questionnaire-Api-Prod-SG \
							--build-arg envName=Erx-Questionnaire-Api-Prod-SG01 \
							--build-arg S3BucketName=erx-questionnaire-configuration-prod-sg01 \
							--build-arg S3FileKey=api/erx-questionnaire-api_${BUILD_ID}.zip \
							--build-arg profile=erx-questionnaire-prod-sg \
							--no-cache . """)
				}

				// remove the image
				sh 'docker image rmi -f aws-cli-erx-questionnaire:prod-$BUILD_ID'
			}
		}
			
		currentBuild.result = 'SUCCESS'

	} catch(e) {
		throw e

	} finally {
		// send notification
		def currentResult = currentBuild.result == 'SUCCESS' ? 'success' : 'failure'

		// send message to slack
		withCredentials([string(credentialsId: 'slack-xyz-token', variable: 'TOKEN')]) {
			docker.image('mikewright/slack-client:latest')
			    .run('-e SLACK_TOKEN=$TOKEN -e SLACK_CHANNEL=#devops_erx_questionnaire', 
			        "\"${env.JOB_NAME}/${env.BRANCH_NAME} - Build#${env.BUILD_NUMBER} - Status: ${currentResult}\n ${env.BUILD_URL}\"")
		}
	}
}
