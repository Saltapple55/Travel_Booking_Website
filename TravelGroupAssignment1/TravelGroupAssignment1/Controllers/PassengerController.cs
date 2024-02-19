using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelGroupAssignment1.Data;

namespace TravelGroupAssignment1.Controllers
{
    public class PassengerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PassengerController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index(int numpass, int fbookingId)
        {
            var fbooking = _context.FlightBookings.Find(fbookingId);

            ViewBag.BookingId = fbookingId;
            ViewBag.NumPassengers = numpass;

            return View(fbooking);
        }

    }
}
