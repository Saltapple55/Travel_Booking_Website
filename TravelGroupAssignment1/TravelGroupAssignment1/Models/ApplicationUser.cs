using Microsoft.AspNetCore.Identity;

namespace TravelGroupAssignment1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChange { get; set; } = 10; //tracks the number of times

    }
}

