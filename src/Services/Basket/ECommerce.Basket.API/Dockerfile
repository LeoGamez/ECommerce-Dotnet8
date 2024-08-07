#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Basket/ECommerce.Basket.API/ECommerce.Basket.API.csproj", "src/Services/Basket/ECommerce.Basket.API/"]
COPY ["src/Shared/ECommerce.Shared/ECommerce.Shared.csproj", "src/Shared/ECommerce.Shared/"]
RUN dotnet restore "./src/Services/Basket/ECommerce.Basket.API/ECommerce.Basket.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/ECommerce.Basket.API"
RUN dotnet build "./ECommerce.Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ECommerce.Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Basket.API.dll"]