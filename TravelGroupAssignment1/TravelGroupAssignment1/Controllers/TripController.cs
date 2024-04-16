using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Packaging.Signing;
using NuGet.Versioning;
using TravelGroupAssignment1.Areas.CarManagement.Models;
using TravelGroupAssignment1.Areas.CustomerManagement.Models;
using TravelGroupAssignment1.Areas.FlightManagement.Models;
using TravelGroupAssignment1.Areas.RoomManagement.Models;
using TravelGroupAssignment1.Data;
using TravelGroupAssignment1.Models;
using TravelGroupAssignment1.Services;

namespace TravelGroupAssignment1.Controllers
{
    public class TripController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISessionService _sessionService;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public TripController(ApplicationDbContext context, ISessionService sessionService, IEmailSender emailSender, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _sessionService = sessionService;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        // GET: Trip
        public async Task<IActionResult> Index()
        {
            List<int> ids = _sessionService.GetSessionData<List<int>>("BookingIds");
            List<FlightBooking> fbookings = new List<FlightBooking>();
            List<CarBooking> cbookings= new List<CarBooking>();
            List<RoomBooking> rbookings = new List<RoomBooking>();
            foreach (int id in ids)
            {
                var flightbooking = await _context.FlightBookings.Include(t => t.Flight).Include(p => p.Passengers).FirstOrDefaultAsync(booking => booking.BookingId == id);
                if(flightbooking != null)
                {
                    fbookings.Add(flightbooking);
                    continue;
                }
                var carbooking = await _context.CarBookings.Include(c => c.Car).FirstOrDefaultAsync(booking => booking.BookingId == id);
                if(carbooking != null)
                {
                    cbookings.Add(carbooking); continue;
                }
                var roombooking = await _context.RoomBookings.Include(r => r.Room).FirstOrDefaultAsync(booking => booking.BookingId == id);
                if (roombooking != null)
                {
                    rbookings.Add(roombooking); continue;
                }
                System.Diagnostics.Debug.WriteLine("This isn't supposed to happen ");
            }
            /*var fbookings = _context.FlightBookings.Include(f => f.Flight).Include(f => f.Passengers).ToList();
            var cbookings = _context.CarBookings.Include(c => c.Car).ToList();
            var rbookings = _context.RoomBookings.Include(r => r.Room).ToList();
*/
            BookingsViewModel bookings = new BookingsViewModel
            {
                flights= fbookings,
                cars= cbookings,
                rooms= rbookings
            };

            return View(bookings);
        }

       

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.TripId == id);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
/*            var fbookings = _context.FlightBookings.Include(f => f.Flight).Include(f => f.Passengers).ToList();
            var cbookings = _context.CarBookings.Include(c => c.Car).ToList();
            var rbooking = _context.RoomBookings.Include(r => r.Room).ToList();*/
            /*var trip = _context.Trips.Find(tripId);
            if (trip == null) return NotFound();*/
            return View();
            //var cust = _context.Customers.Find(trip.CustomerId);
            //if (cust == null) return NotFound();
 ;
        }
      
        [HttpPost]
        public async Task<IActionResult> Checkout( string email="", string name = "")
        {
            Trip trip = new Trip();


            if (ModelState.IsValid)
            {


                _context.Trips.Add(trip);
                _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine("Trip id: " + trip.TripId);
                System.Diagnostics.Debug.WriteLine(email);

                List<int> ids = _sessionService.GetSessionData<List<int>>("BookingIds");
               List<FlightBooking> fbookings = new List<FlightBooking>();
                List<CarBooking> cbookings = new List<CarBooking>();
                List<RoomBooking> rbookings = new List<RoomBooking>();
                /*var fbookings = _context.FlightBookings.Include(f => f.Flight).Include(f => f.Passengers).ToList();
                var cbookings = _context.CarBookings.Include(c => c.Car).ToList();
                var rbookings = _context.RoomBookings.Include(r => r.Room).ToList();

*/
                if ((User != null) && User.Identity.IsAuthenticated)
                {
                    var userId = _userManager.GetUserId(User);
                    
                        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                        email= user.Email;
                    
                    trip.ApplicationUserId = userId;
                    

                }
               
                    foreach (int id in ids)
                    {
                        var flightbooking = await _context.FlightBookings.Include(t => t.Flight).Include(p => p.Passengers).FirstOrDefaultAsync(booking => booking.BookingId == id);
                        if (flightbooking != null)
                        {
                            flightbooking.TripId = trip.TripId;
                           // System.Diagnostics.Debug.WriteLine("FlightBooking's Trip id: " + flightbooking.TripId);

                            _context.FlightBookings.Update(flightbooking);
                            fbookings.Add(flightbooking);
                            continue;
                        }
                        var carbooking = await _context.CarBookings.Include(c => c.Car).FirstOrDefaultAsync(booking => booking.BookingId == id);
                        if (carbooking != null)
                        {
                            carbooking.TripId = trip.TripId;
                            _context.CarBookings.Update(carbooking);
                            cbookings.Add(carbooking);
                            continue;
                        }
                        var roombooking = await _context.RoomBookings.Include(r => r.Room).FirstOrDefaultAsync(booking => booking.BookingId == id);
                        if (roombooking != null)
                        {
                            roombooking.TripId = trip.TripId;
                            _context.RoomBookings.Update(roombooking);
                            rbookings.Add(roombooking); 
                            continue;
                        }
                    }

                    _context.SaveChanges();
                string s = MakeBookingsEmail(trip, rbookings, fbookings, cbookings);
                System.Diagnostics.Debug.WriteLine(s);

                
                    await _emailSender.SendEmailAsync(email, "Booking Confirmation", s);

                ids.Clear();
                   _sessionService.SetSessionData<List<int>>("BookingIds", ids);


                return RedirectToAction("Index", "Home");




            }
            System.Diagnostics.Debug.WriteLine("Trip not valid");
            return View();

        }
        public string MakeBookingsEmail(Trip trip, List<RoomBooking> rbookings, List<FlightBooking> fbookings, List<CarBooking> cbookings )
        {
            string s = $"Here is a list of all your bookings in your Trip\nTrip Number: {trip.TripReference}";
            s += "<br>Room Bookings<br>";
            foreach (RoomBooking b in rbookings)
            {
                s += "<br>" + b.ToEmail();
                s += "<br>------------------";
            }
            s += "<br>Flight Bookings<br>";

            foreach (FlightBooking b in fbookings)
            {
                s += "<br>" + b.ToEmail();
                s += "<br>------------------";
            }
            s += "<br>Car Bookings<br>";

            foreach (CarBooking b in cbookings)
            {
                s += "<br>" + b.ToEmail();
                s += "<br>------------------";
            }
            s += "<br>We hope you enjoy your trip!<br>Regards,<br>ABC Company";
            return s;

        }
    }
}
