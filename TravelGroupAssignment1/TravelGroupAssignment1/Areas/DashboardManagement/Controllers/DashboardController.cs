using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;

namespace TravelGroupAssignment1.Areas.DashboardManagement.Controllers
{
    [Area("DashboardManagement")]
    [Route("[area]/[controller]")]
    public class DashboardController : Controller
    {
        // required
        private readonly ApplicationDbContext _context;

        // required for DI 
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: HotelController
        [HttpGet("HotelIndex")]
        public async Task<IActionResult> HotelIndex()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return View(hotels);
        }

        // ========================== HOTEL ============================
        // GET: HotelController/Details/5
        [HttpGet("HotelDetails/{id:int}")]
        public async Task<IActionResult> HotelDetails(int id)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // GET: HotelController/Create
        [HttpGet("HotelCreate")]
        public IActionResult HotelCreate()
        {
            return View();
        }

        // POST: HotelController/Create
        [HttpPost("HotelCreate")]
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
        [HttpGet("HotelEdit/{id:int}")]
        public async Task<IActionResult> HotelEdit(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // POST: HotelController/Edit/5
        [HttpPost("HotelEdit/{id:int}")]
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
        [HttpGet("HotelDelete/{id:int}")]
        public async Task<IActionResult> HotelDelete(int id)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }

        // POST: HotelController/DeleteConfirmed/5
        [HttpPost("DeleteConfirmed/{id:int}"), ActionName("DeleteConfirmed")]
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
        [HttpGet("HotelSearch")]
        public async Task<IActionResult> HotelSearch(string location, int capacity, DateTime checkInDate, DateTime checkOutDate)
        {
            var hotelQuery = from p in _context.Hotels
                             select p;
            bool searchValid = !String.IsNullOrEmpty(location) && capacity > 0;
            if (searchValid)
            {
                hotelQuery = hotelQuery.Where(h => !String.IsNullOrEmpty(h.Location) && h.Location.Contains(location)
                                        || !String.IsNullOrEmpty(h.Description) && h.Description.Contains(location));
                //hotelQuery = hotelQuery.Where(h => h.Rooms != null && h.Rooms.Any(r => r.Capacity >= capacity));
                //hotelQuery = hotelQuery.Where(h => h.Rooms != null && h.Rooms.Any(r => r.Capacity >= capacity && (!r.RoomBookings.Any(rb => rb.CheckInDate >= checkInDate && rb.CheckInDate <= checkOutDate
                //                        || rb.CheckOutDate >= checkInDate && rb.CheckOutDate <= checkOutDate || rb.CheckInDate <= checkInDate && rb.CheckOutDate >= checkOutDate))));
                hotelQuery = hotelQuery.Where(h => h.Rooms != null && h.Rooms.Any(r => r.Capacity >= capacity && !r.RoomBookings.Any(rb => checkOutDate >= rb.CheckInDate && checkInDate <= rb.CheckOutDate)));

            }
            else
            {
                return RedirectToAction("HotelIndex");
            }
            var hotels = await hotelQuery.ToListAsync();
            ViewBag.SearchValid = searchValid;
            ViewBag.Location = location;
            ViewBag.Capacity = capacity;
            ViewBag.CheckInDate = checkInDate;
            ViewBag.CheckOutDate = checkOutDate;
            return View("HotelIndex", hotels);
        }

        public async Task<bool> HotelExists(int id)
        {
            return await _context.Hotels.AnyAsync(h => h.HotelId == id);
        }

        // ========================== Room ============================

        [HttpGet("RoomIndex/{hotelId:int}")]
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
        [HttpGet("RoomDetails/{id:int}")]
        public async Task<IActionResult> RoomDetails(int id)
        {
            var room = await _context.Rooms
                        .Include(r => r.Hotel)
                        .FirstOrDefaultAsync(r => r.RoomId == id);
            return View(room);
        }

        // GET: RoomController/Create
        [HttpGet("RoomCreate/{hotelId:int}")]
        public async Task<IActionResult> RoomCreate(int hotelId)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel == null) return NotFound();
            ViewBag.HotelName = _context.Hotels.Find(hotelId)?.HotelName;

            var room = new Room { HotelId = hotelId };
            return View(room);
        }

        // POST: RoomController/Create
        [HttpPost("RoomCreate")]
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
        [HttpGet("RoomEdit/{id:int}")]
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
        [HttpPost("RoomEdit/{id:int}")]
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
        [HttpGet("RoomDelete/{id:int}")]
        public async Task<IActionResult> RoomDelete(int id)
        {
            var room = await _context.Rooms
                        .Include(r => r.Hotel)
                        .FirstOrDefaultAsync(r => r.RoomId == id);
            if (room == null) return NotFound();
            return View(room);
        }

        // POST: RoomController/DeleteConfirmed/5
        [HttpPost("RoomDeleteConfirmed/{id:int}"), ActionName("RoomDeleteConfirmed")]
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
        [HttpGet("CarIndex")]
        public async Task<IActionResult> CarIndex()
        {
            var cars = await _context.Cars
                        .Include(cars => cars.Company)
                        .ToListAsync();
            if (cars == null) return NotFound();
            return View(cars);
        }


        [HttpGet("CarDetails/{carId:int}")]
        public async Task<IActionResult> CarDetails(int carId)
        {
            var car = await _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpGet("CarCreate")]
        public async Task<IActionResult> CarCreate()
        {
            ViewBag.Companies = new SelectList(_context.CarRentalCompanies, "CarRentalCompanyId", "CompanyName");
            var companyObjects = await _context.CarRentalCompanies
                                        .ToDictionaryAsync(c => c.CarRentalCompanyId, c => c);
            ViewBag.CompanyObjects = companyObjects;
            return View();
        }

        // POST: CarController/Create
        [HttpPost("CarCreate")]
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

        [HttpGet("CarEdit/{carId:int}")]
        public async Task<IActionResult> CarEdit(int carId)
        {
            var car = await _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null) return NotFound();

            ViewBag.CompanyList = new SelectList(_context.CarRentalCompanies, "CarRentalCompanyId", "CompanyName", car.CarId);
            return View(car);
        }

        [HttpPost("CarEdit/{carId:int}")]
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

        [HttpGet("CarDelete/{carId:int}")]
        public async Task<IActionResult> CarDelete(int carId)
        {
            var car = await _context.Cars
                        .Include(c => c.Company)
                        .FirstOrDefaultAsync(c => c.CarId == carId);
            if (car == null) return NotFound();
            return View(car);
        }

        // POST: CarController/DeleteConfirmed/5
        [HttpPost("CarDeleteConfirmed/{carId:int}"), ActionName("CarDeleteConfirmed")]
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

        [HttpGet("CarSearch")]
		public async Task<IActionResult> CarSearch(string location, DateTime startDate, DateTime endDate)
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
				return RedirectToAction("CarIndex");
			}
			var cars = await carQuery.Include(c => c.Company).ToListAsync();
			ViewBag.SearchValid = searchValid;
			ViewBag.Location = location;
			ViewBag.StartDate = startDate;
			ViewBag.EndDate = endDate;
			return View("CarIndex", cars);
		}

		// ================================== Flight ===========================================

		[HttpGet("FlightIndex")]
        public async Task<IActionResult> FlightIndex()
        {
            var flights = await _context.Flights.ToListAsync();
            return View(flights);
        }
        [HttpGet("FlightCreate")]
        public IActionResult FlightCreate()
        {
            return View();
        }
        [HttpPost("FlightCreate")]
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
        [HttpGet("FlightDetails/{flightId:int}")]
        public async Task<IActionResult> FlightDetails(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);

            return View(flight);

        }
        [HttpGet("FlightEdit/{flightId:int}")]
        public async Task<IActionResult> FlightEdit(int flightId)

        {
            var flight = await _context.Flights.FindAsync(flightId);
            return View(flight);


        }
        [HttpPost("FlightEdit/{id:int}")]
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

        [HttpGet("FlightDelete/{flightId:int}")]
        public async Task<IActionResult> FlightDelete(int flightId)

        {
            var flight = await _context.Flights.FindAsync(flightId);
            if (flight == null) return NotFound();
            return View(flight);
        }

        [HttpPost("FlightDeleteConfirmed/{flightId:int}"), ActionName("FlightDeleteConfirmed")]
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


        [HttpGet("FlightSearch")]
		public async Task<IActionResult> FlightSearch(string locationFrom, string location, int capacity, DateTime startDate)
		{
			var flightQuery = from p in _context.Flights
							  select p;

			bool searchValid = !String.IsNullOrEmpty(location) && capacity > 0;
			if (searchValid)
			{
				flightQuery = flightQuery.Where(f => f.From.Contains(locationFrom) && f.To.Contains(location));
				// I changed line below, not sure if I fixed it or not
				flightQuery = flightQuery.Where(f => f.DepartTime.Date >= startDate.Date);

			}
			else
			{
				return RedirectToAction("FlightIndex");
			}
			var flights = await flightQuery.ToListAsync();
			ViewBag.SearchValid = searchValid;
			ViewBag.Location = location;
			ViewBag.Capacity = capacity;
			ViewBag.StartDate = startDate;
			return View("FlightIndex", flights);
		}

	}
}
