using Microsoft.AspNetCore.Identity;
using TravelGroupAssignment1.Areas.CustomerManagement.Models;

namespace TravelGroupAssignment1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChange { get; set; } = 10; //tracks the number of times
        
        public byte[]? ProfilePic { get; set; }
    }
}

