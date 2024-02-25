using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

    }
}
