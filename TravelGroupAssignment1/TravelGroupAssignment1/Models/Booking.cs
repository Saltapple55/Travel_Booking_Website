using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public abstract class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int TripId { get; set; } // rename and add navigation property
        public string? BookingReference { get; set; }

        protected Booking() 
        {
            BookingReference = GenerateBookingReference();
        }

        protected virtual String GenerateBookingReference()
        {
            string date = DateTime.Now.ToString("yyMMddHHmm");
            string uniqueString = Guid.NewGuid().ToString("").Substring(0, 6);
            return date + uniqueString;
        }
    }
}
