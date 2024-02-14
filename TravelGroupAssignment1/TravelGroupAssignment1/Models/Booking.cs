using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public abstract class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public int TripFKId { get; set; } // rename and add navigation property
        public String? BookingReference { get; set; }

        protected Booking() 
        {
            BookingReference = "";
        }

        protected virtual String GenerateBookingReference()
        {
            string date = DateTime.Now.ToString("yyMMddHHmm");
            string uniqueString = Guid.NewGuid().ToString("").Substring(0, 6);
            return date + uniqueString;
        }
    }
}
