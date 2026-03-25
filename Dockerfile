# ── Stage 1: Build Next.js (static export → out/) ────────────────────────────
FROM node:20-alpine AS ui-builder
WORKDIR /ui
COPY ui/package.json ui/package-lock.json ./
RUN npm ci
COPY ui/ .
RUN npm run build

# ── Stage 2: Build .NET API ───────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS api-builder
WORKDIR /src
COPY api/ .
RUN dotnet restore Edimus.Api/Edimus.Api.csproj
RUN dotnet publish Edimus.Api/Edimus.Api.csproj -c Release --no-restore -o /app/publish

# ── Stage 3: Runtime ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=api-builder /app/publish .
COPY --from=ui-builder /ui/out ./StaticFiles
EXPOSE 8080
ENTRYPOINT ["dotnet", "Edimus.Api.dll"]
