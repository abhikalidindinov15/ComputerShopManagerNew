using Microsoft.AspNetCore.Mvc;
using ReportGeneration.Services;
using System.Collections.Generic;
using System.Text;

namespace ComputerShopManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly WeeklyReportService _reportService;

        public ReportController()
        {
            _reportService = new WeeklyReportService();
        }

        [HttpGet("stock")]
        public ActionResult<List<ReportGeneration.Services.StockReport>> GetStockReport()
        {
            var report = _reportService.GenerateStockReport();
            return Ok(report);
        }

        [HttpGet("sales")]
        public ActionResult<List<SaleReport>> GetSalesReport()
        {
            var report = _reportService.GenerateSalesReport();
            return Ok(report);
        }

        [HttpGet("stock/download")]
        public IActionResult DownloadStockReport()
        {
            var report = _reportService.GenerateStockReport();
            var csv = ConvertStockReportToCsv(report);
            var bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", $"stock_report_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
        }

        private string ConvertStockReportToCsv(List<ReportGeneration.Services.StockReport> report)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Quantity,Price");
            
            foreach (var item in report)
            {
                csv.AppendLine($"{item.Id},{EscapeCsvValue(item.Name)},{item.Quantity},{item.Price}");
            }
            
            return csv.ToString();
        }

        private string EscapeCsvValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }
            
            return value;
        }
    }
}


