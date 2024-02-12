using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        [Required]
        public string? HotelName { get; set; }
        [Required]
        public string? Location { get; set; }
        
        // Consider changing Amenities to a separate entity
        // Amentities:Hotel = M:M
        public string? Amentities { get; set; }

        public ICollection<Room>? Rooms { get; set; }
    }
}
