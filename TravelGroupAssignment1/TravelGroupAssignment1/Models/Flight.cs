using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class Flight
    {

        [Key] public int FlightId { get; set; }
        [Required]
        public string Airline { get; set;}
        [Required]

        public double Price { get; set;}
        [Required]

        public int MaxPassenger { get; set;}
        public IEnumerable<Passenger> PassengerList { get; set;}
        [Required]

        public string Departure { get; set;}
        [Required]

        public string Destination { get; set;}
        [Required]


        public DateTime? DepartTime { get; set;}
        [Required]

        public DateTime? ArrivalTime { get; set; }
        [Required]
        public string[] Stops { get; set; }



    }
}
