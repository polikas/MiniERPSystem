using System.ComponentModel.DataAnnotations;

namespace MiniERPSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<Product>? Products { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
