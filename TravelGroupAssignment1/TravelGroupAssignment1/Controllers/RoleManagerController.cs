using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TravelGroupAssignment1.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleManagerController : Controller
    {
        //mechanism to view all roles and add more roles-who can add roles is depending on the business
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerController(RoleManager<IdentityRole> roleManager) //don't need to extend idenitity role
        {
            this._roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRoles(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));

            }
            return RedirectToAction("Index");
        }

    }
}
