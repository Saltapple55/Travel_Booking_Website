using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public abstract class Booking
    {
        [Key]
        public int bookingId { get; set; }

        public string bookingReference { get; set; }

        public int tripId { get; set; }
    }
}
