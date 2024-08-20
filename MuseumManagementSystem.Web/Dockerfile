#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MuseumManagementSystem.Web/MuseumManagementSystem.Web.csproj", "MuseumManagementSystem.Web/"]
COPY ["MuseumManagementSystem.Application/MuseumManagementSystem.Application.csproj", "MuseumManagementSystem.Application/"]
COPY ["MuseumManagementSystem.Domain/MuseumManagementSystem.Domain.csproj", "MuseumManagementSystem.Domain/"]
COPY ["MuseumManagementSystem.Identity/MuseumManagementSystem.Identity.csproj", "MuseumManagementSystem.Identity/"]
COPY ["MuseumManagementSystem.Persistence/MuseumManagementSystem.Persistence.csproj", "MuseumManagementSystem.Persistence/"]
RUN dotnet restore "./MuseumManagementSystem.Web/./MuseumManagementSystem.Web.csproj"
COPY . .
WORKDIR "/src/MuseumManagementSystem.Web"
RUN dotnet build "./MuseumManagementSystem.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MuseumManagementSystem.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MuseumManagementSystem.Web.dll"]