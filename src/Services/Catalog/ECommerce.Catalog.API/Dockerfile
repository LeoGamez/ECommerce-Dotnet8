#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Step 1. Get Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#Step 2. Build Opeeration
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/ECommerce.Catalog.API/ECommerce.Catalog.API.csproj", "src/Services/Catalog/ECommerce.Catalog.API/"]
COPY ["src/Shared/ECommerce.Shared/ECommerce.Shared.csproj", "src/Shared/ECommerce.Shared/"]
RUN dotnet restore "./src/Services/Catalog/ECommerce.Catalog.API/ECommerce.Catalog.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/ECommerce.Catalog.API"
RUN dotnet build "./ECommerce.Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

#Step 3. Publish app 
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ECommerce.Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#Step 4. Push
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Catalog.API.dll"]