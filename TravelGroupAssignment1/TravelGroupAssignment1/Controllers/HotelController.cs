﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Controllers
{
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HotelController
        [HttpGet]
        public IActionResult Index()
        {
            var hotels = _context.Hotels.ToList();
            return View(hotels);
        }

        // GET: HotelController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == id);
            if(hotel == null) return NotFound();
            return View(hotel);
        }

        // GET: HotelController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: HotelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hotel newHotel)
        {
            if (ModelState.IsValid)
            {
                _context.Hotels.Add(newHotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newHotel);
        }

        // GET: HotelController/Edit/5
        public IActionResult Edit(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // POST: HotelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("HotelId", "HotelName", "Location", "Description","Amenities")] Hotel hotel)
        {
            if(id != hotel.HotelId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Hotels.Update(hotel);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(id)) return NotFound();
                    else throw;
                }
            }
            return View();
        }

        // GET: HotelController/Delete/5
        public IActionResult Delete(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // POST: HotelController/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel != null)
            {
                _context.Remove(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> Search(string location, int capacity)
        {
            var hotelQuery = from p in _context.Hotels
                             select p;
            bool searchValid = !String.IsNullOrEmpty(location) && capacity > 0;
            if (searchValid)
            {
                hotelQuery = hotelQuery.Where(h => !String.IsNullOrEmpty(h.Location) && 
                                        h.Location.Contains(location) || !String.IsNullOrEmpty(h.Description) && h.Description.Contains(location));
                hotelQuery = hotelQuery.Where(h => h.Rooms != null 
                                        && h.Rooms.Any(r => r.Capacity >= capacity));
            } 
            else
            {
                return RedirectToAction("Index");
            }
            var hotels = await hotelQuery.ToListAsync();
            ViewBag.SearchValid = searchValid;
            ViewBag.Location = location;
            ViewBag.Capacity = capacity;
            return View("Index", hotels);
        }

        public bool HotelExists(int id)
        {
            return _context.Hotels.Any(h => h.HotelId == id);
        }
    }
}
