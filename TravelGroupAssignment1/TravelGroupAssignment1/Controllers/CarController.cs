using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Controllers
{
    public class CarController : Controller
    {
        // required
        private readonly ApplicationDbContext _context;

        // required for DI 
        public CarController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: CarController
        [HttpGet]
        public IActionResult Index()
        {
            var cars = _context.Cars.ToList();
            if (cars == null) return NotFound();
            return View(cars);
        }

        // GET: CarController/Details/5
        [HttpGet]
        public IActionResult Details(int carId)
        {
            var car = _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefault(c => c.CarId == carId);
            return View();
        }

        // GET: CarController/Create
        [HttpGet]
        public IActionResult Create(int carId)
        {
            var company = _context.CarRentalCompanies.Find(carId);
            if (company == null) return NotFound();
            
            var car = new Car { CarId = carId };
            return View(car);
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Make", "Model", "PricePerDay", "CompanyId",
            "Company", "MaxPassenger", "Transmission", "HasAirConditioning", "HasUnlimitedMileage")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
                return RedirectToAction("Index", new { carId = car.CarId });
            }
            ViewBag.CompanyList = new SelectList(_context.CarRentalCompanies, "CarId", "CompanyName", car.CarId);
            return View(car);

        }

        // GET: CarController/Edit/5
        [HttpGet]
        public IActionResult Edit(int carId)
        {
            var car = _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefault(c => c.CarId == carId);
            if (car == null) return NotFound();

            ViewBag.CompanyList = new SelectList(_context.CarRentalCompanies, "CarId", "CompanyName", car.CarId);
            return View(car);
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int carId, [Bind("CarId", "Make", "Model", "PricePerDay", "CompanyId",
            "Company", "MaxPassenger", "Transmission", "HasAirConditioning", "HasUnlimitedMileage")] Car car)
        {
            if (carId != car.CarId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Cars.Update(car);
                _context.SaveChanges();
                return RedirectToAction("Index", new { carId = car.CarId });
            }
            ViewBag.CompanyList = new SelectList(_context.CarRentalCompanies, "CarId", "CompanyName", car.CarId);
            return View();
        }

        // GET: CarController/Delete/5
        [HttpGet]
        public IActionResult Delete(int carId)
        {
            var car = _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefault(c => c.CarId == carId);
            if (car == null) return NotFound();
            return View(car);
        }

        // POST: CarController/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int carId)
        {
            var car = _context.Cars.Find(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
                return RedirectToAction("Index", new { carId = car.CarId });
            }
            return NotFound();
        }
    }
}
