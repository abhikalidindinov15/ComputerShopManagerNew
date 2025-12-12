using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Register services from other projects
builder.Services.AddScoped<StockManager.Services.StockService>();
builder.Services.AddScoped<SaleManagement.Services.SaleService>();
builder.Services.AddScoped<ReportGeneration.Services.WeeklyReportService>();
//builder.Services.AddScoped<ReportGeneration.Services.MonthlyReportService>();
//builder.Services.AddScoped<ReportGeneration.Services.YearlyReportService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
