# Edimus

A full-stack restaurant management system for handling table sessions, orders, menus, and inventory in real time.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | Next.js 15, React 19, TypeScript |
| UI library | Mantine v8, SCSS Modules |
| State | Zustand (client), TanStack Query v5 (server) |
| Charts | Recharts |
| Real-time | SignalR (`@microsoft/signalr`) |
| Backend | ASP.NET Core 8, .NET 8 |
| ORM | Entity Framework Core 9 |
| Database | PostgreSQL 14 |
| CQRS | Mediator (source-generated) |
| Auth | JWT Bearer |
| Email | MailKit |
| Object mapping | Mapster |
| Deployment | Docker, Render.com |

---

## Architecture

### Backend — Modular Monolith / Vertical Slice

The API is organized into self-contained feature modules. Each module owns its full slice: HTTP layer → application logic → data access.

```
Modules/
└── Products/
    ├── Products/              Controllers + request input DTOs
    ├── Products.Core/         Features (CQRS handlers, responses, models)
    └── Products.Infrastructure/ EF Core repositories + entity config
```

Modules: `Categories`, `Companies`, `Identity`, `Ingredients`, `Products`, `Sectors`, `Statistics`, `Tables`, `Users`.

Cross-cutting concerns (base entities, EF context, migrations, JWT, email) live in `Common/`.

### Frontend — Feature-Based Component Tree

```
src/
├── app/          Next.js App Router pages
├── components/
│   ├── ui/       Generic reusable components (Button, Input, Select…)
│   └── views/    Feature views (Admin, Menu, Saloon…)
├── hooks/        React Query data hooks + custom hooks
├── stores/       Zustand stores
├── types/        Shared TypeScript types
└── utils/        Helpers (image compression, formatters…)
```

### Production Build

The `Dockerfile` is a three-stage build: Next.js static output + .NET publish → single ASP.NET runtime image. The API serves the frontend's static files directly — one process, one port (8080).

In development, the two processes run separately (API on `8080`, UI on `3000`).

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20+](https://nodejs.org/)
- [Docker](https://www.docker.com/) (for the local database)

---

## Local Development

### 1. Start the database

```bash
cd db
docker compose up -d
```

This starts a PostgreSQL 14 container on port `5432` with:

| Setting | Value |
|---|---|
| Host | `localhost` |
| Port | `5432` |
| Username | `root` |
| Password | `ABCabc123` |

The database (`EdimusDB`) is created automatically by EF Core on the first API startup — Docker only provides the server and the user.

Data is persisted to `~/apps/postgres`.

### 2. Configure the API

`api/Edimus.Api/appsettings.Development.json` ships with a connection string pre-wired for the Docker setup above. If you renamed the database, update the `Database` value there.

Email sending is optional locally — leave `Email:OriginEmail` and `Email:OriginPassword` empty to skip it.

### 3. Run the API

```bash
# From the repo root
dotnet run --project api/Edimus.Api/Edimus.Api.csproj
```

Or from the `ui/` directory using the npm shortcut:

```bash
npm run server
```

The API starts on `http://localhost:8080`. On first run it **auto-applies all migrations and seeds demo data** (company, layout, tables, sectors, and a root admin user).

Swagger is available at `http://localhost:8080/swagger`.

### 4. Run the UI

```bash
cd ui
npm install
npm run dev
```

Open `http://localhost:3000`.

---

## Environment Variables

### API — `appsettings.Development.json`

| Key | Description |
|---|---|
| `ConnectionStrings:ConnectionString` | PostgreSQL connection string |
| `JWT:Secret` | Signing secret for JWT tokens |
| `JWT:Expiration` | Access token lifetime in minutes (default: `30`) |
| `JWT:RefreshExpiration` | Refresh token lifetime in days (default: `7`) |
| `Email:OriginEmail` | SMTP sender address |
| `Email:OriginPassword` | SMTP credentials |
| `Email:FrontendUrl` | Used in password-reset links (default: `http://localhost:3000`) |

### UI — `ui/.env.local`

| Key | Description |
|---|---|
| `NEXT_PUBLIC_API_BASE_URL` | URL of the running API (e.g. `http://localhost:8080`) |
| `NEXT_PUBLIC_DEV_USER` | *(optional)* Auto-fill login email in dev mode |
| `NEXT_PUBLIC_DEV_PASS` | *(optional)* Auto-fill login password in dev mode |

---

## Project Structure

```
Edimus/
├── api/
│   ├── Edimus.Api/                   Entry point — DI registration, middleware, startup
│   ├── Common/
│   │   ├── Shared.Core/              Base entities, domain abstractions, exceptions
│   │   └── Shared.Infrastructure/    EF Core context, migrations, repositories, JWT, email
│   └── Modules/
│       ├── Categories/
│       ├── Companies/
│       ├── Identity/
│       ├── Ingredients/
│       ├── Products/
│       ├── Sectors/
│       ├── Statistics/
│       ├── Tables/
│       └── Users/
├── db/
│   └── docker-compose.yml            Local PostgreSQL container
├── ui/                               Next.js 15 frontend
│   └── src/
│       ├── app/                      App Router pages
│       ├── components/               UI components
│       ├── hooks/                    Data-fetching and custom hooks
│       ├── stores/                   Zustand state stores
│       ├── types/                    TypeScript type definitions
│       └── utils/                    Shared utilities
├── Dockerfile                        Multi-stage production image
└── render.yaml                       Render.com deployment config
```

---

## Deployment

The app is deployed on [Render.com](https://render.com) via the `Dockerfile` and `render.yaml`. The database is hosted on [Supabase](https://supabase.com) (managed PostgreSQL). Secrets (`ConnectionStrings`, `Jwt__Secret`, email credentials) are set directly in the Render dashboard and are not committed to the repo.

To deploy a new version, push to `main` — Render picks up the change and rebuilds automatically.
