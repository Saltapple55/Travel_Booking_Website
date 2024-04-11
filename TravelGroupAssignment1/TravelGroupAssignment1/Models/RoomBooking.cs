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
        [Display(Name = "Check In Date")]
        [DataType(DataType.DateTime)]
        public DateTime? CheckInDate { get; set; }

        [Required]
        [ValidEndDate("CheckInDate", ErrorMessage = "End date must be after start date")]
        [Display(Name = "Check Out Date")]
        [DataType(DataType.DateTime)]
        public DateTime? CheckOutDate { get; set; } // validate end date >= start date
    }
}
