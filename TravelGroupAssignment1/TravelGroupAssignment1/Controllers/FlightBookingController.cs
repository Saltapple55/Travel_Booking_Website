using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
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
            var bookings = _context.FlightBookings.Where(t => t.FlightId == flightId).Include(t=>t.Flight).Include(p=>p.Passengers).ToList();
            System.Diagnostics.Debug.WriteLine(bookings.Count);
            System.Diagnostics.Debug.WriteLine(flightId);

            ViewBag.FlightId = flightId;
            ViewBag.From = bookings[0].Flight.From;
            ViewBag.To = bookings[0].Flight.To;

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

        public IActionResult Create( [Bind("BookingId, BookingReference, TripId, FlightClass,Flight, Seat, FlightId, Passengers")] FlightBooking booking)
        {


            if (ModelState.IsValid)
            {
                

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
            

            return View(booking);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var flightbooking = _context.FlightBookings.Include(t => t.Flight).Include(p=>p.Passengers).FirstOrDefault(booking => booking.BookingId == id);

            // var passengers = _context.Passengers.Where(t => t.Flig == flightbooking.BookingId).ToList();
            if (flightbooking == null) return NotFound();
            //var flightBooking = _context.FlightBookings.Find(id);

            return View(flightbooking);

        }
        [HttpGet]
        public IActionResult Edit(int id)

        {
            var booking = _context.FlightBookings.Include(f=>f.Flight).Include(p=>p.Passengers).FirstOrDefault(b=>b.BookingId==id);
            if (booking == null) return NotFound();
            ViewBag.BookingsList = new SelectList(_context.Flights, "FlightId", "From" , booking.FlightId.ToString());

            return View(booking);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, [Bind("BookingId,FlightId, FlightClass, Seat, Passengers")] Models.FlightBooking flightbooking)
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
                    return RedirectToAction("Index", new { flightId = flightbooking.BookingId });
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
            var booking = _context.FlightBookings.Include(t => t.Flight).Include(p=>p.Passengers).FirstOrDefault(b => b.BookingId == id);

            if (booking == null) return NotFound();
            return View(booking);


        }
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int bookingId)
        {
            var booking = _context.FlightBookings.Include(p=>p.Passengers).FirstOrDefault(b=>b.BookingId==bookingId);
           
            if (booking != null)
            {
                _context.Passengers.Remove(booking.Passengers[0]);
                _context.FlightBookings.Remove(booking);
                _context.SaveChanges();
                return RedirectToAction("Index", new { flightId = booking.FlightId });
            }
            return NotFound();
        }

        public bool FlightBookingExists(int id)    
        {

            return _context.FlightBookings.Any(e => e.BookingId == id);
        }


    }
}
