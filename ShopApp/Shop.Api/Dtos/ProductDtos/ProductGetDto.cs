using System.ComponentModel;

namespace Shop.Api.Dtos.ProductDtos
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal CostPrice { get; set; }
        public BrandInProductGetDto Brand { get; set; }
    }

    public class BrandInProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
