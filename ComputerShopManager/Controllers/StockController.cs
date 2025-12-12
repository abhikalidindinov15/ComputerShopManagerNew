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







