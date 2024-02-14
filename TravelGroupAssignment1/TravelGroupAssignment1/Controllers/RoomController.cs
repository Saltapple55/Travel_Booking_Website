using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Controllers
{
    public class RoomController : Controller
    {
        // required
        private readonly ApplicationDbContext _context;

        // required for DI 
        public RoomController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: RoomController
        [HttpGet]
        public IActionResult Index(int hotelId)
        {
            var rooms = _context.Rooms
            .Where(h => h.HotelId == hotelId)
            .ToList();
            if (rooms == null) return NotFound();
            ViewBag.HotelId = hotelId;
            ViewBag.HotelName = _context.Hotels.Find(hotelId).HotelName;
            return View(rooms);
        }

        // GET: RoomController/Details/5
        [HttpGet]
        public IActionResult Details(int roomId)
        {
            var room = _context.Rooms
                        .Include(r => r.Hotel)
                        .FirstOrDefault(r => r.RoomId == roomId);
            return View();
        }

        // GET: RoomController/Create
        [HttpGet]
        public IActionResult Create(int hotelId)
        {
            var hotel = _context.Hotels.Find(hotelId);
            if (hotel == null) return NotFound();
            
            var room = new Room { HotelId = hotelId };
            return View(room);
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name", "HotelId", "Hotel",
            "Capacity", "BedDescription", "PricePerNight", "RoomSize")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return RedirectToAction("Index", new { hotelId = room.HotelId });
            }
            ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
            return View(room);

        }

        // GET: RoomController/Edit/5
        [HttpGet]
        public IActionResult Edit(int roomId)
        {
            var room = _context.Rooms
                        .Include(r => r.Hotel)
                        .FirstOrDefault(r => r.RoomId == roomId);
            if (room == null) return NotFound();

            ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
            return View(room);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int roomId, [Bind("RoomId", "Name", "HotelId", "Hotel",
            "Capacity", "BedDescription", "PricePerNight", "RoomSize")] Room room)
        {
            if (roomId != room.RoomId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Rooms.Update(room);
                _context.SaveChanges();
                return RedirectToAction("Index", new { hotelId = room.HotelId });
            }
            ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
            return View();
        }

        // GET: RoomController/Delete/5
        [HttpGet]
        public IActionResult Delete(int roomId)
        {
            var room = _context.Rooms
                        .Include(r => r.Hotel)
                        .FirstOrDefault(r => r.RoomId == roomId);
            if (room == null) return NotFound();
            return View(room);
        }

        // POST: RoomController/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int roomId)
        {
            var room = _context.Rooms.Find(roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
                return RedirectToAction("Index", new { hotelId = room.HotelId });
            }
            return NotFound();
        }
    }
}
