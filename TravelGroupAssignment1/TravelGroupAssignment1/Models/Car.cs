using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Required]
        public string? Make { get; set; }
        [Required]
        public string? Mode {  get; set; }
        [Required]
        public double PricePerDay { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public CarRentalCompany? Company { get; set; }
        public int MaxPassengers { get; set; }
        public string? Transmission { get; set; }
        public bool HasAirConditioning { get; set; }
        public bool HasUnlimitedMileage { get; set; }
    }
}
