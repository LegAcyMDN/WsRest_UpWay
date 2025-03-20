FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

ARG DB_CONNECTION_URL=""
ENV DB_CONNECTION_URL=$DB_CONNECTION_URL

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish WsRest_UpWay -o out
# Run tests
RUN dotnet test --no-build --verbosity normal

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "WsRest_UpWay.dll"]
