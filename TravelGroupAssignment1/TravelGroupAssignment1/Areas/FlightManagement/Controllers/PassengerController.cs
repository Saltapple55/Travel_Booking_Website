using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Areas.FlightManagement.Controllers
{
    public class PassengerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PassengerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int fbookingId)
        {
            /*var fbooking = _context.Passengers.Where(t => t.BookingId == fbookingId).ToList();
            ViewBag.FlightId = flightId;
            System.Diagnostics.Debug.WriteLine(flightId);

            return View(flight);
            ViewBag.BookingId = fbookingId;

            return View(fbooking);
            */
            return View();
        }

    }
}
