using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelGroupAssignment1.Models
{
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }
        [Required]
        public string PassportNo { get; set; }
        
        //public int BookingId { get; set; }
        //public FlightBooking? FlightBooking { get; set; }

    }
}
