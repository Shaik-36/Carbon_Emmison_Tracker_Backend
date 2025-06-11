# Use the official .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY CarbonPulse.API/*.csproj ./CarbonPulse.API/
RUN dotnet restore

# Copy everything else and build
COPY . ./
WORKDIR /app/CarbonPulse.API
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/CarbonPulse.API/out ./
ENTRYPOINT ["dotnet", "CarbonPulse.API.dll"]
