using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;
using FlightBooking = TravelGroupAssignment1.Models.FlightBooking;

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
            var fs=_context.FlightBookings.ToList();
            return View(fs);
        }
        [HttpGet]
        public IActionResult Create(int flightId)
        {
            var flight = _context.Flights.Find(flightId);
            if (flight==null)
            {
                return NotFound();

            }
            var flightbooking = new FlightBooking
            {
                FlightId = flightId
            };


            return View(flight);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create([Bind("FlightId, FlightClass, Passengers")] FlightBooking booking)
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

        public IActionResult Edit(int id, [Bind("Flight, FlightClass, Passengers")] Models.FlightBooking flightbooking)
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
