using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Controllers
{
    public class FlightBookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightBookingController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(FlightBooking booking)
        {
            if (ModelState.IsValid)
            {
                _context.FlightBookings.Add(booking);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var flight = _context.FlightBookings.Find(id);

            return View(flight);

        }
        [HttpGet]
        public IActionResult Edit(int id)

        {
            var flight = _context.FlightBookings.Find(id);
            return View(flight);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, [Bind("Flight, FlightClass, Passengers")] FlightBooking flightbooking)
        {
            if (id != flightbooking.BookingId)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.FlightBookings.Update(flightbooking);                 //add new project - only in memory, nothing in database yet
                    _context.SaveChanges(); //commits changes to memory
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                { //for when two updates at the same time-rarely will happen with our form
                    if (!FlightBookingExists(flightbooking.BookingId))
                    {
                        return NotFound();
                    }

                    throw;
                }

            }
            return View(flightbooking);

        }
        public IActionResult Delete(int id)

        {
            var flight = _context.FlightBookings.Find(id);
            if (flight == null) return NotFound();
            return View(flight);


        }
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var flight = _context.FlightBookings.Find(id);
            if (flight != null)
            {
                _context.Remove(flight);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public bool FlightBookingExists(int id)    
        {

            return _context.FlightBookings.Any(e => e.BookingId == id);
        }


    }
}
