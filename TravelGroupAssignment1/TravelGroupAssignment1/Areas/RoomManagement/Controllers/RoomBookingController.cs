using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
using TravelGroupAssignment1.Areas.RoomManagement.Models;
using TravelGroupAssignment1.Data;

namespace TravelGroupAssignment1.Areas.RoomManagement.Controllers
{
    public class RoomBookingController : Controller
    {
        // required
        private readonly ApplicationDbContext _context;

        // required for DI 
        public RoomBookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomBookingController/5
        [HttpGet]
        public async Task<IActionResult> Index(int roomId)
        {
            var roomBookings = await _context.RoomBookings
                                .Include(rb => rb.Room)
                                .Where(rb => rb.RoomId == roomId)
                                .ToListAsync();

            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);
            if (room == null) return NotFound();
            ViewBag.RoomName = room.Name;
            ViewBag.RoomId = room.RoomId;
            ViewBag.HotelId = room.HotelId;
            return View(roomBookings);
        }

        // GET: RoomBookingControllers/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id, string? con = "RoomBooking")
        {
            var booking = await _context.RoomBookings
                        .Include(rb => rb.Room)
                        .FirstOrDefaultAsync(rb => rb.BookingId == id);
            if (booking == null) return NotFound();
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room == null) return NotFound();
            var hotel = await _context.Hotels.FindAsync(room.HotelId);
            if (hotel == null) return NotFound();
            ViewBag.Hotel = hotel;
            ViewBag.Controller = con;
            return View(booking);
        }

        // GET: RoomBookingController/Create/5
        [HttpGet]
        public async Task<IActionResult> Create(int roomId, DateTime? checkInDate, DateTime? checkOutDate)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null) return NotFound();
            var hotel = await _context.Hotels.FindAsync(room.HotelId);
            if (hotel == null) return NotFound();

            ViewBag.Room = room;
            ViewBag.Hotel = hotel;

            ViewBag.CheckInDate = checkInDate;
            ViewBag.CheckOutDate = checkOutDate;

            return View(new RoomBooking { RoomId = roomId, TripId = 1 });
        }

        // POST: RoomBookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripId", "BookingReference",
            "RoomId", "Room", "CheckInDate", "CheckOutDate")] RoomBooking roomBooking)
        {
            // information needed if booking not successfull created
            var room = await _context.Rooms.FindAsync(roomBooking.RoomId);
            if (room == null) return NotFound();
            var hotel = await _context.Hotels.FindAsync(room.HotelId);
            if (hotel == null) return NotFound();
            ViewBag.Room = room;
            ViewBag.Hotel = hotel;

            if (ModelState.IsValid)
            {
                // check if room is already booked on given dates
                if (await roomBookingExists(roomBooking))
                {
                    ModelState.AddModelError("", "Room is not available for booking on given date range.");
                    return View(roomBooking);
                }

                await _context.RoomBookings.AddAsync(roomBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Trip");
            }

            //return RedirectToAction("Create", new { roomId = roomBooking.RoomId });
            return View(roomBooking);
        }

        // GET: RoomBookingController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var roomBooking = await _context.RoomBookings
                            .Include(rb => rb.Room)
                            .FirstOrDefaultAsync(cb => cb.BookingId == id);
            if (roomBooking == null) return NotFound();
            var room = await _context.Rooms.FindAsync(roomBooking.RoomId);
            if (room == null) return NotFound();
            var hotel = await _context.Hotels.FindAsync(room.HotelId);
            if (hotel == null) return NotFound();
            if (roomBooking == null) return NotFound();
            ViewBag.Room = room;
            ViewBag.Hotel = hotel;
            return View(roomBooking);
        }

        // POST: RoomBookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId", "TripId", "BookingReference",
            "RoomId", "Room", "CheckInDate", "CheckOutDate")] RoomBooking roomBooking)
        {
            if (id != roomBooking.BookingId) return NotFound();

            // info to render view if failure
            var room = await _context.Rooms.FindAsync(roomBooking.RoomId);
            var hotel = await _context.Hotels.FindAsync(room.HotelId);
            if (roomBooking == null) return NotFound();
            ViewBag.Room = room;
            ViewBag.Hotel = hotel;

            if (ModelState.IsValid)
            {
                if (await roomBookingExists(roomBooking))
                {
                    ModelState.AddModelError("", "Room is not available for booking on given date range.");
                    return View(roomBooking);
                }
                _context.RoomBookings.Update(roomBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { roomId = roomBooking.RoomId });
            }
            return View(roomBooking);
        }

        // GET: RoomBookingController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id, string? con = "RoomBooking")
        {
            var booking = await _context.RoomBookings
                        .Include(rb => rb.Room)
                        .FirstOrDefaultAsync(rb => rb.BookingId == id);
            if (booking == null) return NotFound();
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room == null) return NotFound();
            var hotel = await _context.Hotels.FindAsync(room.HotelId);
            if (hotel == null) return NotFound();
            ViewBag.Hotel = hotel;
            ViewBag.Controller = con;

            return View(booking);
        }

        // POST: RoomBookingController/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string? con = "RoomBooking")
        {
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking != null)
            {
                _context.RoomBookings.Remove(roomBooking);
                await _context.SaveChangesAsync();
                if (string.Equals(con, "RoomBooking"))
                    return RedirectToAction("Index", con);
                return RedirectToAction("Index", new { roomId = roomBooking.RoomId });
            }
            return NotFound();
        }

        public async Task<bool> roomBookingExists(RoomBooking roomBooking)
        {
            var roomBookingQuery = from p in _context.RoomBookings
                                   select p;
            roomBookingQuery = roomBookingQuery.Where(r => r.RoomId == roomBooking.RoomId)
                                            .Where(r => r.CheckInDate >= roomBooking.CheckInDate && r.CheckInDate <= roomBooking.CheckOutDate ||
                                            r.CheckOutDate >= roomBooking.CheckInDate && r.CheckOutDate <= roomBooking.CheckOutDate);
            var existingRoomBookings = await roomBookingQuery.ToListAsync();
            return existingRoomBookings.Count > 0;
        }
    }
}
