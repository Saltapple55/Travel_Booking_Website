using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class FlightPassengerAndBooking
    {
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set;}

        public int BookingId { get; set; }
        public FlightBooking FlightBooking { get; set; }    

        public string Seat { get; set; }

    }
}
