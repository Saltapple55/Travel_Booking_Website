using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
                        .Where(rb => rb.Rooms.Any(room => room.RoomId == roomId))
                        .ToList();
            if (roomBookings == null) return NotFound();
            var room = _context.Rooms.Find(roomId);
            if (room == null) return NotFound();

            ViewBag.RoomName = room.Name;
            ViewBag.RoomId = roomId;
            return View(roomBookings);
        }

        // GET: CarBookingController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var booking = _context.RoomBookings
                        .Include(rb => rb.Rooms)
                        .FirstOrDefault(rb => rb.BookingId == id);
            return View(booking);
        }

        // GET: CarBookingController/Create/5
        [HttpGet]
        public IActionResult Create(int roomId)
        {
            var room = _context.Rooms.Find(roomId);
            if (room == null) return NotFound();
            ViewBag.RoomName = room.Name;
            ViewBag.RoomId = roomId;

            //var roomBooking = new Room_RoomBooking { };
            return View();
        }

        // POST: CarBookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TripId", "BookingReference",
            "CarId", "Car", "StartDate", "EndDate")] CarBooking carBooking)
        {
            if (ModelState.IsValid)
            {
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
                return RedirectToAction("Index", new { carId = carBooking.CarId });
            }
            return View();
        }

        // GET: CarBookingController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var carBooking = _context.CarBookings
                        .Include(cb => cb.Car)
                        .FirstOrDefault(cb => cb.BookingId == id);
            if (carBooking == null) return NotFound();
            return View(carBooking);
        }

        // POST: CarBookingController/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var carBooking = _context.CarBookings.Find(id);
            if (carBooking != null)
            {
                _context.CarBookings.Remove(carBooking);
                _context.SaveChanges();
                return RedirectToAction("Index", new { carId = carBooking.CarId });
            }
            return NotFound();
        }
    }
}
