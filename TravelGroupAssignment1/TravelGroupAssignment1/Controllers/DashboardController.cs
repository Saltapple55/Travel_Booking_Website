using Microsoft.AspNetCore.Http;
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
        public IActionResult HotelIndex()
        {
            var hotels = _context.Hotels.ToList();
            return View(hotels);
        }

        // ========================== HOTEL ============================
        // GET: HotelController/Details/5
        [HttpGet]
        public IActionResult HotelDetails(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == id);
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
        public IActionResult HotelCreate(Hotel newHotel)
        {
            if (ModelState.IsValid)
            {
                _context.Hotels.Add(newHotel);
                _context.SaveChanges();
                return RedirectToAction("HotelIndex");
            }
            return View(newHotel);
        }

        // GET: HotelController/Edit/5
        public IActionResult HotelEdit(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // POST: HotelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HotelEdit(int id, [Bind("HotelId", "HotelName", "Location", "Description", "Amenities")] Hotel hotel)
        {
            if (id != hotel.HotelId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Hotels.Update(hotel);
                    _context.SaveChanges();
                    return RedirectToAction("HotelIndex");
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
        public IActionResult HotelDelete(int id)
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
                return RedirectToAction("HotelIndex");
            }
            return NotFound();
        }

        public bool HotelExists(int id)
        {
            return _context.Hotels.Any(h => h.HotelId == id);
        }

		// ========================== Room ============================

		[HttpGet]
		public IActionResult RoomIndex(int hotelId)
		{
			var rooms = _context.Rooms
						.Where(h => h.HotelId == hotelId)
						.ToList();
			if (rooms == null) return NotFound();
			ViewBag.HotelId = hotelId;
			ViewBag.HotelName = _context.Hotels.Find(hotelId)?.HotelName;
			return View(rooms);
		}

		// GET: RoomController/Details/5
		[HttpGet]
		public IActionResult RoomDetails(int id)
		{
			var room = _context.Rooms
						.Include(r => r.Hotel)
						.FirstOrDefault(r => r.RoomId == id);
			return View(room);
		}

		// GET: RoomController/Create
		[HttpGet]
		public IActionResult RoomCreate(int hotelId)
		{
			var hotel = _context.Hotels.Find(hotelId);
			if (hotel == null) return NotFound();
			ViewBag.HotelName = _context.Hotels.Find(hotelId)?.HotelName;

			var room = new Room { HotelId = hotelId };
			return View(room);
		}

		// POST: RoomController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult RoomCreate([Bind("Name", "Capacity", "BedDescription", "RoomSize",
			 "PricePerNight", "Amenities", "HotelId")] Room room)
		{
			if (ModelState.IsValid)
			{
				_context.Rooms.Add(room);
				_context.SaveChanges();
				return RedirectToAction("RoomIndex", new { hotelId = room.HotelId });
			}
			ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
			return View(room);

		}

		// GET: RoomController/Edit/5
		[HttpGet]
		public IActionResult RoomEdit(int id)
		{
			var room = _context.Rooms
						.Include(r => r.Hotel)
						.FirstOrDefault(r => r.RoomId == id);
			if (room == null) return NotFound();

			ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
			return View(room);
		}

		// POST: RoomController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult RoomEdit(int id, [Bind("RoomId", "Name", "Capacity", "BedDescription", "RoomSize",
			 "PricePerNight", "Amenities", "HotelId")] Room room)
		{
			if (id != room.RoomId) return NotFound();

			if (ModelState.IsValid)
			{
				_context.Rooms.Update(room);
				_context.SaveChanges();
				return RedirectToAction("RoomIndex", new { hotelId = room.HotelId });
			}
			ViewBag.HotelList = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
			return View();
		}

		// GET: RoomController/Delete/5
		[HttpGet]
		public IActionResult RoomDelete(int id)
		{
			var room = _context.Rooms
						.Include(r => r.Hotel)
						.FirstOrDefault(r => r.RoomId == id);
			if (room == null) return NotFound();
			return View(room);
		}

		// POST: RoomController/DeleteConfirmed/5
		[HttpPost, ActionName("RoomDeleteConfirmed")]
		[ValidateAntiForgeryToken]
		public IActionResult RoomDeleteConfirmed(int id)
		{
			var room = _context.Rooms.Find(id);
			if (room != null)
			{
				_context.Rooms.Remove(room);
				_context.SaveChanges();
				return RedirectToAction("RoomIndex", new { hotelId = room.HotelId });
			}
			return NotFound();
		}

		// ========================== CAR ============================
		[HttpGet]
        public IActionResult CarIndex()
        {
            var cars = _context.Cars
                        .Include(cars => cars.Company)
                        .ToList();
            if (cars == null) return NotFound();
            return View(cars);
        }


        [HttpGet]
        public IActionResult CarDetails(int carId)
        {
            var car = _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefault(c => c.CarId == carId);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpGet]
        public IActionResult CarCreate()
        {
            ViewBag.Companies = new SelectList(_context.CarRentalCompanies, "CarRentalCompanyId", "CompanyName");
            var companyObjects = _context.CarRentalCompanies
                                        .ToDictionary(c => c.CarRentalCompanyId, c => c);
            ViewBag.CompanyObjects = companyObjects;
            return View();
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CarCreate([Bind("Make", "Model", "Type", "PricePerDay", "MaxPassengers",
            "CompanyId", "Company", "Transmission", "HasAirConditioning", "HasUnlimitedMileage")] Car car)
        {
            if (ModelState.IsValid)
            {
                CarRentalCompany? company = _context.CarRentalCompanies.FirstOrDefault(cr => cr.CarRentalCompanyId == car.CompanyId);
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
                _context.SaveChanges();
                return RedirectToAction("CarIndex");
            }
            ViewBag.Companies = new SelectList(_context.CarRentalCompanies, "CarRentalCompanyId", "CompanyName", car.CarId);
            return View(car);

        }

        [HttpGet]
        public IActionResult CarEdit(int carId)
        {
            var car = _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefault(c => c.CarId == carId);
            if (car == null) return NotFound();

            ViewBag.CompanyList = new SelectList(_context.CarRentalCompanies, "CarRentalCompanyId", "CompanyName", car.CarId);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CarEdit(int carId, [Bind("CarId", "Make", "Model", "Type", "PricePerDay", "MaxPassengers",
            "CompanyId", "Company", "Transmission", "HasAirConditioning", "HasUnlimitedMileage")] Car car)
        {
            if (carId != car.CarId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Cars.Update(car);
                _context.SaveChanges();
                return RedirectToAction("CarIndex");
            }
            ViewBag.CompanyList = new SelectList(_context.CarRentalCompanies, "CompanyId", "CompanyName");
            return View();
        }

        [HttpGet]
        public IActionResult CarDelete(int carId)
        {
            var car = _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefault(c => c.CarId == carId);
            if (car == null) return NotFound();
            return View(car);
        }

        // POST: CarController/DeleteConfirmed/5
        [HttpPost, ActionName("CarDeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult CarDeleteConfirmed(int carId)
        {
            var car = _context.Cars.Find(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
                return RedirectToAction("CarIndex");
            }
            return NotFound();
        }

		// ================================== Flight ===========================================

		public IActionResult FlightIndex()
		{
			var flights = _context.Flights.ToList();
			return View(flights);
		}
		[HttpGet]
		public IActionResult FlightCreate()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult FlightCreate(Flight newFlight)
		{
			if (ModelState.IsValid)
			{
				_context.Flights.Add(newFlight);
				_context.SaveChanges();
				return RedirectToAction("FlightIndex");
			}
			return View(newFlight);
		}
		[HttpGet]
		public IActionResult FlightDetails(int flightId)
		{
			var flight = _context.Flights.Find(flightId);

			return View(flight);

		}
		[HttpGet]
		public IActionResult FlightEdit(int flightId)

		{
			var flight = _context.Flights.Find(flightId);
			return View(flight);


		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult FlightEdit(int id, [Bind("FlightId", "Airline", "Price", "MaxPassenger", "From", "To", "DepartTime", "ArrivalTime")] Flight flight)
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
				_context.SaveChanges();
				return RedirectToAction("FlightIndex");


			}

			return View(flight);

		}

		public IActionResult FlightDelete(int flightId)

		{
			var flight = _context.Flights.Find(flightId);
			if (flight == null) return NotFound();
			return View(flight);


		}
		[HttpPost, ActionName("FlightDeleteConfirmed")]
		[ValidateAntiForgeryToken]
		public IActionResult FlightDeleteConfirmed(int flightId)
		{
			var flight = _context.Flights.Find(flightId);
			if (flight != null)
			{
				_context.Remove(flight);
				_context.SaveChanges();
				return RedirectToAction("FlightIndex");
			}
			return NotFound();
		}

		public bool FlightExists(int id)
		{

			return _context.Flights.Any(e => e.FlightId == id);
		}


	}
}
