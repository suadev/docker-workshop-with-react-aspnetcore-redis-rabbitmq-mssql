FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env

WORKDIR /api

COPY aspnet-core-docker-workshop.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim

WORKDIR /api

COPY --from=build-env /api/wait-for-it.sh .

COPY --from=build-env /api/out .

# ENTRYPOINT ["dotnet", "aspnet-core-docker-workshop.dll"]