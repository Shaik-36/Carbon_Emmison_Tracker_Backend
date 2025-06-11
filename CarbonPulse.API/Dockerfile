# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY CarbonPulse.API/*.csproj ./CarbonPulse.API/
RUN dotnet restore

COPY . ./
WORKDIR /app/CarbonPulse.API
RUN dotnet publish -c Release -o out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/CarbonPulse.API/out ./
ENTRYPOINT ["dotnet", "CarbonPulse.API.dll"]
