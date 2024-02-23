using System.ComponentModel.DataAnnotations;
using TravelGroupAssignment1.Validation;

namespace TravelGroupAssignment1.Models
{
    public class Flight
    {

        [Key]  public int FlightId { get; set; }
        [Required]
        public string Airline { get; set;}
        [Required]

        public double Price { get; set;}
        [Required]

        public int MaxPassenger { get; set;}
        public IEnumerable<Passenger>? PassengerList { get; set;}
        [Required]
        public string From { get; set;}
        [Required]
        public string To { get; set;}

        [Required]

        public DateTime DepartTime { get; set;}
        [Required]
        [ValidEndDate("DepartTime")]

        public DateTime ArrivalTime { get; set; }



    }
}
