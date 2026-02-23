#!/bin/bash

# Script to apply EF Core migrations using values from .env file
# This script reads POSTGRES_USER, POSTGRES_PASSWORD, and POSTGRES_DB from .env
# and runs the migration against localhost (where Docker exposes the PostgreSQL port)

set -e  # Exit on any error

# Check if .env file exists
if [ ! -f .env ]; then
    echo "❌ Error: .env file not found!"
    echo "Please create a .env file first. You can copy from .env.example:"
    echo "  cp .env.example .env"
    exit 1
fi

# Load environment variables from .env
echo "📖 Reading configuration from .env file..."
export $(grep -v '^#' .env | grep -v '^$' | xargs)

# Validate required variables
if [ -z "$POSTGRES_USER" ] || [ -z "$POSTGRES_PASSWORD" ] || [ -z "$POSTGRES_DB" ]; then
    echo "❌ Error: Missing required environment variables in .env file!"
    echo ""
    echo "Please ensure these variables are set:"
    echo "  - POSTGRES_USER"
    echo "  - POSTGRES_PASSWORD"
    echo "  - POSTGRES_DB"
    exit 1
fi

# Override Docker-internal connection strings with localhost equivalents
# (.env uses Host=postgres which only works inside Docker, not from the host machine)
export Database__ConnectionString="Host=localhost;Port=5433;Username=$POSTGRES_USER;Password=$POSTGRES_PASSWORD;Database=$POSTGRES_DB"
export Redis__ConnectionString="localhost:6380"

# Construct connection string (using localhost since we're running from host machine)
CONNECTION_STRING="Host=localhost;Port=5433;Username=$POSTGRES_USER;Password=$POSTGRES_PASSWORD;Database=$POSTGRES_DB"

echo "🔌 Connecting to PostgreSQL at localhost:5433..."
echo "   Database: $POSTGRES_DB"
echo "   User: $POSTGRES_USER"
echo ""

# Check if dotnet-ef is installed
if ! command -v dotnet-ef &> /dev/null; then
    echo "⚠️  dotnet-ef tool not found. Installing..."
    dotnet tool install --global dotnet-ef
    echo ""
fi

# Run migrations
echo "🚀 Applying database migrations..."
dotnet ef database update --connection "$CONNECTION_STRING"

echo ""
echo "✅ Migration completed successfully!"
