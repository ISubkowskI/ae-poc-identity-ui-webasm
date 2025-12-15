# AE Identity UI POC (WebAssembly)

This project is a **Blazor WebAssembly** application designed as a Proof of Concept (POC) for an Identity Management UI. It demonstrates a modular, AOT-compatible architecture using .NET 9, separating the UI host, reusable components, and shared libraries.

## 🚀 Features

- **Blazor WebAssembly**: Client-side single-page application (SPA) built with .NET 9.
- **Modular Architecture**:
  - **Host**: Main entry point and configuration.
  - **Components**: Reusable UI components (e.g., Identity Storage management).
  - **Lib**: Shared services, DTOs, and settings.
- **Identity Service Integration**:
  - HTTP-based client (`IdentityStorageClient`) to communicate with the backend Identity API.
  - Configurable API endpoints via `appsettings.json`.
- **AOT Compabitility**:
  - **Manual Mapping**: Uses optimized, reflection-free extension methods for object mapping to ensure full compatibility with Blazor WASM AOT trimming.
  - **Docker Ready**: Includes production-ready `Dockerfile` using Nginx.

## 📂 Project Structure

The solution consists of three main projects:

### 1. `ae-poc-identity-ui-webasm`
The main Blazor WebAssembly host application.
- **Key Files**:
  - `Program.cs`: Entry point, service registration (`AddAppServices`), and configuration.
  - `wwwroot/appsettings.json`: Application configuration.
  - `App.razor`: Root component.
  - `Dockerfile`: Multi-stage build definition for Docker support.

### 2. `ae-poc-identity-ui-components`
A Razor Class Library (RCL) containing reusable UI components.
- **Key Folders**:
  - `Components/IdentityStorage`: Components related to identity storage management (e.g., `AppClaimsComponent`).

### 3. `ae-poc-identity-ui-lib`
A class library containing shared logic, services, and models.
- **Key Folders**:
  - `Services`: Contains `IdentityStorageClient` for API communication.
  - `Dtos`: Data Transfer Objects for API contracts.
  - `UiData`: View models used by the UI components.
  - `Extensions`: Contains `MapperExtensions.cs` for manual DTO mapping.
  - `Settings`: Configuration classes like `IdentityApiOptions` and `IdentityStorageApiOptions`.

## 🛠️ Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- Any code editor (VS Code, Visual Studio 2022, etc.)

### 🏃 Running with Docker (Recommended)

This verifies the application in a production-like environment (Nginx hosting).

1. **Build and Run**:
   ```bash
   docker-compose up -d --build
   ```

2. **Access the application**:
   Open [http://localhost:8080](http://localhost:8080) in your browser.

3. **Stop the container**:
   ```bash
   docker-compose down
   ```

### 💻 Running Locally (Development)

1. **Navigate to the host project**:
   ```bash
   cd ae-poc-identity-ui-webasm
   ```

2. **Run the application**:
   ```bash
   dotnet run
   ```
   The application will typically start at `http://localhost:5278` (or the port defined in `launchSettings.json`).

## ⚙️ Configuration

The application is configured via `wwwroot/appsettings.json`.

### Example `appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "App": {
    "Title": "Identity JWT WebAssembly Standalone App",
    "Version": "1.0.1",
    "ClientId": "ae-poc-identity-ui-webasm"
  },
  "IdentityStorageApi": {
    "ApiUrl": "http://localhost:5023",
    "ApiBasePath": "/api/v2"
  },
  "IdentityApi": {
    "ApiUrl": "http://localhost:5023",
    "ApiBasePath": "/api/v2"
  }
}
```

### Key Settings
- **IdentityStorageApi**:
  - `ApiUrl`: The base URL of the backend Identity API (e.g., `http://localhost:5023`). A matching container or local service must be running at this address.
  - `ApiBasePath`: The versioned path prefix (e.g., `/api/v2`).

## ⚠️ Important Notes

### API Connection & CORS
Since this is a client-side WebAssembly application running in the **browser**, it calls the API directly from the user's browser, not from within the Docker container network.

1. **Host Port Mapping**: Your Identity API must expose its port (e.g., `5023`) to the host machine.
2. **CORS (Cross-Origin Resource Sharing)**: The Identity API **MUST** allow CORS requests from the UI origin (e.g., `http://localhost:8080` or `http://localhost:5278`). If CORS is not enabled, the browser will block the requests.


