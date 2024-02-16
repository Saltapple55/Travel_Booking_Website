using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }

        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }

        [Required]
        public string phone { get; set; }
        [Required]
        public string passportNo { get; set; }

    }
}
