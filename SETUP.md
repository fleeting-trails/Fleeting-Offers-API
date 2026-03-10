# How to Run This Project (From Scratch)

This guide covers everything you need to get the project running after a fresh clone — no prior setup assumed.

---

## Prerequisites

Install the following tools before starting:

| Tool           | Version | Download                                         |
| -------------- | ------- | ------------------------------------------------ |
| Docker Desktop | Latest  | https://www.docker.com/products/docker-desktop   |
| .NET SDK       | 9.0     | https://dotnet.microsoft.com/download/dotnet/9.0 |

Verify they are installed:

```bash
docker --version
docker compose version
dotnet --version   # Should say 9.x.x
```

---

## Step 1 — Set Up the `.env` File

Copy the example file and fill in the values:

```bash
cp .env.example .env
```

Open `.env` and fill in **all** the variables:

```env
# PostgreSQL credentials — choose any values you want
POSTGRES_USER=fleetingtrails
POSTGRES_PASSWORD=YourStrongPassword123!
POSTGRES_DB=fleetingtrails

# ASP.NET Core listen URL inside the container — do not change this
ASPNETCORE_URLS=http://+:8080

# JWT secret — use any long random string (min 32 characters)
JwtToken__Secret=your_super_secret_jwt_key_at_least_32_chars

# Developer secret — used for dev-only endpoints
Developer__Secret=your_developer_secret_key

# Mailtrap password — get this from your Mailtrap account dashboard
# Sign up free at https://mailtrap.io
MailSettings__Password=your_mailtrap_smtp_password

# Database connection string — use the same credentials from above
# The host MUST be "postgres" (the Docker container name), not "localhost"
Database__ConnectionString=Host=postgres;Port=5432;Username=fleetingtrails;Password=YourStrongPassword123!;Database=fleetingtrails

# Redis connection string — host must be "redis" (the Docker container name)
Redis__ConnectionString=redis:6379
```

> **Important:** The `Host=postgres` and `redis` hostnames are Docker internal network names. Do NOT change them to `localhost` — the app container uses these names to communicate with the database and cache containers inside Docker's network.
>
> **Note on ports:** Inside Docker, PostgreSQL runs on port `5432` and Redis on `6379`. On your host machine, they are mapped to ports `5433` and `6380` respectively (to avoid conflicts with any locally installed PostgreSQL or Redis). The `.env` values above are for the Docker app container — the migration script and local dev settings use the mapped ports automatically.

---

## Step 2 — Start the Database and Cache Containers

Start only the infrastructure services first (PostgreSQL, Redis, Adminer):

```bash
docker compose up -d postgres redis adminer
```

Wait about 10–15 seconds for PostgreSQL to fully initialize. You can confirm it is ready by checking its logs:

```bash
docker logs postgres
```

Look for the line:

```
database system is ready to accept connections
```

---

## Step 3 — Apply Database Migrations

Migrations must be run from your local machine (not inside the Docker container) because the app container uses the ASP.NET runtime image which does not include the .NET SDK or EF tools.

### Option A: Using the Migration Script (Recommended)

Run the provided script that automatically reads your `.env` file and applies migrations:

```bash
./migrate.sh
```

The script will:

- Read `POSTGRES_USER`, `POSTGRES_PASSWORD`, and `POSTGRES_DB` from your `.env` file
- Install `dotnet-ef` if not already installed
- Apply all pending migrations to your database

You should see output ending with:

```
✅ Migration completed successfully!
```

### Option B: Manual Migration

If you prefer to run migrations manually or need to troubleshoot:

First, restore packages and install the EF Core CLI tool:

```bash
dotnet restore
dotnet tool install --global dotnet-ef
```

If you already have `dotnet-ef` installed, make sure it is up to date:

```bash
dotnet tool update --global dotnet-ef
```

Then apply the migrations. Use `localhost` here (not `postgres`) because you are running this command on your machine. The PostgreSQL port `5432` inside Docker is mapped to port `5433` on your host:

```bash
dotnet ef database update --connection "Host=localhost;Port=5433;Username=YOUR_USER;Password=YOUR_PASSWORD;Database=YOUR_DB"
```

Replace `YOUR_USER`, `YOUR_PASSWORD`, and `YOUR_DB` with the exact values you set in your `.env` file for `POSTGRES_USER`, `POSTGRES_PASSWORD`, and `POSTGRES_DB`.

You should see output ending with:

```
Done.
```

---

## Step 4 — Build and Start the API

Now build the API image and start the app container:

```bash
docker compose up -d app
```

The first run will take a few minutes because Docker needs to pull the .NET SDK and runtime images and build the project. Subsequent starts will be much faster.

To watch the build and startup logs in real time:

```bash
docker compose logs -f app
```

Wait until you see something like:

```
Now listening on: http://[::]:8080
Application started.
```

---

## Step 5 — Access the Application

Once running, the following URLs are available:

| Service    | URL                           | Description            |
| ---------- | ----------------------------- | ---------------------- |
| API        | http://localhost:5001         | Main REST API          |
| Swagger UI | http://localhost:5001/swagger | Interactive API docs   |
| Scalar UI  | http://localhost:5001/scalar  | Alternative API docs   |
| Adminer    | http://localhost:8080         | Database management UI |

### Logging into Adminer (database UI)

1. Open http://localhost:8080
2. Fill in:
   - **System:** PostgreSQL
   - **Server:** postgres
   - **Username:** (your `POSTGRES_USER` value)
   - **Password:** (your `POSTGRES_PASSWORD` value)
   - **Database:** (your `POSTGRES_DB` value)
3. Click **Login**

---

## Stopping the Project

```bash
docker compose down
```

This stops and removes the containers but keeps your database data (stored in the `./data` folder).

To also delete all stored data and start completely fresh:

```bash
docker compose down
rm -rf ./data
```

---

## Restarting After a Stop

After the initial setup, you only need:

```bash
docker compose up -d
```

No need to repeat the migration step unless new migrations have been added to the codebase.

---

## Checking for New Migrations

When you pull new code, check if new migration files were added:

```bash
git log --oneline Migrations/
```

If new migrations exist, rerun the migration script:

```bash
./migrate.sh
```

Or manually run the migration command from Step 3.

---

## Local Development (Without Docker for the App)

If you want live hot reload while developing, run the app locally instead of in Docker.

1. Start only the infrastructure:

   ```bash
   docker compose up -d postgres redis adminer
   ```

2. Override the connection strings for local use. Create or edit `appsettings.Development.json`:

   ```json
   {
     "Database": {
       "ConnectionString": "Host=localhost;Port=5433;Username=fleetingtrails;Password=YourStrongPassword123!;Database=fleetingtrails"
     },
     "ConnectionStrings": {
       "Redis": "localhost:6380"
     }
   }
   ```

3. Load secrets from `.env` into `dotnet user-secrets` using the provided script:

   ```bash
   chmod +x init-secret.sh
   ./init-secret.sh
   ```

   This script reads your `.env` file and registers every value as a dotnet user secret, automatically converting `__` to `:` (e.g. `JwtToken__Secret` becomes `JwtToken:Secret`). Run it once after setting up your `.env`, and again any time you change a value in `.env`.

4. Run with hot reload:

   **On macOS** — use the provided script (starts Redis via Homebrew then runs `dotnet watch`):

   ```bash
   chmod +x run.sh
   ./run.sh
   ```

   > Note: `run.sh` uses `brew services start redis` and is macOS-only. It also expects Redis installed locally via Homebrew, not via Docker. If you started Redis with Docker in step 1, skip this script and just run `dotnet watch` directly.

   **On Linux / Windows (WSL)** — run directly:

   ```bash
   dotnet watch
   ```

The app will be available at http://localhost:5166.

---

## Utility Scripts

The project includes helper scripts for development tasks:

### `init-secret.sh` — Load `.env` into dotnet user-secrets

```bash
./init-secret.sh
```

Reads your `.env` file and loads all values into `dotnet user-secrets`, converting `__` to `:` for nested config keys. Use this for local development instead of manually exporting environment variables.

### `run.sh` — macOS local dev runner

```bash
./run.sh
```

Checks if Redis is running locally (via Homebrew), starts it if needed, then runs `dotnet watch`. **macOS only** — requires Redis installed via `brew install redis`.

### `reset-migration.sh` — Reset all migrations from scratch

```bash
./reset-migration.sh
```

Removes the last EF migration, creates a new `InitialMigration`, and applies it. Use this only when you need to wipe and recreate the migration history during development. **Do not run this if you have data you want to keep.**

---

## Troubleshooting

### `docker compose up` fails with "port already in use"

Another process is using port `5433`, `5001`, `8080`, or `6380`. Stop the conflicting process or change the port mappings in `docker-compose.yaml`.

### Migrations fail with "password authentication failed" or "connection refused"

If you get a **password authentication error**, you may have a local PostgreSQL installation running on port 5432 that is intercepting the connection. Docker maps PostgreSQL to port `5433` to avoid this conflict. Make sure your migration command or script uses port `5433`.

If you get a **connection refused** error, the PostgreSQL container is not ready yet. Wait a few more seconds and retry. You can also check if it is running:

```bash
docker ps
docker logs postgres
```

### `dotnet-ef` command not found

Install it globally:

```bash
dotnet tool install --global dotnet-ef
```

Then close and reopen your terminal so the PATH is updated.

### App container exits immediately

Check the logs for errors:

```bash
docker compose logs app
```

Common causes: missing `.env` values, wrong `Database__ConnectionString` hostname (make sure it says `Host=postgres`, not `Host=localhost`).

### Database tables are missing or empty

You skipped Step 3. Run the migration script again:

```bash
./migrate.sh
```

### `init-secret.sh` or `run.sh` permission denied

Make the scripts executable first:

```bash
chmod +x init-secret.sh run.sh reset-migration.sh
```
