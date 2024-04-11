using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelGroupAssignment1.Areas.FlightManagement.Models;

namespace TravelGroupAssignment1.Models
{
    public class FlightBooking : Booking
    {
        
        [ForeignKey("Flight")]
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }
        [Required]
        public string FlightClass { get; set; }
        public string Seat { get; set; }

        public IList<Passenger> Passengers { get; set; }

       // public Passenger[]? Passengers { get; set; } = null;

    }
}
