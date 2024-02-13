using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class CarRentalCompany
    {
        [Key]
        public int CarRentalCompanyId { get; set; }
        [Required]
        public string? CompanyName { get; set; }
        [Required]
        public string? Location { get; set; } // !multiple locations?
        public double Rating { get; set; }
        
    }
}
