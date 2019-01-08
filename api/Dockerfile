FROM microsoft/dotnet:2.2-sdk AS build-env

WORKDIR /api

COPY aspnet-core-docker-workshop.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore

WORKDIR /api

COPY --from=build-env /api/out .

ENTRYPOINT ["dotnet", "aspnet-core-docker-workshop.dll"]