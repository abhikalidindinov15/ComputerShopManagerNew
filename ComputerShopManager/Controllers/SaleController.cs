using Microsoft.AspNetCore.Mvc;
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
