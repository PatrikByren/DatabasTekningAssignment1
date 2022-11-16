using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApp.Models.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public ICollection<OrderEntity> Orders { get; set; }
    }
}
