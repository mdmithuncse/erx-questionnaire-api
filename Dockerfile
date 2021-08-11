FROM mcr.microsoft.com/dotnet/core/sdk:5.0

RUN mkdir -p /home/erx/questionnaire \
	&& mkdir /home/erx/questionnaire/api

WORKDIR /home/erx/questionnaire

COPY . .

#
# Compile
#
RUN dotnet restore src/Erx.Questionnaire.Api \ 
	&& dotnet publish src/Erx.Questionnaire.Api -c Release -f netcoreapp5.0 -o /home/erx/questionnaire/api

#
# Test
#
RUN dotnet test test/Erx.Questionnaire.Business.Test

RUN dotnet test test/Erx.Questionnaire.Services.Test

#
# Release
#
RUN chmod -R 777 /home/erx/questionnaire/api

CMD cp -Rp /home/home/questionnaire/api/* /mnt