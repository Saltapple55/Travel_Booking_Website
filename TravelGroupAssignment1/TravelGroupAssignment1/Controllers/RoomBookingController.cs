using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Controllers
{
    public class RoomBookingController : Controller
    {
        // required
        private readonly ApplicationDbContext _context;

        // required for DI 
        public RoomBookingController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: RoomBookingController/5
        [HttpGet]
        public IActionResult Index(int roomId)
        {
            var roomBookings = _context.RoomBookings
                                .Include(rb => rb.Room)
                                .Where(rb => rb.RoomId == roomId)
                                .ToList();
            
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            if(room == null) return NotFound();
            ViewBag.RoomName = room.Name;
            ViewBag.RoomId = room.RoomId;
            ViewBag.HotelId = room.HotelId;
            return View(roomBookings);
        }

        // GET: RoomBookingControllers/Details/5
        [HttpGet]
        public IActionResult Details(int id, string? con = "RoomBooking")
        {
            var booking = _context.RoomBookings
                        .Include(rb => rb.Room)
                        .FirstOrDefault(rb => rb.BookingId == id);
            if (booking == null) return NotFound();
            var room = _context.Rooms.Find(booking.RoomId);
            if (room == null) return NotFound();
            var hotel = _context.Hotels.Find(room.HotelId);
            if (hotel == null) return NotFound();
            ViewBag.Hotel = hotel;
            ViewBag.Controller = con;
            return View(booking);
        }

        // GET: RoomBookingController/Create/5
        [HttpGet]
        public IActionResult Create(int roomId, DateTime? checkInDate, DateTime? checkOutDate)
        {
            var room = _context.Rooms.Find(roomId);
            if (room == null) return NotFound();
            var hotel = _context.Hotels.Find(room.HotelId);
            if (hotel == null) return NotFound();

            ViewBag.Room = room;
            ViewBag.Hotel = hotel;
            
            ViewBag.CheckInDate = checkInDate;
            ViewBag.CheckOutDate = checkOutDate;

            return View(new RoomBooking { RoomId = roomId, TripId=1});
        }

        // POST: RoomBookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TripId", "BookingReference",
            "RoomId", "Room", "CheckInDate", "CheckOutDate")] RoomBooking roomBooking)
        {
            // information needed if booking not successfull created
            var room = _context.Rooms.Find(roomBooking.RoomId);
            if (room == null) return NotFound();
            var hotel = _context.Hotels.Find(room.HotelId);
            if (hotel == null) return NotFound();
            ViewBag.Room = room;
            ViewBag.Hotel = hotel;

            if (ModelState.IsValid)
            {
                // check if room is already booked on given dates
                if (roomBookingExists(roomBooking))
                {
                    ModelState.AddModelError("", "Room is not available for booking on given date range.");
                    return View(roomBooking);
                }

                _context.RoomBookings.Add(roomBooking);
                _context.SaveChanges();
                return RedirectToAction("Index", "Trip");
            }

            //return RedirectToAction("Create", new { roomId = roomBooking.RoomId });
            return View(roomBooking);
        }

        // GET: RoomBookingController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var roomBooking = _context.RoomBookings
                            .Include(rb => rb.Room)
                            .FirstOrDefault(cb => cb.BookingId == id);
            if (roomBooking == null) return NotFound();
            var room = _context.Rooms.Find(roomBooking.RoomId);
            if (room == null) return NotFound();
            var hotel = _context.Hotels.Find(room.HotelId);
            if (hotel == null) return NotFound();
            if (roomBooking == null) return NotFound();
            ViewBag.Room = room;
            ViewBag.Hotel = hotel;
            return View(roomBooking);
        }

        // POST: RoomBookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookingId", "TripId", "BookingReference",
            "RoomId", "Room", "CheckInDate", "CheckOutDate")] RoomBooking roomBooking)
        {
            if (id != roomBooking.BookingId) return NotFound();

            // info to render view if failure
            var room = _context.Rooms.Find(roomBooking.RoomId);
            var hotel = _context.Hotels.Find(room.HotelId);
            if (roomBooking == null) return NotFound();
            ViewBag.Room = room;
            ViewBag.Hotel = hotel;

            if (ModelState.IsValid)
            {
                if (roomBookingExists(roomBooking))
                {
                    ModelState.AddModelError("", "Room is not available for booking on given date range.");
                    return View(roomBooking);
                }
                _context.RoomBookings.Update(roomBooking);
                _context.SaveChanges();
                return RedirectToAction("Index", new { roomId = roomBooking.RoomId });
            }
            return View(roomBooking);
        }

        // GET: RoomBookingController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id, string? con="RoomBooking")
        {
            var booking = _context.RoomBookings
                        .Include(rb => rb.Room)
                        .FirstOrDefault(rb => rb.BookingId == id);
            if (booking == null) return NotFound();
            var room = _context.Rooms.Find(booking.RoomId);
            if (room == null) return NotFound();
            var hotel = _context.Hotels.Find(room.HotelId);
            if (hotel == null) return NotFound();
            ViewBag.Hotel = hotel;
            ViewBag.Controller = con;

            return View(booking);
        }

        // POST: RoomBookingController/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string? con="RoomBooking")
        {
            var roomBooking = _context.RoomBookings.Find(id);
            if (roomBooking != null)
            {
                _context.RoomBookings.Remove(roomBooking);
                _context.SaveChanges();
                if(string.Equals(con, "RoomBooking"))
                return RedirectToAction("Index", new { roomId = roomBooking.RoomId });

                return RedirectToAction("Index", con);
            }
            return NotFound();
        }

        public bool roomBookingExists(RoomBooking roomBooking)
        {
            var roomBookingQuery = from p in _context.RoomBookings
                                   select p;
            roomBookingQuery = roomBookingQuery.Where(r => r.RoomId == roomBooking.RoomId)
                                            .Where(r => r.CheckInDate >= roomBooking.CheckInDate && r.CheckInDate <= roomBooking.CheckOutDate ||
                                            r.CheckOutDate >= roomBooking.CheckInDate && r.CheckOutDate <= roomBooking.CheckOutDate);
            var existingRoomBookings = roomBookingQuery.ToList();
            return existingRoomBookings.Count > 0;
        }
    }
}
