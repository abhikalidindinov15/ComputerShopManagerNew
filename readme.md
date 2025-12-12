ComputerShopManagerSolution
│
├── ComputerShopManager (Web API Project)
│   ├── Controllers
│   │   ├── StockController.cs
│   │   ├── SaleController.cs
│   │   └── ReportController.cs
│   ├── Properties
│   └── appsettings.json
│
├── StockManager (Class Library)
│   ├── Models
│   │   └── StockItem.cs
│   ├── Services
│   │   ├── StockService.cs
│   │   ├── ExcelImportService.cs
│   │   └── ReportGenerationService.cs
│   └── StockManager.csproj
│
├── SaleManagement (Class Library)
│   ├── Models
│   │   └── SaleTransaction.cs
│   ├── Services
│   │   ├── SaleService.cs
│   │   └── StockServiceIntegration.cs  (Uses methods from StockManager)
│   └── SaleManagement.csproj
│
├── ReportGeneration (Class Library)
│   ├── Models
│   │   └── ReportData.cs
│   ├── Services
│   │   ├── WeeklyReportService.cs
│   │   ├── MonthlyReportService.cs
│   │   └── YearlyReportService.cs
│   └── ReportGeneration.csproj
│
└── ComputerShopManager.sln
Explanation of Projects and Folders:
ComputerShopManager (Web API Project):






Sure! Below is an example of how to add data to a SQLite database within the structure you've provided. I'll show how to integrate SQLite logic in the StockService and SaleService classes using System.Data.SQLite. This setup will allow for data to be saved to and retrieved from a SQLite database.

Step 1: Add SQLite NuGet Package
Before you start, ensure you have the SQLite package installed in each project that interacts with the database.

In the terminal, navigate to each project (e.g., StockManager and SaleManagement) and run:

bash
Copy code
dotnet add package System.Data.SQLite
Step 2: Create a Database and Tables (If They Don’t Exist)
To set up the database and tables, we’ll add logic to initialize SQLite within the StockService and SaleService.

3. Implement SQLite Logic in StockService
In StockService.cs, add methods to insert stock items and check for a database connection.

StockService.cs
csharp
Copy code
using System;
using System.Data.SQLite;
using StockManager.Models;

namespace StockManager.Services
{
    public class StockService
    {
        private readonly string _connectionString = "Data Source=ComputerShopManager.db;Version=3;";

        public StockService()
        {
            InitializeDatabase();
        }

        // Create the table if it doesn't exist
        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string tableQuery = @"CREATE TABLE IF NOT EXISTS StockItem (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Name TEXT NOT NULL,
                                        Quantity INTEGER,
                                        Price REAL
                                    );";
                using (var command = new SQLiteCommand(tableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Add a new stock item
        public void AddStockItem(StockItem stockItem)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO StockItem (Name, Quantity, Price) VALUES (@Name, @Quantity, @Price);";
                
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", stockItem.Name);
                    command.Parameters.AddWithValue("@Quantity", stockItem.Quantity);
                    command.Parameters.AddWithValue("@Price", stockItem.Price);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
4. Implement SQLite Logic in SaleService
In SaleService.cs, add methods to record sales transactions in a SaleTransaction table.

SaleService.cs
csharp
Copy code
using System;
using System.Data.SQLite;
using SaleManagement.Models;

namespace SaleManagement.Services
{
    public class SaleService
    {
        private readonly string _connectionString = "Data Source=ComputerShopManager.db;Version=3;";

        public SaleService()
        {
            InitializeDatabase();
        }

        // Create the table if it doesn't exist
        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string tableQuery = @"CREATE TABLE IF NOT EXISTS SaleTransaction (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        StockItemId INTEGER,
                                        Quantity INTEGER,
                                        SaleDate TEXT,
                                        FOREIGN KEY (StockItemId) REFERENCES StockItem (Id)
                                    );";
                using (var command = new SQLiteCommand(tableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Add a new sale transaction
        public void AddSaleTransaction(SaleTransaction saleTransaction)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO SaleTransaction (StockItemId, Quantity, SaleDate) VALUES (@StockItemId, @Quantity, @SaleDate);";
                
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@StockItemId", saleTransaction.StockItemId);
                    command.Parameters.AddWithValue("@Quantity", saleTransaction.Quantity);
                    command.Parameters.AddWithValue("@SaleDate", saleTransaction.SaleDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
Step 5: Define Models for Database
Ensure your models match the table structure to handle data correctly.

StockItem.cs (in StockManager.Models)
csharp
Copy code
namespace StockManager.Models
{
    public class StockItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
SaleTransaction.cs (in SaleManagement.Models)
csharp
Copy code
using System;

namespace SaleManagement.Models
{
    public class SaleTransaction
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
Step 6: Use StockService and SaleService in Controllers
In your controllers, inject StockService and SaleService to perform add operations:

StockController.cs
csharp
Copy code
using Microsoft.AspNetCore.Mvc;
using StockManager.Models;
using StockManager.Services;

namespace ComputerShopManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;

        public StockController()
        {
            _stockService = new StockService();
        }

        [HttpPost("add")]
        public IActionResult AddStock([FromBody] StockItem stockItem)
        {
            _stockService.AddStockItem(stockItem);
            return Ok("Stock item added successfully.");
        }
    }
}
SaleController.cs
csharp
Copy code
using Microsoft.AspNetCore.Mvc;
using SaleManagement.Models;
using SaleManagement.Services;

namespace ComputerShopManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly SaleService _saleService;

        public SaleController()
        {
            _saleService = new SaleService();
        }

        [HttpPost("add")]
        public IActionResult AddSale([FromBody] SaleTransaction saleTransaction)
        {
            _saleService.AddSaleTransaction(saleTransaction);
            return Ok("Sale transaction added successfully.");
        }
    }
}



in case sqlite issue
dotnet add package System.Data.SQLite


cd computerShopManager/
dotnet clean ComputerShopManager.sln
dotnet build ComputerShopManager.sln

Testing the API Endpoints
To test the add functionality:

Run the application: dotnet run --project ComputerShopManager.
Use an API tool (like Postman) to test the endpoints.
For example:

POST /api/stock/add with JSON payload:
json
Copy code
{
    "name": "Laptop",
    "quantity": 10,
    "price": 1200.00
}
POST /api/sale/add with JSON payload:
json
Copy code
{
    "stockItemId": 1,
    "quantity": 2,
    "saleDate": "2023-01-01T10:00:00"
}
This should successfully insert records into ComputerShopManager.db.


dotnet add package RhinoMocks
dotnet add package xunit
dotnet add package xunit.runner.visualstudio