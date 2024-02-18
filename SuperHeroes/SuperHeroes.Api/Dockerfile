FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SuperHeroes.Api/SuperHeroes.Api.csproj", "SuperHeroes.Api/"]
RUN dotnet restore "./SuperHeroes.Api/SuperHeroes.Api.csproj"
COPY . .
WORKDIR "/src/SuperHeroes.Api"
RUN dotnet build "./SuperHeroes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SuperHeroes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperHeroes.Api.dll"]