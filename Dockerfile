FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet build "RamenGo.Api/RamenGo.Api.csproj" -c Release -o /app/build
RUN dotnet publish "RamenGo.Api/RamenGo.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS release

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT [ "dotnet", "RamenGo.Api.dll" ]