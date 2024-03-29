﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Controllers
{
    public class DashboardController : Controller
    {
        // required
        private readonly ApplicationDbContext _context;

        // required for DI 
        public DashboardController(ApplicationDbContext context)
        {
            this._context = context;
        }
        // GET: HotelController
        [HttpGet]
        public async Task<IActionResult> HotelIndex()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return View(hotels);
        }

        // ========================== HOTEL ============================
        // GET: HotelController/Details/5
        [HttpGet]
        public async Task <IActionResult> HotelDetails(int id)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // GET: HotelController/Create
        [HttpGet]
        public IActionResult HotelCreate()
        {
            return View();
        }

        // POST: HotelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HotelCreate(Hotel newHotel)
        {
            if (ModelState.IsValid)
            {
                await _context.Hotels.AddAsync(newHotel);
                await _context.SaveChangesAsync();
                return RedirectToAction("HotelIndex");
            }
            return View(newHotel);
        }

        // GET: HotelController/Edit/5
        public async Task<IActionResult> HotelEdit(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // POST: HotelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HotelEdit(int id, [Bind("HotelId", "HotelName", "Location", "Description", "Amenities")] Hotel hotel)
        {
            if (id != hotel.HotelId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Hotels.Update(hotel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("HotelIndex");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await HotelExists(id)) return NotFound();
                    else throw;
                }
            }
            return View();
        }

        // GET: HotelController/Delete/5
        public async Task<IActionResult> HotelDelete(int id)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // POST: HotelController/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Remove(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction("HotelIndex");
            }
            return NotFound();
        }

        public async Task<bool> HotelExists(int id)
        {
            return await _context.Hotels.AnyAsync(h => h.HotelId == id);
        }

		// ========================== Room ============================

		[HttpGet]
		public async Task<IActionResult> RoomIndex(int hotelId)
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
		public async Task<IActionResult> RoomDetails(int id)
		{
			var room = await _context.Rooms
						.Include(r => r.Hotel)
						.FirstOrDefaultAsync(r => r.RoomId == id);
			return View(room);
		}

		// GET: RoomController/Create
		[HttpGet]
		public async Task<IActionResult> RoomCreate(int hotelId)
		{
			var hotel = await _context.Hotels.FindAsync(hotelId);
			if (hotel == null) return NotFound();
			ViewBag.HotelName = _context.Hotels.Find(hotelId)?.HotelName;

			var room = new Room { HotelId = hotelId };
			return View(room);
		}

		// POST: RoomController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RoomCreate([Bind("Name", "Capacity", "BedDescription", "RoomSize",
			 "PricePerNight", "Amenities", "HotelId")] Room room)
		{
			if (ModelState.IsValid)
			{
				await _context.Rooms.AddAsync(room);
				await _context.SaveChangesAsync();
				return RedirectToAction("RoomIndex", new { hotelId = room.HotelId });
			}
			ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
			return View(room);

		}

		// GET: RoomController/Edit/5
		[HttpGet]
		public async Task<IActionResult> RoomEdit(int id)
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
		public async Task<IActionResult> RoomEdit(int id, [Bind("RoomId", "Name", "Capacity", "BedDescription", "RoomSize",
			 "PricePerNight", "Amenities", "HotelId")] Room room)
		{
			if (id != room.RoomId) return NotFound();

			if (ModelState.IsValid)
			{
				_context.Rooms.Update(room);
				await _context.SaveChangesAsync();
				return RedirectToAction("RoomIndex", new { hotelId = room.HotelId });
			}
			ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
			return View();
		}

		// GET: RoomController/Delete/5
		[HttpGet]
		public async Task<IActionResult> RoomDelete(int id)
		{
			var room = await _context.Rooms
						.Include(r => r.Hotel)
						.FirstOrDefaultAsync(r => r.RoomId == id);
			if (room == null) return NotFound();
			return View(room);
		}

		// POST: RoomController/DeleteConfirmed/5
		[HttpPost, ActionName("RoomDeleteConfirmed")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RoomDeleteConfirmed(int id)
		{
			var room = await _context.Rooms.FindAsync(id);
			if (room != null)
			{
				_context.Rooms.Remove(room);
				await _context.SaveChangesAsync();
				return RedirectToAction("RoomIndex", new { hotelId = room.HotelId });
			}
			return NotFound();
		}

		// ========================== CAR ============================
		[HttpGet]
        public async Task<IActionResult> CarIndex()
        {
            var cars = await _context.Cars
                        .Include(cars => cars.Company)
                        .ToListAsync();
            if (cars == null) return NotFound();
            return View(cars);
        }


        [HttpGet]
        public async Task<IActionResult> CarDetails(int carId)
        {
            var car = await _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult> CarCreate()
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
        public async Task<IActionResult> CarCreate([Bind("Make", "Model", "Type", "PricePerDay", "MaxPassengers",
            "CompanyId", "Company", "Transmission", "HasAirConditioning", "HasUnlimitedMileage")] Car car)
        {
            if (ModelState.IsValid)
            {
                CarRentalCompany? company = await _context.CarRentalCompanies.FirstOrDefaultAsync(cr => cr.CarRentalCompanyId == car.CompanyId);
                Car newCar = new Car
                {
                    Make = car.Make,
                    Model = car.Model,
                    Type = car.Type,
                    PricePerDay = car.PricePerDay,
                    MaxPassengers = car.MaxPassengers,
                    Transmission = car.Transmission,
                    HasAirConditioning = car.HasAirConditioning,
                    HasUnlimitedMileage = car.HasUnlimitedMileage,
                    CompanyId = car.CompanyId,
                    Company = company
                };
                _context.Cars.Add(newCar);
                await _context.SaveChangesAsync();
                return RedirectToAction("CarIndex");
            }
            ViewBag.Companies = new SelectList(_context.CarRentalCompanies, "CarRentalCompanyId", "CompanyName", car.CarId);
            return View(car);

        }

        [HttpGet]
        public async Task<IActionResult> CarEdit(int carId)
        {
            var car = await _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null) return NotFound();

            ViewBag.CompanyList = new SelectList(_context.CarRentalCompanies, "CarRentalCompanyId", "CompanyName", car.CarId);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarEdit(int carId, [Bind("CarId", "Make", "Model", "Type", "PricePerDay", "MaxPassengers",
            "CompanyId", "Company", "Transmission", "HasAirConditioning", "HasUnlimitedMileage")] Car car)
        {
            if (carId != car.CarId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("CarIndex");
            }
            ViewBag.CompanyList = new SelectList(_context.CarRentalCompanies, "CompanyId", "CompanyName");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CarDelete(int carId)
        {
            var car = await _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null) return NotFound();
            return View(car);
        }

        // POST: CarController/DeleteConfirmed/5
        [HttpPost, ActionName("CarDeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarDeleteConfirmed(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("CarIndex");
            }
            return NotFound();
        }

		// ================================== Flight ===========================================

		public async Task<IActionResult> FlightIndex()
		{
			var flights = await _context.Flights.ToListAsync();
			return View(flights);
		}
		[HttpGet]
		public IActionResult FlightCreate()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> FlightCreate(Flight newFlight)
		{
			if (ModelState.IsValid)
			{
				await _context.Flights.FindAsync(newFlight);
				await _context.SaveChangesAsync();
				return RedirectToAction("FlightIndex");
			}
			return View(newFlight);
		}
		[HttpGet]
		public async Task<IActionResult> FlightDetails(int flightId)
		{
			var flight = await _context.Flights.FindAsync(flightId);

			return View(flight);

		}
		[HttpGet]
		public async Task<IActionResult> FlightEdit(int flightId)

		{
			var flight = await _context.Flights.FindAsync(flightId);
			return View(flight);


		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> FlightEdit(int id, [Bind("FlightId", "Airline", "Price", "MaxPassenger", "From", "To", "DepartTime", "ArrivalTime")] Flight flight)
		{

			if (id != flight.FlightId)
			{
				System.Diagnostics.Debug.WriteLine(id);
				System.Diagnostics.Debug.WriteLine(flight.FlightId);
				Console.WriteLine(flight.FlightId);
				// return RedirectToAction("Index", "Flight", id, flight.FlightId);
				return RedirectToAction("Index");

			}
			if (ModelState.IsValid)
			{
				_context.Flights.Update(flight);
				await _context.SaveChangesAsync();
				return RedirectToAction("FlightIndex");


			}

			return View(flight);

		}

		public async Task<IActionResult> FlightDelete(int flightId)

		{
			var flight = await _context.Flights.FindAsync(flightId);
			if (flight == null) return NotFound();
			return View(flight);


		}
		[HttpPost, ActionName("FlightDeleteConfirmed")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> FlightDeleteConfirmed(int flightId)
		{
			var flight = await _context.Flights.FindAsync(flightId);
			if (flight != null)
			{
				_context.Remove(flight);
				_context.SaveChanges();
				return RedirectToAction("FlightIndex");
			}
			return NotFound();
		}

		public async Task<bool> FlightExists(int id)
		{

			return await _context.Flights.AnyAsync(e => e.FlightId == id);
		}


	}
}
