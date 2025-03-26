FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

ARG BRAINTREE_ENV=""
ENV BRAINTREE_ENV=$BRAINTREE_ENV

ARG BRAINTREE_MERCHANT_ID=""
ENV BRAINTREE_MERCHANT_ID=$BRAINTREE_MERCHANT_ID

ARG BRAINTREE_PUBLIC_KEY=""
ENV BRAINTREE_PUBLIC_KEY=$BRAINTREE_PUBLIC_KEY

ARG BRAINTREE_PRIVATE_KEY=""
ENV BRAINTREE_PRIVATE_KEY=$BRAINTREE_PRIVATE_KEY

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Run tests
RUN dotnet test --verbosity normal
# Build and publish a release
RUN dotnet publish WsRest_UpWay -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "WsRest_UpWay.dll"]
