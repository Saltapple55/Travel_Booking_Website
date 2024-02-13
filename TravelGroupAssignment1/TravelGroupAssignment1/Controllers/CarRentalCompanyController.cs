using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Controllers
{
    public class CarRentalCompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarRentalCompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarRentalCompanyController
        [HttpGet]
        public IActionResult Index()
        {
            var carRentalCompanies = _context.CarRentalCompanies.ToList();
            return View(carRentalCompanies);
        }

        // GET: CarRentalCompanyController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var hotel = _context.CarRentalCompanies.FirstOrDefault(c => c.CarRentalCompanyId == id);
            if (hotel == null) return NotFound();
            return View();
        }

        // GET: CarRentalCompanyController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarRentalCompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarRentalCompany newCarRentalCompany)
        {
            if (ModelState.IsValid)
            {
                _context.CarRentalCompanies.Add(newCarRentalCompany);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newCarRentalCompany);
        }

        // GET: CarRentalCompanyController/Edit/5
        public IActionResult Edit(int id)
        {
            var hotel = _context.CarRentalCompanies.Find(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // POST: CarRentalCompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CarRentalCompanyId", "CompanyName", "Location", "Rating")] CarRentalCompany company)
        {
            if (id != company.CarRentalCompanyId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.CarRentalCompanies.Update(company);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarRentalCompanyExists(id)) return NotFound();
                    else throw;
                }
            }
            return View();
        }

        // GET: CarRentalCompanyController/Delete/5
        public IActionResult Delete(int id)
        {
            var company = _context.CarRentalCompanies.FirstOrDefault(c => c.CarRentalCompanyId == id);
            if (company == null) return NotFound();
            return View();
        }

        // POST: CarRentalCompanyController/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var company = _context.CarRentalCompanies.Find(id);
            if (company != null)
            {
                _context.Remove(company);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public bool CarRentalCompanyExists(int id)
        {
            return _context.CarRentalCompanies.Any(h => h.CarRentalCompanyId == id);
        }
    }
}
