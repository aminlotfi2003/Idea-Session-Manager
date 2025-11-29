# Idea Session Manager

Idea Session Manager is an ASP.NET Core 9 solution for running innovation events with multi-role access. Administrators create events and manage judges, participants submit ideas, and judges evaluate submissions so organizers can rank and publish results.

## Solution structure
- **API (`src/API/ISM.WebApi`)** – REST API with versioned controllers for authentication, events, ideas, and evaluations. The API boots through shared pipeline helpers in `ISM.WebFramework` to add Swagger/OpenAPI, API versioning, authentication, and global error handling.
- **Application & Domain (`src/Core`)** – Application layer (MediatR handlers, DTOs) and domain model (entities, value objects, enums, and identity primitives).
- **Infrastructure (`src/Infrastructure`)** – EF Core persistence, Identity/JWT auth, repositories, and database seeding, including a default admin account.
- **UI (`src/UI/ISM.WebApp`)** – ASP.NET Core web client that calls the API using a configurable base URL and stores JWTs in a secure cookie.

## Features
- **Role-based auth with JWT**: Identity setup issues access/refresh tokens, enforces password complexity, and seeds a default admin user (`admin@system.com` / `Admin@12345`).
- **Event lifecycle management**: Admins can create events, publish them, open/close submissions, assign judges, finalize scoring, publish results, archive events, and pull summary reports.
- **Idea submission & scoring**: Participants submit ideas; admins assign them to judges and calculate final rankings; participants can review their own submissions and results.
- **Judge workflows**: Judges see assigned ideas and submit evaluations via dedicated endpoints.
- **Versioned REST API with Swagger**: API versioning and Swagger UI are added during startup for discoverability.

## Prerequisites
- .NET 9 SDK
- SQL Server instance

## Configuration
1. Update the API `appsettings.Development.json` with your SQL Server connection string (`ConnectionStrings:Default`) and a strong `Jwt:SigningKey`.
2. (Optional) Adjust JWT lifetimes or issuer/audience to match your environment.
3. For the UI, set `ApiSettings:BaseUrl` to the running API address and tune the JWT cookie options if needed.

## Database setup
Apply the Entity Framework migrations to create the database schema:

```bash
dotnet ef database update \
  --project src/Infrastructure/ISM.Infrastructure.Persistence/ISM.Infrastructure.Persistence.csproj \
  --startup-project src/API/ISM.WebApi/ISM.WebApi.csproj
```

The startup pipeline seeds the default admin account during application launch.

## Running locally
1. Restore dependencies:
   ```bash
   dotnet restore
   ```
2. Run the API (listens on the configured Kestrel ports):
   ```bash
   dotnet run --project src/API/ISM.WebApi/ISM.WebApi.csproj
   ```
   Swagger UI will be available at `/swagger` for the active API version.
3. Run the web client (ensure `ApiSettings:BaseUrl` points to the API):
   ```bash
   dotnet run --project src/UI/ISM.WebApp/ISM.WebApp.csproj
   ```

## Default credentials
After seeding, sign in as the admin user:
- **Username/Email:** `admin@system.com`
- **Password:** `Admin@12345`

## API surface (v1)
Key endpoints (all prefixed with `api/v1/`):
- **Auth** (`/auth` route): register participant, login/logout, refresh token, change password, create judge accounts.
- **Events**: manage events and criteria, assign judges, finalize and publish results, retrieve summaries.
- **Ideas**: submit ideas, assign to judges, calculate scores, list submissions/results for admins or participants.
- **Evaluations**: judges fetch assigned ideas and submit evaluations.

## Useful commands
- Format & build solution:
  ```bash
  dotnet build Idea-Session-Manager.sln
  ```
- Run API integration smoke test (HTTP file):
  ```bash
  dotnet tool install --global Microsoft.dotnet-httprepl
  httrepl https://localhost:5001/swagger/v1/swagger.json
  ```

## Security notes
- JWT signing keys should never be left empty in production; store secrets securely (e.g., user secrets or environment variables).
- Update the seeded admin password immediately after first login.
- HTTPS is enabled by default in the middleware pipeline; keep it enforced in production deployments.

## License
This project includes a `SECURITY.md` policy in the repository root. Add licensing details here if required.
