#!/bin/bash

# Export all variables from .env (except comments)
export $(grep -v '^#' .env | xargs)

# Read each line from .env
while IFS='=' read -r key value; do
  # Skip empty lines
  [ -z "$key" ] && continue

  # Replace double underscores with colons
  formatted_key=$(echo "$key" | sed 's/__/:/g')

  # Set the secret
  dotnet user-secrets set "$formatted_key" "$value"
done < .env
