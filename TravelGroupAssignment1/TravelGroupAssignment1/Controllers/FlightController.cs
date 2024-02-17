using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Controllers
{
    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var flights = _context.Flights.ToList();
            return View(flights);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Flight newFlight)
        {
            if (ModelState.IsValid)
            {
                _context.Flights.Add(newFlight);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newFlight);
        }
        [HttpGet]
        public IActionResult Details(int id) {
            var flight = _context.Flights.Find(id);

            return View(flight);

        }
        [HttpGet]
        public IActionResult Edit(int flightId)

        {
            var flight = _context.Flights.Find(flightId);
            return View(flight);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FlightId", "Airline", "Price", "MaxPassenger", "From", "To", "DepartTime", "ArrivalTime")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                Console.WriteLine("isValid");
                ///try
                //{
                    _context.Flights.Update(flight);             
                    _context.SaveChanges(); 
                    return RedirectToAction("Index");
               /* }
                catch (DbUpdateConcurrencyException ex)
                { 
                    if (!FlightExists(flight.FlightId))
                    {
                        return NotFound();
                     }

                    throw;
                }*/

            }
            
            Console.WriteLine("not valid");
            return RedirectToAction("Index", "Home");

        }
        public IActionResult Delete(int id)

        {
            var flight = _context.Flights.Find(id);
            if (flight == null) return NotFound();
            return View(flight);


        }
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight != null)
            {
                _context.Remove(flight);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public bool FlightExists(int id)
        {

            return _context.Flights.Any(e => e.FlightId == id);
        }


    }
}
