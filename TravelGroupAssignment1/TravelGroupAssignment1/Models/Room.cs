using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Capacity { get; set; }
        public string? BedDescription { get; set; }
        [Required]
        public double PricePerNight { get; set; }
        public int? RoomSize { get; set; }

        // change amenities to class (Amenities:Room = M:M)
        public string? Amenities { get; set; }
        [Required]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public ICollection<RoomBooking>? RoomBookings { get; set; }

    }
}
