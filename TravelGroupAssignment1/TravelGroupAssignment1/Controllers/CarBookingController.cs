using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Controllers
{
    public class CarBookingController : Controller
    {
        // required
        private readonly ApplicationDbContext _context;

        // required for DI 
        public CarBookingController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: CarBookingController/5
        [HttpGet]
        public IActionResult Index(int carId)
        {
            var bookings = _context.CarBookings
                        .Include(c => c.Car)
                        .Where(c => c.CarId == carId)
                        .ToList();
            if (bookings == null) return NotFound();
            Car? car = _context.Cars.Find(carId);
            if (car == null) return NotFound();
            ViewBag.CarName = car.Make + " " + car.Model;
            ViewBag.CarType = car.Type;
            ViewBag.CarId = carId;
            return View(bookings);
        }

        // GET: CarBookingController/Details/5
        [HttpGet]
        public IActionResult Details(int id, string? con="CarBooking")
        {
            var booking = _context.CarBookings
                        .Include(cb => cb.Car)
                        .FirstOrDefault(cb => cb.BookingId == id);
            ViewBag.Controller = con;

            return View(booking);
        }

        // GET: CarBookingController/Create/5
        [HttpGet]
        public IActionResult Create(int carId, DateTime? startDate, DateTime? endDate)
        {
            var car = _context.Cars.Find(carId);
            if (car == null) return NotFound();
            var company = _context.CarRentalCompanies.Find(car.CompanyId);
            ViewBag.CarName = car.Make + " " + car.Model;
            ViewBag.CarType = car.Type;
            ViewBag.Car = car;
            ViewBag.Company = company;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;



            return View(new CarBooking { CarId = car.CarId, TripId= 1 });

        }

        // POST: CarBookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TripId", "BookingReference",
            "CarId", "Car", "StartDate", "EndDate")] CarBooking carBooking)
        {
            // View bag components to be show in page
            var car = _context.Cars.Find(carBooking.CarId);
            if (car == null) return NotFound();
            var company = _context.CarRentalCompanies.Find(car.CompanyId);
            ViewBag.CarName = car.Make + " " + car.Model;
            ViewBag.CarType = car.Type;
            ViewBag.Car = car;
            ViewBag.Company = company;

            if (ModelState.IsValid)
            {
                // check if car is booked 
                if (carBookingExists(carBooking))
                {
                    ModelState.AddModelError("", "Car is not available for booking on given date range.");
                    return View(carBooking);
                }
                _context.CarBookings.Add(carBooking);
                _context.SaveChanges();
                return RedirectToAction("Index", new { carId = carBooking.CarId });
            }
            return View(carBooking);
        }

        // GET: CarBookingController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var carBooking = _context.CarBookings
                            .Include(cb => cb.Car)
                            .FirstOrDefault(cb => cb.BookingId == id);
            if (carBooking == null) return NotFound();



            var car = _context.Cars.Find(carBooking.CarId);
            if (car == null) return NotFound();
            ViewBag.CarName = car.Make + " " + car.Model;
            ViewBag.CarType = car.Type;
            ViewBag.Car = car;

            return View(carBooking);
        }

        // POST: CarBookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookingId", "TripId", "BookingReference",
            "CarId", "Car", "StartDate", "EndDate")] CarBooking carBooking)
        {
            if (id != carBooking.BookingId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.CarBookings.Update(carBooking);
                _context.SaveChanges();
                return RedirectToAction("Index", new { carId = carBooking.CarId});
            }

            var car = _context.Cars.Find(carBooking.CarId);
            if (car == null) return NotFound();
            ViewBag.CarName = car.Make + " " + car.Model;
            ViewBag.CarType = car.Type;
            ViewBag.Car = car;
            return View(carBooking);

        }

        // GET: CarBookingController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id, string? con = "CarBooking")
        {
            var carBooking = _context.CarBookings
                        .Include(cb => cb.Car)
                        .FirstOrDefault(cb => cb.BookingId == id);
            if (carBooking == null) return NotFound();
            ViewBag.Controller = con;
            System.Diagnostics.Debug.WriteLine(con);
            return View(carBooking);
        }

        // POST: CarBookingController/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string? con = "CarBooking")
        {
            var carBooking = _context.CarBookings.Find(id);
            
            if (carBooking != null)
            {
                _context.CarBookings.Remove(carBooking);
                _context.SaveChanges();
                if(string.Equals(con, "CarBooking"))
                return RedirectToAction("Index", new { carId = carBooking.CarId });

                return RedirectToAction("Index", con);

            }
            return NotFound();
        }

        // Helper function: check if no identical carbooking (car, start date, end date) exists
        public bool carBookingExists(CarBooking carBooking)
        {
            var carBookingQuery = from p in _context.CarBookings
                                  select p;
            carBookingQuery = carBookingQuery.Where(c => c.CarId == carBooking.CarId)
                                            .Where(c => c.StartDate >= carBooking.StartDate && c.StartDate <= carBooking.EndDate ||
                                            c.EndDate >= carBooking.StartDate && c.EndDate <= carBooking.EndDate);
            var existingCarBookings = carBookingQuery.ToList(); 
            return existingCarBookings.Any();
        }
    }
}
