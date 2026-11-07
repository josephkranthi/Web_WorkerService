# Web and Worker Service

A .NET 10 solution that demonstrates a web application and a background worker working with the same SQL Server database. The ASP.NET Core MVC application displays employees that have not yet been processed, while the hosted background service periodically selects the oldest unprocessed employee and marks it as processed.

## Features

- Displays unprocessed employees in an ASP.NET Core MVC view.
- Uses Entity Framework Core with SQL Server in both applications.
- Processes employees in the background without blocking the web application.
- Uses dependency injection and a scoped `EmployeeDbContext` in the worker service.
- Includes a 20-second polling interval between worker executions.

## Projects

- **Web_WorkerService** - ASP.NET Core MVC web application for viewing unprocessed employees.
- **WorkerService1** - .NET Worker Service that processes employees in the background.

## Tech Stack

- C#
- .NET 10
- ASP.NET Core MVC
- .NET Worker Service
- Entity Framework Core 10
- SQL Server
- Razor Views
- Bootstrap

## Database Setup

Both projects use a SQL Server database named `EmployeeDB` and an `Employee` table. Create the database and table with the following script:

```sql
CREATE DATABASE EmployeeDB;
GO

USE EmployeeDB;
GO

CREATE TABLE dbo.Employee
(
	Id int IDENTITY(1,1) NOT NULL,
	Name nvarchar(100) NULL,
	Salary decimal(18, 2) NULL,
	IsProcessed bit NULL CONSTRAINT DF_Employee_IsProcessed DEFAULT (0),
	CONSTRAINT PK_Employee PRIMARY KEY (Id)
);
GO
```

Optional sample data:

```sql
INSERT INTO dbo.Employee (Name, Salary, IsProcessed)
VALUES
	('Alice Johnson', 65000.00, 0),
	('Bob Smith', 72000.00, 0),
	('Charlie Brown', 58000.00, 0);
GO
```

## Connection String

The default connection string in both `appsettings.json` files is:

```json
"ConnectionStrings": {
  "EmployeeDB": "Server=localhost;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Update the connection string if your SQL Server instance uses a different server name, SQL authentication, or a different database configuration. Avoid committing passwords or other sensitive credentials to source control.

## How to Run the Project

1. Clone the repository:

   ```bash
   git clone https://github.com/josephkranthi/Web_WorkerService.git
   cd Web_WorkerService
   ```

2. Open `Web_WorkerService.slnx` in Visual Studio 2026 or another IDE that supports .NET 10.

3. Make sure the .NET 10 SDK and SQL Server are installed and running.

4. Create the `EmployeeDB` database and `Employee` table using the SQL script above.

5. Verify the `EmployeeDB` connection string in:
   - `Web_WorkerService/appsettings.json`
   - `WorkerService1/appsettings.json`

6. Start both projects:
   - Start **Web_WorkerService** to open the web application.
   - Start **WorkerService1** to run the background processor.

7. Browse to the Employees page:

   ```text
   https://localhost:7069/Employee
   ```

   The HTTP profile is also available at `http://localhost:5055/Employee`.

## Processing Behavior

The web application lists employees where `IsProcessed` is `false`. The worker service checks the database every 20 seconds, selects the first unprocessed employee by `Id`, marks it as processed, and saves the change. Refresh the Employees page to see the updated list.

## Configuration

Application configuration is stored in each project's `appsettings.json` and can be overridden with environment-specific settings in `appsettings.Development.json`. The web application's local URLs are defined in `Web_WorkerService/Properties/launchSettings.json`.
