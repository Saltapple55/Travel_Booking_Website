using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Controllers
{
    public class DashboardController : Controller
    {
        // required
        private readonly ApplicationDbContext _context;

        // required for DI 
        public DashboardController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult HotelIndex()
        {
            return View();
        }
    }
}
