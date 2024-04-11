using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelGroupAssignment1.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        //public IList<Booking>? Bookings { get; set; }

    }
}
