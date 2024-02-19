using System.ComponentModel.DataAnnotations;

namespace TravelGroupAssignment1.Models
{
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }
        [Required]
        public string PassportNo { get; set; }

    }
}
