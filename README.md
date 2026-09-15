# Smart-X IoT Data Ingestion and Telemetry Gateway

Smart-X is a .NET 10 IoT application for registering devices and sensors, simulating telemetry, detecting anomalies, uploading sensor files, and viewing live telemetry through a Blazor web app.

The solution contains:

- **API** — ASP.NET Core Web API
- **Web App** — Blazor frontend
- **Simulator** — Generates mock telemetry every 7 seconds
- **Shared** — Shared DTOs, enums, and telemetry classes

---

# Setup Instructions

## 1. Requirements

Install the following before running the project:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Visual Studio 2022/2026 with ASP.NET and web development tools
- [SQL Server 2025 Express](https://www.microsoft.com/en-us/download/details.aspx?id=104781)
- [SQL Server Management Studio](https://learn.microsoft.com/en-us/ssms/install/install)

Restore the project packages:

```bash
dotnet restore
```

---

# Database Set-up

## 1. Install Dependencies

If you don't already have SQL Server installed:

Download & Install [SQL Server 2025 Express](https://www.microsoft.com/en-us/download/details.aspx?id=104781)

- Run the installer.
- Choose the **Basic** installation type.
- Leave the default instance name, usually `SQLEXPRESS`.

Download & Install [SQL Server Management Studio](https://learn.microsoft.com/en-us/ssms/install/install)

- Run the installer.
- Follow the prompts to install SSMS.

For a more detailed setup guide, follow:

[SQL Server Installation Video](https://youtu.be/vbJ_p0Zs3Lk?si=DfLektjjN6NhXhUD)

---

## 2. Create the Database

1. Open **SQL Server Management Studio (SSMS)**.
2. Connect to:

```text
localhost\SQLEXPRESS
```

or:

```text
.\SQLEXPRESS
```

3. Select **Windows Authentication**.
4. Click **Connect**.
5. Right-click **Databases**.
6. Select **New Database...**
7. Name the database:

```text
SmartXDb
```

8. Click **OK**.

---

## 3. Configure the Connection String

Open `appsettings.json` in the **API** project.

Use:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=SmartXDb;Integrated Security=true;TrustServerCertificate=True;"
}
```

If your project already uses a different database name or connection-string key, make sure the database name matches your project configuration.

---

## 4. Apply Database Migrations

### Visual Studio

Open the **Package Manager Console** and run:

```powershell
Update-Database
```

If required:

```powershell
Update-Database -Project API -StartupProject API
```

### VS Code / .NET CLI

Run:

```bash
dotnet ef database update --project API --startup-project API
```

Database setup is now complete.

---

# Running the Project

## Visual Studio

The solution is configured to start all required projects together.

1. Open the solution in Visual Studio.
2. Make sure SQL Server is running.
3. Make sure the database migrations have been applied.
4. Click the green **Start** button or press **F5**.

This starts:

- **API**
- **Web App**
- **Simulator**

The simulator connects to:

```text
http://localhost:5000/
```

The simulator automatically:

- Loads active sensors.
- Generates telemetry.
- Sends telemetry to the API.
- Waits 7 seconds.
- Repeats.

New sensors are automatically detected on the next simulator cycle.

---

# Video Demo

Add the project demonstration video here:

**Video Link:**  
`PASTE YOUTUBE VIDEO LINK HERE`

---

# Features Implemented

## Device Management

- Register IoT devices.
- Store unique identifiers / MAC addresses.
- Prevent duplicate identifiers.
- Display registered devices.
- Track device creation and last-seen times.

Supported device types include:

- ESP32
- Smart Plug
- Gateway
- Actuator Controller
- Smart Meter
- Other

---

## Sensor Management

- Register sensors.
- Update sensors.
- Assign sensors to devices.
- Assign sensors to deployment locations.
- Enable or disable sensors.
- Configure numeric thresholds.

Supported sensor data types:

- Float
- Integer
- Boolean

Supported categories:

- Environmental
- Power Consumption
- Actuator
- Other

---

## Telemetry

The application supports:

- Float telemetry
- Integer telemetry
- Boolean telemetry
- Latest sensor value
- Last-seen timestamp
- Sensor status
- Telemetry sequence numbers
- Telemetry history
- Automatic telemetry generation

The telemetry dashboard displays up to the latest **100 readings** for a selected sensor.

---

## Anomaly Detection

Numeric sensor readings are checked against:

- Minimum threshold
- Maximum threshold
- Maximum allowed delta between readings

If a reading is outside the configured limits, the sensor is marked:

```text
Warning
```

Normal sensors are marked:

```text
Online
```

Boolean sensors do not use numeric threshold checks.

---

## Live Dashboard Refresh

The sensor page automatically refreshes selected sensor information and telemetry every:

```text
5 seconds
```

The simulator generates new telemetry every:

```text
7 seconds
```

---

## Sensor File Attachments

Users can upload files to a sensor.

Supported attachment types:

- Configuration File
- Deployment Photo
- Hardware Log
- Other

Supported actions:

- Upload
- View
- Download
- Delete

Maximum file size:

```text
10 MB
```

Uploaded files are stored under:

```text
uploads/sensors/{sensorId}/
```

---

# API Endpoints

Base API address:

```text
http://localhost:5000
```

## Deployment Nodes

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/deploymentnodes` | Get deployment nodes and their display paths |

## Devices

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/devices` | Get all devices |
| GET | `/api/devices/{id}` | Get one device |
| POST | `/api/devices` | Register a device |

## Sensors

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/sensors` | Get all sensors |
| GET | `/api/sensors/{id}` | Get one sensor |
| POST | `/api/sensors` | Register a sensor |
| PUT | `/api/sensors/{id}` | Update a sensor |
| GET | `/api/sensors/{id}/telemetry?limit=100` | Get recent telemetry |

## Sensor Attachments

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/sensors/{sensorId}/attachments` | Get sensor attachments |
| POST | `/api/sensors/{sensorId}/attachments` | Upload a file |
| GET | `/api/sensors/{sensorId}/attachments/{attachmentId}/download` | Download a file |
| DELETE | `/api/sensors/{sensorId}/attachments/{attachmentId}` | Delete a file |

## Telemetry

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/telemetry/float` | Submit float telemetry |
| POST | `/api/telemetry/integer` | Submit integer telemetry |
| POST | `/api/telemetry/boolean` | Submit Boolean telemetry |

---

# Advanced C# Concepts Used

## Generics

Generic telemetry packets are used:

```csharp
TelemetryPacket<T>
```

This allows the application to handle different telemetry data types using one reusable structure.

## Generic Math

Numeric telemetry uses:

```csharp
where T : INumber<T>
```

This allows generic numeric calculations.

## Operator Overloading

`NumericTelemetryReading<T>` overloads operators such as:

```text
+
-
>
<
```

The subtraction operator is used to calculate the change between readings.

## Jagged Arrays

The simulator uses:

```csharp
float[][]
int[][]
bool[][]
```

These values are converted into generic telemetry packets before being sent to the API.

## Recursion

Deployment paths are built recursively, for example:

```text
Smart Farm > Greenhouse A > Hydroponics Row 1
```

A `HashSet<long>` is used to help prevent infinite recursion if an invalid circular hierarchy exists.

## Async Programming

`async` and `await` are used for:

- API requests
- Database operations
- File uploads
- Telemetry processing
- Background refresh
- Simulator delays

---

# First Run Example

1. Start the project.
2. Open the **Devices** page.
3. Register a device.
4. Open the **Sensors** page.
5. Register a sensor.
6. Select its device.
7. Select a deployment location.
8. Choose Float, Integer, or Boolean.
9. Set thresholds if using a numeric sensor.
10. Make sure the sensor is active.
11. Save the sensor.
12. Wait for the simulator to send telemetry.
13. Open the telemetry page to view the readings.

---

# Troubleshooting

## Simulator cannot connect

Make sure the API is running at:

```text
http://localhost:5000/
```

## No telemetry appears

Check that:

- The API is running.
- The Simulator is running.
- A sensor has been created.
- The sensor is active.
- The sensor has a valid data type.

## Sensor stays Offline

A newly registered sensor stays Offline until it receives telemetry.

## Database errors

Check:

- SQL Server Express is running.
- The connection string is correct.
- The database exists.
- Migrations have been applied.

## File upload fails

Check that:

- A sensor has been selected.
- The file is smaller than 10 MB.
- The API can write to the `uploads` folder.

---

# References

Refactoring.Guru, 2026. *Composite in C#*. Available at: https://refactoring.guru/design-patterns/composite/csharp/example [Accessed 15 September 2026].

Microsoft, 2026. *ASP.NET Core Blazor forms and validation*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/blazor/forms/validation?view=aspnetcore-10.0 [Accessed 15 September 2026].

Microsoft, 2026. *ASP.NET Core Blazor file uploads*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/aspnet/core/blazor/file-uploads?view=aspnetcore-10.0 [Accessed 15 September 2026].

Microsoft, 2026. *Asynchronous programming with async and await*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/ [Accessed 15 September 2026].

Microsoft, 2026. *Task.Delay Method*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.delay?view=net-10.0 [Accessed 15 September 2026].

Microsoft, 2026. *Generic types and methods*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics [Accessed 15 September 2026].

Microsoft, 2023. *Generic math*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/standard/generics/math [Accessed 15 September 2026].

Microsoft, 2026. *Operator overloading*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading [Accessed 15 September 2026].

Microsoft, 2026. *Arrays - C# language reference*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays [Accessed 15 September 2026].

Microsoft, 2023. *One-to-many relationships - EF Core*. Microsoft Learn. Available at: https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many [Accessed 15 September 2026].
