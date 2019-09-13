FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Core/Core.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish Core/Core.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/Core/out .
ENTRYPOINT ["dotnet", "Core.dll"]