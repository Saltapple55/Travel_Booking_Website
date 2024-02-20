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
        public IActionResult Index(int flightId)
        {
            var bookings = _context.FlightBookings.Where(t => t.FlightId == flightId).ToList();
            ViewBag.FlightId = flightId;
           // System.Diagnostics.Debug.WriteLine(flightId);

            return View(bookings);
        }
        [HttpGet]
        public IActionResult Create(int flightId)
        {

            var flight = _context.Flights.Find(flightId);

            if (flight == null)
            {
                System.Diagnostics.Debug.WriteLine(flightId);

                return NotFound();

            }
            var flightbooking = new FlightBooking
            {
                FlightId = flightId
            };


            var viewModel = new PassengerBooking
            {
                FBooking = flightbooking
        };
            return View(flightbooking);
        }
        [HttpGet]
        public IActionResult AddPassenger(int index) 
        {
            ViewBag["Index"]= index;
            return PartialView("_AddPassenger");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create( [Bind("BookingId, BookingReference, TripId, FlightClass,Flight, FlightId, Passengers")] FlightBooking booking)
        {
            System.Diagnostics.Debug.WriteLine(booking.Passengers[0]);


            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Model is Valid");
                System.Diagnostics.Debug.WriteLine(booking.Passengers[0].FirstName);

                if (booking.Passengers.Any())
                {
                    foreach(Passenger p in booking.Passengers)
                    {
                        _context.Passengers.Add(p);

                    }
                    _context.SaveChanges();

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No passengers");
                    return NotFound(); }
                _context.FlightBookings.Add(booking);
                _context.SaveChanges();
                // return RedirectToAction("Index", "Passenger", new {fbookingId= booking.BookingId });
                return RedirectToAction("Index", new{flightId = booking.FlightId});
            }
            System.Diagnostics.Debug.WriteLine(booking.BookingId);
            System.Diagnostics.Debug.WriteLine(booking.TripId);
            System.Diagnostics.Debug.WriteLine(booking.FlightClass);
            System.Diagnostics.Debug.WriteLine(booking.FlightId);


            return View(booking);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var flightbooking = _context.FlightBookings.Include(t => t.Flight).FirstOrDefault(booking => booking.BookingId == id);
            if(flightbooking == null) return NotFound();
            //var flightBooking = _context.FlightBookings.Find(id);

            return View(flightbooking);

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
