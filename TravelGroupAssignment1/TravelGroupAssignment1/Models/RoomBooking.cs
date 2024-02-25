using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using TravelGroupAssignment1.Validation;

namespace TravelGroupAssignment1.Models
{
    public class RoomBooking : Booking
    {
        [Required]
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        [Required]
        public DateTime? CheckInDate { get; set; }
        [Required(ErrorMessage = "End date must be after start date")]
        [ValidEndDate("CheckInDate"), ]
        public DateTime? CheckOutDate { get; set; } // validate end date >= start date
    }
}
