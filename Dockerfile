# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["GestionBibliotheque/GestionBibliotheque.csproj", "GestionBibliotheque/"]
RUN dotnet restore "GestionBibliotheque/GestionBibliotheque.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/GestionBibliotheque"
RUN dotnet build "GestionBibliotheque.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "GestionBibliotheque.csproj" -c Release -o /app/publish

# Use the runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestionBibliotheque.dll"]
