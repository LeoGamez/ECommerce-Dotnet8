#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WebApps/ECommerce.Web.UI.BlazorSSR/ECommerce.Web.UI.BlazorSSR/ECommerce.Web.UI.BlazorSSR.csproj", "src/WebApps/ECommerce.Web.UI.BlazorSSR/ECommerce.Web.UI.BlazorSSR/"]
COPY ["src/WebApps/ECommerce.Web.UI.BlazorSSR.Shared/ECommerce.Web.UI.BlazorSSR.Shared.csproj", "src/WebApps/ECommerce.Web.UI.BlazorSSR.Shared/"]
COPY ["src/WebApps/ECommerce.Web.UI.BlazorSSR/ECommerce.Web.UI.BlazorSSR.Client/ECommerce.Web.UI.BlazorSSR.Client.csproj", "src/WebApps/ECommerce.Web.UI.BlazorSSR/ECommerce.Web.UI.BlazorSSR.Client/"]
RUN dotnet restore "./src/WebApps/ECommerce.Web.UI.BlazorSSR/ECommerce.Web.UI.BlazorSSR/ECommerce.Web.UI.BlazorSSR.csproj"
COPY . .
WORKDIR "/src/src/WebApps/ECommerce.Web.UI.BlazorSSR/ECommerce.Web.UI.BlazorSSR"
RUN dotnet build "./ECommerce.Web.UI.BlazorSSR.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ECommerce.Web.UI.BlazorSSR.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Web.UI.BlazorSSR.dll"]