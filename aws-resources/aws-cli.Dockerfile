FROM xyzci.azurecr.io/xyzci/aws-cli:latest

ARG versionLabel
ARG appName
ARG envName
ARG S3BucketName
ARG S3FileKey
ARG profile

# copy configuration files
#RUN mkdir /root/.aws

COPY aws-resources/aws_config /root/.aws/config
COPY aws-resources/aws_credentials /root/.aws/credentials

# re-define the working directory
WORKDIR /workspace_aws

# this is a work-around because the build phase has not in the scope the execution of this command
RUN aws --profile $profile elasticbeanstalk create-application-version --application-name $appName --version-label $versionLabel --source-bundle S3Bucket=$S3BucketName,S3Key=$S3FileKey && \
 aws --profile $profile elasticbeanstalk update-environment --application-name $appName --environment-name $envName --version-label $versionLabel

CMD