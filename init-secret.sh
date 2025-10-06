export $(grep -v '^#' .env | xargs) &&
while IFS='=' read -r key value; do
  [ -n "$key" ] && dotnet user-secrets set "$key" "$value"
done < .env
