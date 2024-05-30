FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RamenGo.Host/RamenGo.Host.csproj", "RamenGo.Host/"]
RUN dotnet restore "./RamenGo.Host/./RamenGo.Host.csproj"
COPY . .
WORKDIR "/src/RamenGo.Host"
RUN dotnet build "./RamenGo.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RamenGo.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RamenGo.Host.dll"]