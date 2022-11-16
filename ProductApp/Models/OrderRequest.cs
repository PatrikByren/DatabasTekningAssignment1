using ProductApp.Models.Entities;

namespace ProductApp.Models
{
    public class OrderRequest
    {
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; } = null!;
        public int CustomersId { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
    }
}
