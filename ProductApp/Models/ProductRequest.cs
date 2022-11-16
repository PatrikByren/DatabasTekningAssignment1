using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApp.Models
{
    public class ProductRequest
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
