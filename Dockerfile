# ===== BASE =====
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia todos os .csproj para cachear o restore
COPY src/FCGCatalog.API/FCGCatalog.API.csproj                        src/FCGCatalog.API/
COPY src/FCGCatalog.Infrastructure/FCGCatalog.Infrastructure.csproj  src/FCGCatalog.Infrastructure/
COPY src/FCGCatalog.IoC/FCGCatalog.IoC.csproj                        src/FCGCatalog.IoC/
COPY src/FCGCatalog.Domain/FCGCatalog.Domain.csproj                  src/FCGCatalog.Domain/
COPY src/FCGCatalog.Application/FCGCatalog.Application.csproj        src/FCGCatalog.Application/
COPY src/FCG.Contracts/FCG.Contracts.csproj                          src/FCG.Contracts/

# Restore explícito pelo entry point da API (puxa todas as dependências)
RUN dotnet restore src/FCGCatalog.API/FCGCatalog.API.csproj

# Copia o restante do código
COPY src/ ./src/

# ===== PUBLISH =====
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish src/FCGCatalog.API/FCGCatalog.API.csproj \
    -c $BUILD_CONFIGURATION \
    --no-restore \
    -o /app/publish

# ===== FINAL =====
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCGCatalog.API.dll"]