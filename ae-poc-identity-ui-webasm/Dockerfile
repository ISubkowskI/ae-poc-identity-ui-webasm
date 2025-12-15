FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ae-poc-identity-ui-webasm/ae-poc-identity-ui-webasm.csproj ae-poc-identity-ui-webasm/
COPY ae-poc-identity-ui-components/ae-poc-identity-ui-components.csproj ae-poc-identity-ui-components/
COPY ae-poc-identity-ui-lib/ae-poc-identity-ui-lib.csproj ae-poc-identity-ui-lib/
RUN dotnet restore ae-poc-identity-ui-webasm/ae-poc-identity-ui-webasm.csproj

# Copy everything else and build
COPY . .
WORKDIR /src/ae-poc-identity-ui-webasm
RUN dotnet publish -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Remove default nginx static assets
RUN rm -rf ./*

# Copy static assets from builder stage
COPY --from=build /app/publish/wwwroot .

# Copy custom configuration
COPY ae-poc-identity-ui-webasm/nginx.conf /etc/nginx/nginx.conf

# Expose ports
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
