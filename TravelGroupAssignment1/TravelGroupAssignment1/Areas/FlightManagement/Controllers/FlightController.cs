using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Areas.FlightManagement.Controllers
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
        public IActionResult Details(int flightId)
        {
            var flight = _context.Flights.Find(flightId);

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
        [HttpGet]
        public IActionResult WhatsWrong(int id, int id2)
        {
            ViewData["Id"] = id;
            ViewData["Second"] = id2;
            return View();
        }
        public IActionResult Delete(int flightId)

        {
            var flight = _context.Flights.Find(flightId);
            if (flight == null) return NotFound();
            return View(flight);


        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int flightId)
        {
            var flight = _context.Flights.Find(flightId);
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
