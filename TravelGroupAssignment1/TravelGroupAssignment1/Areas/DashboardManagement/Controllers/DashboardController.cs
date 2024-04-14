using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Areas.CarManagement.Models;
using TravelGroupAssignment1.Areas.DashboardManagement.Models.ViewModels;
using TravelGroupAssignment1.Areas.HotelManagement.Models;
using TravelGroupAssignment1.Areas.RoomManagement.Models;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Areas.DashboardManagement.Controllers
{
    [Area("DashboardManagement")]
    [Route("[area]/[controller]")]
    /*[Authorize(Roles = "SuperAdmin, Admin")]*/
    public class DashboardController : Controller
    {
        // required
        private readonly ApplicationDbContext _context;

        // required for DI 
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel();

            // Assuming you have DbContext set up
            model.HotelCount = await _context.Hotels.CountAsync();
            model.RoomCount = await _context.Rooms.CountAsync();
            model.FlightCount = await _context.Flights.CountAsync();
            model.CarRentalCompanyCount = await _context.CarRentalCompanies.CountAsync();
            model.CarCount = await _context.Cars.CountAsync();
            // Will comment out after have the user table
            // model.UserCount = await _context.Users.CountAsync();
            return View(model);
        }
    }
}

    