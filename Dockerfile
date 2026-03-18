# ===== BASE (runtime leve) =====
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# cache de restore
COPY *.sln .
COPY src/FCGCatalog.API/FCGCatalog.API.csproj src/FCGCatalog.API/

RUN dotnet restore src/FCGCatalog.API/FCGCatalog.API.csproj

# copia restante
COPY src/ ./src/

# build
RUN dotnet build src/FCGCatalog.API/FCGCatalog.API.csproj \
    -c $BUILD_CONFIGURATION \
    --no-restore \
    -o /app/build

# ===== PUBLISH =====
FROM build AS publish
RUN dotnet publish src/FCGCatalog.API/FCGCatalog.API.csproj \
    -c $BUILD_CONFIGURATION \
    --no-restore \
    -o /app/publish

# ===== FINAL =====
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "FCGCatalog.API.dll"]