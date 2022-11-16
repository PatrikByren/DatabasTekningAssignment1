using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApp.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; } = null!;
        public int CustomersId { get; set; }
    }
}
