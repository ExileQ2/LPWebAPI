# LPWebAPI

This ASP.NET Core Web API provides endpoints to record QR scan data into a SQL Server database and includes a sample weather forecast endpoint.

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server instance (e.g., SQL Express)
- A database named **TestQR** with a table **DummyQRScan** matching the scan data schema

## Configuration

### Connection String
The SQL Server connection string is defined in **ScanController.cs** (or can be moved to *appsettings.json*):
```csharp
private readonly string connString =
    "Data Source=YOUR_SERVER;Initial Catalog=TestQR;Integrated Security=True;" +
    "Encrypt=False;TrustServerCertificate=True";
```

To centralize configuration, you can add to *appsettings.json*:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=YOUR_SERVER;Initial Catalog=TestQR;Integrated Security=True;Encrypt=False;TrustServerCertificate=True"
}
```

And update **ScanController** to read from configuration:
```csharp
var connString = Configuration.GetConnectionString("DefaultConnection");
```

## API Endpoints

All scan-related endpoints are under the `/api/scan` route. Use JSON over HTTP.

### POST /api/scan
Insert a new scan record into the **DummyQRScan** table.

```http
POST /api/scan HTTP/1.1
Host: your-api-host
Content-Type: application/json

{
  "Jobdetail": "Job-001",
  "PerID": 42,
  "Name": "Operator A",
  "Mno": 7,
  "Partno": "PN-123",
  "JobPhase": 2,
  "SetM": true,
  "Pass": 100,
  "Fail": 5,
  "Rework": 1,
  "CheckQuant": 10,
  "Seriesno": "SN-456",
  "StartTime": "2023-01-01T08:00:00",
  "EndTime": "2023-01-01T08:30:00",
  "TimeCountM": 1800,
  "CycleTimeM": 18,
  "Efficiency": 0.95,
  "Workno": "WN-789",
  "ProductOrder": "PO-1024",
  "Note": "All operations normal",
  "Company": "Acme",
  "Groupname": "Line1",
  "PhaseName": "Assembly",
  "ReworkBit": false,
  "MachineCode": "MC-001"
}
```

**Responses**
- `200 OK`: `{ "message": "Inserted successfully!" }`
- `400 Bad Request`: `{ "error": "<error message>" }`

### GET /weatherforecast
Returns a sample set of weather forecast data (template endpoint).

```http
GET /weatherforecast HTTP/1.1
Host: your-api-host
Accept: application/json
```

**Responses**
- `200 OK`: Array of forecast objects:
```json
[
  {
    "date": "2023-01-01",
    "temperatureC": 23,
    "temperatureF": 73,
    "summary": "Sunny"
  },
  ...
]
```

## Running the API

1. Update the SQL connection string in **ScanController.cs** or *appsettings.json*.
2. Run the application:
   ```bash
   dotnet run --project LPWebAPI.csproj
   ```
3. In **Development** environment, navigate to `https://localhost:<port>/swagger` for interactive API documentation.
4. You can also use the provided *LPWebAPI.http* file for REST Client testing.

---
*Generated on: $(date)*
