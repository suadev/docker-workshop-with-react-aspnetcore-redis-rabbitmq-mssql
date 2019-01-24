FROM microsoft/dotnet:2.2.102-sdk-stretch AS build-env

WORKDIR /api

COPY aspnet-core-docker-workshop.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2.1-aspnetcore-runtime-stretch-slim

WORKDIR /api

COPY --from=build-env /api/wait-for-it.sh .

COPY --from=build-env /api/out .

# ENTRYPOINT ["dotnet", "aspnet-core-docker-workshop.dll"]