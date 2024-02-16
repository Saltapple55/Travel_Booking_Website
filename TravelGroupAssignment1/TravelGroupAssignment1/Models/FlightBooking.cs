using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class FlightBooking: Booking
    {    
        public int BookingId { get; set; }
        [Required]
        public int FlightId { get; set; }
        [Required]
        public Flight Flight { get; set; }
        [Required]
        public string FlightClass { get; set; }
        [Required]
        public Passenger[] Passengers { get; set; }

    }
}
