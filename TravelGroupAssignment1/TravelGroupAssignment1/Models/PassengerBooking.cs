namespace TravelGroupAssignment1.Models
{
    public class PassengerBooking
    {
        
            public FlightBooking FBooking { get; set; }
            public List<Passenger>? Passengers { get; set; }
        

    }
}
