using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelGroupAssignment1.Models
{
    public class FlightBooking : Booking
    {
        
        [ForeignKey("Flight")]
        public int FlightId { get; set; }
        [Required]
        public Flight Flight { get; set; }
        [Required]
        public string FlightClass { get; set; }
        public ICollection<Passenger>? Passengers { get; set; }

    }
}
