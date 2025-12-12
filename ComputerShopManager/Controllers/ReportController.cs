using Microsoft.AspNetCore.Mvc;
using ReportGeneration.Services;
using System.Collections.Generic;

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
        public ActionResult<List<StockReport>> GetStockReport()
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
    }
}


