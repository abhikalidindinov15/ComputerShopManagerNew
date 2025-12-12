# Copilot Instructions for ComputerShopManagerSolution

## Project Overview
- **Solution Structure:**
  - `ComputerShopManager` (ASP.NET Core Web API, main backend)
  - `StockManager`, `SaleManagement`, `ReportGeneration` (class libraries for business logic)
  - `ComputerShopManagerUI` (Razor Pages frontend)
- **Architecture:**
  - Layered: UI → API Controllers → Service Layer (in class libraries) → SQLite DB
  - Controllers delegate to service classes in referenced libraries.
  - Data flows from UI forms to API endpoints, then to service classes, and finally to the database.

## Developer Workflows
- **Build:**
  - Use `dotnet build` at the solution root.
- **Run API:**
  - `dotnet run --project ComputerShopManager/ComputerShopManager.csproj`
- **Run UI:**
  - `dotnet run --project ComputerShopManagerUI/ComputerShopManagerUI.csproj`
- **Database:**
  - SQLite files are in the API project directory. No migrations; DB is managed by service classes.
- **Swagger:**
  - API docs at `/swagger` when running in Development.

## Project Conventions & Patterns
- **Controllers:**
  - One controller per domain (`StockController`, `SaleController`, `ReportController`).
  - Route pattern: `api/[controller]`.
  - Use `[HttpPost]` for mutations, `[HttpGet]` for queries.
- **Service Instantiation:**
  - Some controllers use `new Service()` instead of DI. Prefer DI for testability.
- **Models:**
  - Data models are in `StockManager.Models`, `SaleManagement.Models`, `ReportGeneration.Models`.
- **Error Handling:**
  - Minimal; most endpoints return `Ok()` or basic error messages.
- **API to SOAP Migration:**
  - If migrating to SOAP, define service contracts in a `Contracts/` folder and implement them in a `Services/` folder. Use gRPC or WCF as appropriate.

## Integration Points
- **Database:**
  - Uses `System.Data.SQLite` for persistence.
- **Swagger:**
  - Uses `Swashbuckle.AspNetCore` for API documentation.
- **Frontend:**
  - Razor Pages in `ComputerShopManagerUI` call API endpoints via HTTP.
- **External Libraries:**
  - `Microsoft.Extensions.*` for DI, logging, configuration.

## Key Files & Directories
- `ComputerShopManager/Controllers/` — API endpoints
- `StockManager/Services/StockService.cs` — Stock logic
- `SaleManagement/Services/SaleService.cs` — Sales logic
- `ReportGeneration/Services/WeeklyReportService.cs` — Reporting logic
- `ComputerShopManagerUI/Pages/` — UI pages
- `appsettings.json` — Logging and config

## Examples
- **Add Stock:**
  - POST `/api/stock/add` with a `StockItem` JSON body
- **Add Sale:**
  - POST `/api/sale/add` with a `SaleTransaction` JSON body
- **Get Reports:**
  - GET `/api/report/stock` or `/api/report/sales`

## Special Notes
- No custom build/test scripts; standard .NET CLI is used.
- No migrations or code-first DB; DB is managed directly by service classes.
- If adding new endpoints, follow the existing controller/service pattern.
- For SOAP migration, keep all business logic in service classes and expose via service contracts.
