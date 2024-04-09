using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
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
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars
                        .Include(cars => cars.Company)
                        .ToListAsync();
            if (cars == null) return NotFound();
            return View(cars);
        }

        // GET: CarController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int carId)
        {
            var car = await _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null) return NotFound();
            return View(car);
        }

        // GET: CarController/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Companies = new SelectList(_context.CarRentalCompanies, "CarRentalCompanyId", "CompanyName");
            var companyObjects = await _context.CarRentalCompanies
                                        .ToDictionaryAsync(c => c.CarRentalCompanyId, c => c);
            ViewBag.CompanyObjects = companyObjects;
            return View();
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Make", "Model", "Type", "PricePerDay", "MaxPassengers",
            "CompanyId", "Company", "Transmission", "HasAirConditioning", "HasUnlimitedMileage")] Car car)
        {
            if (ModelState.IsValid)
            {
                // retrieve Company navigation property via id
                CarRentalCompany? company = await _context.CarRentalCompanies.FirstOrDefaultAsync(cr => cr.CarRentalCompanyId == car.CompanyId);
                // create new Car object with navigation property
                Car newCar = new Car { 
                    Make = car.Make, 
                    Model = car.Model, 
                    Type = car.Type,
                    PricePerDay = car.PricePerDay, 
                    MaxPassengers = car.MaxPassengers, 
                    Transmission = car.Transmission, 
                    HasAirConditioning = car.HasAirConditioning,
                    HasUnlimitedMileage = car.HasUnlimitedMileage, 
                    CompanyId = car.CompanyId, 
                    Company = company }; 
                await _context.Cars.AddAsync(newCar);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Companies = new SelectList(_context.CarRentalCompanies, "CarRentalCompanyId", "CompanyName", car.CarId);
            return View(car);

        }

        // GET: CarController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int carId)
        {
            var car = await _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null) return NotFound();

            ViewBag.CompanyList = new SelectList(_context.CarRentalCompanies, "CarRentalCompanyId", "CompanyName", car.CarId);
            return View(car);
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int carId, [Bind("CarId", "Make", "Model", "Type", "PricePerDay", "MaxPassengers",
            "CompanyId", "Company", "Transmission", "HasAirConditioning", "HasUnlimitedMileage")] Car car)
        {
            if (carId != car.CarId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyList = new SelectList(_context.CarRentalCompanies, "CompanyId", "CompanyName");
            return View();
        }

        // GET: CarController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int carId)
        {
            var car = await _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null) return NotFound();
            return View(car);
        }

        // POST: CarController/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> Search(string location, DateTime startDate, DateTime endDate)
        {
            var carQuery = from p in _context.Cars
                           select p;
            bool searchValid = !String.IsNullOrEmpty(location);
            if (searchValid)
            {
                // search by location, and by cars with no bookings in given date range
                carQuery = carQuery.Where(c => c.Company != null && c.Company.Location.Contains(location))
                                .Where(c => !c.Bookings.Any(b => b.EndDate >= startDate && b.EndDate <= endDate
                                    || b.StartDate >= startDate && b.StartDate <= endDate));
            }
            else
            {
                return RedirectToAction("Index");
            }
            var cars = await carQuery.Include(c => c.Company).ToListAsync();
            ViewBag.SearchValid = searchValid;
            ViewBag.Location = location;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            return View("Index", cars);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAjax(string location, DateTime startDate, DateTime endDate)
        {
            var carQuery = from p in _context.Cars
                           select p;
            bool searchValid = !String.IsNullOrEmpty(location);
            if (searchValid)
            {
                // search by location, and by cars with no bookings in given date range
                carQuery = carQuery.Where(c => c.Company != null && c.Company.Location.Contains(location))
                                .Where(c => !c.Bookings.Any(b => b.EndDate >= startDate && b.EndDate <= endDate
                                    || b.StartDate >= startDate && b.StartDate <= endDate));
            }
            else
            {
                return StatusCode(400, "Search invalid");
            }

            //var cars = await carQuery.ToListAsync();
            var cars = await carQuery.Include(c => c.Company).Select(c => new
            {
                carId = c.CarId,
                make = c.Make,
                model = c.Model,
                maxPassengers = c.MaxPassengers,
                transmission = c.Transmission,
                hasAirConditioning = c.HasAirConditioning,
                hasUnlimitedMileage = c.HasUnlimitedMileage,
                pricePerDay = c.PricePerDay,
                companyName = c.Company.CompanyName
            }).ToListAsync();

            //????
            //ViewBag.SearchValid = searchValid;
            //ViewBag.Location = location;
            //ViewBag.StartDate = startDate;
            //ViewBag.EndDate = endDate;

            return Json(cars);
        }
    }
}
