# Use the official ASP.NET 6.0 base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ./GestionBibliotheque/GestionBibliotheque/GestionBibliotheque.csproj ./GestionBibliotheque/
RUN dotnet restore ./GestionBibliotheque/GestionBibliotheque.csproj

# Copy everything else and build the project
COPY ./GestionBibliotheque/ ./GestionBibliotheque/
WORKDIR /src/GestionBibliotheque
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestionBibliotheque.dll"]
