namespace ProductApp.Models.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<OrderEntity> Orders { get; set; }
    }
}
