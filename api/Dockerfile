FROM microsoft/aspnetcore-build:2.0 AS build-env

WORKDIR /api

COPY aspnet-core-docker-workshop.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore:2.0

WORKDIR /api

COPY --from=build-env /api/out .

ENTRYPOINT ["dotnet", "aspnet-core-docker-workshop.dll"]