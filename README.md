# Loss-Prevention-Backend

Production-ready **.NET 8 Web API** backend for the **Loss Prevention & Goods Security** platform. Connects to Microsoft SQL Server (`RFID_ReaderDB`) using Dapper and `Microsoft.Data.SqlClient` to serve real-time RFID telemetry, incident notifications, and operational store analytics across 700+ retail outlets.

---

## 🚀 Technology Stack
* **Framework**: ASP.NET Core Web API (.NET 8 LTS)
* **Data Access**: [Dapper](https://github.com/DapperLib/Dapper) micro-ORM (v2.1.89)
* **Database Provider**: `Microsoft.Data.SqlClient` (v7.1.0)
* **API Documentation**: Swagger / OpenAPI (Swashbuckle v6.6.2)
* **Database**: Microsoft SQL Server (`RFID_ReaderDB`)

---

## 📂 Project Structure
```
backend/
├── Controllers/
│   └── TestController.cs           # Health checks & live DB diagnostic endpoints
├── Data/
│   └── DbConnectionFactory.cs      # SqlConnection provider for RFID_ReaderDB
├── Properties/
│   └── launchSettings.json         # Development server profiles (http://localhost:5000)
├── appsettings.json                # Connection strings & CORS origins
├── Program.cs                      # Service registration, DI, CORS & middleware
├── LossPrevention.Api.csproj       # Project dependencies & .NET 8 configuration
└── README.md
```

---

## ⚙️ Configuration

In `appsettings.json`, set your Microsoft SQL Server connection credentials:
```json
{
  "ConnectionStrings": {
    "RFID_ReaderDB": "Server=localhost;Database=RFID_ReaderDB;User Id=sa;Password=mil;TrustServerCertificate=True;MultipleActiveResultSets=True;"
  },
  "CorsOrigins": [
    "http://localhost:5173",
    "http://127.0.0.1:5173",
    "http://localhost:3000"
  ]
}
```

---

## 🛠️ Running the Application

### 1. Build
```bash
dotnet build
```

### 2. Run
```bash
dotnet run --launch-profile http
```
The API starts on: **`http://localhost:5000`**

### 3. Interactive Swagger UI
Open in your browser:
👉 **`http://localhost:5000/swagger`**

---

## 📡 API Endpoints

### Diagnostic / Verification
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/v1/test/health` | Service online heartbeat and server timestamp |
| `GET` | `/api/v1/test/db-connection` | Executes `sp_GetStoreConnectionDetails` and aggregates table metrics |

---

## 🔒 License
Proprietary & Confidential - Loss Prevention & Goods Security Solution
