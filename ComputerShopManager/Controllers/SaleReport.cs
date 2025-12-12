using System;

namespace ComputerShopManager.Controllers
{
    public class SaleReport
    {
        public int SaleId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
