FROM microsoft/aspnetcore-build:2.0-stretch AS publish

ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT "Production"


WORKDIR /src/
COPY ./ .
# WORKDIR Readme.Web

RUN dotnet restore
RUN dotnet publish --output /src/out

FROM microsoft/aspnetcore-build:2.0-stretch

ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT "Production"

WORKDIR /dotnetapp
COPY --from=publish /src/out .

EXPOSE 80/tcp

ENTRYPOINT ["dotnet", "Readme.Web.dll"]

CMD echo $(pwd)
# THIS DOCKER FILE FOR FEND
# Readme.Web.dll
# RUN dotnet run 


#COMPLETE
#cd to this file dir
#docker image build -t coreang:fend .
#docker run -it coreang:fend /bin/bash
#docker run -d -p 8080:80 --name mycoreang coreang:fend