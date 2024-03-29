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
        public async Task<IActionResult> Index(int hotelId)
        {
            var rooms = await _context.Rooms
                        .Where(h => h.HotelId == hotelId)
                        .ToListAsync();
            if (rooms == null) return NotFound();
            ViewBag.HotelId = hotelId;
            ViewBag.HotelName = _context.Hotels.Find(hotelId)?.HotelName;
            return View(rooms);
        }

        // GET: RoomController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var room = await _context.Rooms
                        .Include(r => r.Hotel)
                        .FirstOrDefaultAsync(r => r.RoomId == id);
            return View(room);
        }

        // GET: RoomController/Create
        [HttpGet]
        public async Task<IActionResult> Create(int hotelId)
        {
            var hotel = await _context.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.HotelId == hotelId);
            if (hotel == null) return NotFound();
            ViewBag.HotelName = hotel.HotelName;

            var room = new Room { HotelId = hotelId, Hotel = hotel };
            return View(room);

        }
        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name", "Capacity", "BedDescription", "RoomSize",
             "PricePerNight", "Amenities", "HotelId")] Room room)
        {
            if (ModelState.IsValid)
            {
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { hotelId = room.HotelId });
            }
            ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
            return View(room);
        }

        // GET: RoomController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _context.Rooms
                        .Include(r => r.Hotel)
                        .FirstOrDefaultAsync(r => r.RoomId == id);
            if (room == null) return NotFound();

            ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
            return View(room);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId", "Name", "Capacity", "BedDescription", "RoomSize",
             "PricePerNight", "Amenities", "HotelId")] Room room)
        {
            if (id != room.RoomId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { hotelId = room.HotelId });
            }
            ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
            return View();
        }

        // GET: RoomController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms
                        .Include(r => r.Hotel)
                        .FirstOrDefaultAsync(r => r.RoomId == id);
            if (room == null) return NotFound();
            return View(room);
        }

        // POST: RoomController/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { hotelId = room.HotelId });
            }
            return NotFound();
        }

        // search by location and capacity (search bar same for hotel and room)
        public async Task<IActionResult> Search(int hotelId, int capacity, DateTime checkInDate, DateTime checkOutDate)
        {
            var roomQuery = from p in _context.Rooms select p;

            bool searchValid = hotelId >= 0 && capacity >= 0;
            if (!searchValid)
                return RedirectToAction("Index");

            // find room of given Hotel and capacity
            roomQuery = roomQuery.Where(r => r.HotelId == hotelId)
                            .Where(r => r.Capacity >= capacity);
            // find room with no bookings on given start / end dates
            roomQuery = roomQuery.Where(r => !r.RoomBookings.Any(rb => checkOutDate >= rb.CheckInDate && checkInDate <= rb.CheckOutDate));
            var rooms = await roomQuery.ToListAsync();

            // Passing view information via ViewBag
            var hotel = _context.Hotels.Find(hotelId);
            ViewBag.SearchValid = searchValid;
            ViewBag.Capacity = capacity;
            ViewBag.CheckInDate = checkInDate;
            ViewBag.CheckOUtDate = checkOutDate;
            ViewBag.HotelId = hotelId;
            ViewBag.HotelName = hotel?.HotelName;
            return View("Index", rooms);
        }


    }


}
