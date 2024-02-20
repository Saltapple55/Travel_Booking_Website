using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class RoomBooking : Booking
    {
        [Required]
        public ICollection<Room_RoomBooking>? Room_RoomBooking { get; set; }
        [Required]
        public DateTime? CheckInDate { get; set; }
        [Required]
        public DateTime? CheckOutDate { get; set; } // validate end date >= start date
    }
}
