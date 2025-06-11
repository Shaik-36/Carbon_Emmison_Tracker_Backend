# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY Carbon_Emmison_Tracker_Backend/*.csproj ./Carbon_Emmison_Tracker_Backend/
RUN dotnet restore

COPY . ./
WORKDIR /app/Carbon_Emmison_Tracker_Backend
RUN dotnet publish -c Release -o out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/Carbon_Emmison_Tracker_Backend/out ./
ENTRYPOINT ["dotnet", "Carbon_Emmison_Tracker_Backend.dll"]
