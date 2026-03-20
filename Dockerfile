# Imagem base de runtime usada na etapa final — sem SDK, menor e mais segura
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
USER app

# Etapa de build: restaura dependências (incluindo pacotes privados do GitHub Packages)
# e compila o projeto
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY nuget.config .

COPY src/FCGCatalog.Domain/FCGCatalog.Domain.csproj src/FCGCatalog.Domain/
COPY src/FCGCatalog.Application/FCGCatalog.Application.csproj src/FCGCatalog.Application/
COPY src/FCGCatalog.Infrastructure/FCGCatalog.Infrastructure.csproj src/FCGCatalog.Infrastructure/
COPY src/FCGCatalog.IoC/FCGCatalog.IoC.csproj src/FCGCatalog.IoC/
COPY src/FCGCatalog.API/FCGCatalog.API.csproj src/FCGCatalog.API/

RUN --mount=type=secret,id=nuget_token \
    NUGET_AUTH_TOKEN=$(cat /run/secrets/nuget_token) \
    dotnet restore src/FCGCatalog.API/FCGCatalog.API.csproj

COPY . .

RUN dotnet build src/FCGCatalog.API/FCGCatalog.API.csproj \
    -c $BUILD_CONFIGURATION --no-restore -o /app/build

# Etapa de publicação: gera os artefatos otimizados para produção
FROM build AS publish
RUN dotnet publish src/FCGCatalog.API/FCGCatalog.API.csproj \
    -c $BUILD_CONFIGURATION --no-restore -o /app/publish

# Imagem final: copia apenas os artefatos publicados para a imagem base de runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCGCatalog.API.dll"]
