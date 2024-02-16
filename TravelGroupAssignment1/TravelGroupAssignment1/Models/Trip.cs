namespace TravelGroupAssignment1.Models
{
    public class Trip
    { 
        public int TripId { get; set; }
        public int CustomerId { get; set; }
        public Booking[] FlightBookings {  get; set; }

        public Booking[] CarBookings { get; set; }
        public Booking[] HotelBookings { get; set; }


    }
}
