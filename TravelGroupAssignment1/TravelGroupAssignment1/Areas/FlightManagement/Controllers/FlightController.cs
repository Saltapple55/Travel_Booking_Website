using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Areas.FlightManagement.Models;

namespace TravelGroupAssignment1.Areas.FlightManagement.Controllers
{
    [Area("FlightManagement")]
    [Route("[controller]")]
    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var flights = await _context.Flights.ToListAsync();
            return View(flights);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flight newFlight)
        {
            if (ModelState.IsValid)
            {
                await _context.Flights.AddAsync(newFlight);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newFlight);
        }

        [HttpGet("Details/{flightId:int}")]
        public async Task<IActionResult> Details(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);

            return View(flight);

        }

        [HttpGet("Edit/{flightId:int}")]
        public IActionResult Edit(int flightId)

        {
            var flight = _context.Flights.Find(flightId);
            return View(flight);


        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FlightId", "Airline", "Price", "MaxPassenger", "From", "To", "DepartTime", "ArrivalTime")] Flight flight)
        {

            if (id != flight.FlightId)
            {
                System.Diagnostics.Debug.WriteLine(id);
                System.Diagnostics.Debug.WriteLine(flight.FlightId);
                Console.WriteLine(flight.FlightId);
                // return RedirectToAction("Index", "Flight", id, flight.FlightId);
                return RedirectToAction("Index");

            }
            if (ModelState.IsValid)
            {
                _context.Flights.Update(flight);
                _context.SaveChanges();
                return RedirectToAction("Index");


            }

            return View(flight);

        }

        [HttpGet("Delete/{flightId:int}")]
        public IActionResult Delete(int flightId)

        {
            var flight = _context.Flights.Find(flightId);
            if (flight == null) return NotFound();
            return View(flight);


        }

        [HttpPost("DeleteConfirmed/{flightId:int}"), ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);
            if (flight != null)
            {
                _context.Remove(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public bool FlightExists(int id)
        {

            return _context.Flights.Any(e => e.FlightId == id);
        }

        [HttpGet("Search/{searchString?}")]
        public async Task<IActionResult> Search(string locationFrom, string location, int capacity, DateTime startDate, DateTime endDate)
        {
            var flightQuery = from p in _context.Flights
                              select p;

            bool searchValid = !string.IsNullOrEmpty(location) && capacity > 0;
            if (searchValid)
            {
                flightQuery = flightQuery.Where(f => f.From.Contains(locationFrom) && f.To.Contains(location));
                // I changed line below, not sure if I fixed it or not
                flightQuery = flightQuery.Where(f => f.DepartTime.Date >= startDate.Date && f.ArrivalTime.Date <= endDate.Date);

            }
            else
            {
                return RedirectToAction("Index");
            }
            var flights = await flightQuery.ToListAsync();
            ViewBag.SearchValid = searchValid;
            ViewBag.Location = location;
            ViewBag.Capacity = capacity;
            ViewBag.StartDate = startDate;
            return View("Index", flights);
        }

        [HttpGet("SearchAjax/{searchString?}")]
        public async Task<IActionResult> SearchAjax(string locationFrom, string location, int capacity, DateTime startDate, DateTime endDate)
        {
            var flightQuery = from p in _context.Flights
                              select p;

            bool searchValid = !string.IsNullOrEmpty(location) && capacity > 0;
            if (searchValid)
            {
                flightQuery = flightQuery.Where(f => f.From.Contains(locationFrom) && f.To.Contains(location));
                // I changed line below, not sure if I fixed it or not
                flightQuery = flightQuery.Where(f => f.DepartTime.Date >= startDate.Date && f.ArrivalTime.Date <= endDate.Date);

            }
            else
            {
                return RedirectToAction("Index");
            }
            var flights = await flightQuery.ToListAsync();

            return Json(flights);
        }



    }
}
