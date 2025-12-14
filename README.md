# AE Identity UI POC (WebAssembly)

This project is a **Blazor WebAssembly** application designed as a Proof of Concept (POC) for an Identity Management UI. It demonstrates a modular architecture using .NET 9, separating the UI host, reusable components, and shared libraries.

## 🚀 Features

- **Blazor WebAssembly**: Client-side single-page application (SPA) built with .NET 9.
- **Modular Architecture**:
  - **Host**: Main entry point and configuration.
  - **Components**: Reusable UI components (e.g., Identity Storage).
  - **Lib**: Shared services, DTOs, and settings.
- **Identity Service Integration**:
  - HTTP-based client to communicate with the backend Identity API.
  - Configurable API endpoints via `appsettings.json`.
- **AutoMapper Integration**: Object-to-object mapping for data transformation.

## 📂 Project Structure

The solution consists of three main projects:

### 1. `ae-poc-identity-ui-webasm`
The main Blazor WebAssembly host application.
- **Key Files**:
  - `Program.cs`: Entry point, service registration (`AddAppServices`, `AddAppMapper`), and configuration.
  - `wwwroot/appsettings.json`: Application configuration.
  - `App.razor`: Root component.

### 2. `ae-poc-identity-ui-components`
A Razor Class Library (RCL) containing reusable UI components.
- **Key Folders**:
  - `Components/IdentityStorage`: Components related to identity storage management.

### 3. `ae-poc-identity-ui-lib`
A class library containing shared logic, services, and models.
- **Key Folders**:
  - `Services`: Contains `IdentityClient` for API communication.
  - `Dtos`: Data Transfer Objects.
  - `Settings`: Configuration classes like `IdentityApiOptions`.

## 🛠️ Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Any code editor (VS Code, Visual Studio 2022, etc.)

### Running Locally

1. **Clone the repository**:
   ```bash
   git clone <repository-url>
   cd ae-poc-identity-ui-webasm
   ```

2. **Navigate to the host project**:
   ```bash
   cd ae-poc-identity-ui-webasm
   ```

3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

4. **Run the application**:
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
  "IdentityApi": {
    "ApiUrl": "http://localhost:5000"
  }
}
```

- **IdentityApi:ApiUrl**: The base URL of the backend Identity API. Ensure this matches your running API service.

