using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using TravelGroupAssignment1.Models;
using TravelGroupAssignment1.Validation;

namespace TravelGroupAssignment1.Areas.RoomManagement.Models
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
        [Display(Name = "Check Out Date")]
        [DataType(DataType.DateTime)]
        [ValidEndDate("CheckInDate")]
        public DateTime? CheckOutDate { get; set; } // validate end date >= start date
    }
}
