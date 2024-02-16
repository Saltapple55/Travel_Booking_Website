using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelGroupAssignment1.Models
{
    public abstract class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public string BookingReference { get; set; }


        [ForeignKey("Trip")]
        public int TripId { get; set; }
    }
}  
