using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class CarBooking : Booking
    {
        [Required]
        public int CarId { get; set; }
        public Car? Car { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; } // validate end date >= start date
    }
}
