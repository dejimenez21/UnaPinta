# Get base SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy CSPROJ files and restore dependencies
COPY Api/*.csproj ./Api/
COPY UnaPinta.Core/*.csproj ./UnaPinta.Core/
COPY UnaPinta.Data/*.csproj ./UnaPinta.Data/
COPY UnaPinta.Dto/*.csproj ./UnaPinta.Dto/
RUN dotnet restore Api/UnaPinta.Api.csproj

# Copy the projects files and release
COPY . ./
RUN dotnet publish Api/UnaPinta.Api.csproj -c Release -o out --no-restore

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out ./
ENV UnaPintaSecretKey StagingSecretKey
ENTRYPOINT [ "dotnet", "UnaPinta.Api.dll", "--environment=development"]