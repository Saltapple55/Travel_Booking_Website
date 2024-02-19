namespace TravelGroupAssignment1.Models
{
    public class Room_RoomBooking
    {
        public int RoomId { get; set; }
        public int BookingId { get; set; }
        public Room? Room { get; set; }
        public RoomBooking? RoomBooking { get; set; }
    }
}
