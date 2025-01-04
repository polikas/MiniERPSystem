using System.ComponentModel.DataAnnotations;

namespace MiniERPSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
