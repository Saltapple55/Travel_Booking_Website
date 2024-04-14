using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelGroupAssignment1.Areas.CarManagement.Controllers;
using TravelGroupAssignment1.Areas.CarManagement.Models;
using TravelGroupAssignment1.Data;

namespace TravelGroupAssignment_UnitTesting.Controllers
{
    public class CarBookingControllerTests
    {
        public ApplicationDbContext GetApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        public async void SetupCarsAndRentalCompanyAsync(ApplicationDbContext context)
        {
            var stubCompany = new CarRentalCompany { CarRentalCompanyId = 1, CompanyName = "stubCompany", Location = "stubLocation" };

            var stubCar1 = new Car { CarId = 1, Make = "Toyota", Model = "Corolla", Type = "Sedan", CompanyId = 1, Company = stubCompany };
            var stubCar2 = new Car { CarId = 2, Make = "Toyota", Model = "Yaris", Type = "Hatchback", CompanyId = 1, Company = stubCompany };
            var stubCar3 = new Car { CarId = 3, Make = "Toyota", Model = "RAV4", Type = "SUV", CompanyId = 1, Company = stubCompany };

            await context.Cars.AddRangeAsync(stubCar1, stubCar2, stubCar3);

            await context.CarBookings.AddRangeAsync(
                new CarBooking { BookingId = 1, CarId = 1, Car = stubCar1, TripId = 1, StartDate = new DateTime(2020, 1, 1), EndDate = new DateTime(2020, 1, 2) },
                new CarBooking { BookingId = 2, CarId = 1, Car = stubCar1, TripId = 1, StartDate = new DateTime(2020, 1, 2), EndDate = new DateTime(2020, 1, 3) },
                new CarBooking { BookingId = 3, CarId = 2, Car = stubCar2, TripId = 1, StartDate = new DateTime(2020, 1, 1), EndDate = new DateTime(2020, 1, 2) }
            );
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task Index_ReturnsViewResult_AndListOfCarBookings()
        {

            using (var context = GetApplicationDbContext())
            {
                // Arrange: Cars, Company and Controller
                SetupCarsAndRentalCompanyAsync(context);

                var controller = new CarBookingController(context);

                // Action: call Index()
                var result = await controller.Index(1);

                // Assert: returned ViewResult, model returned is List<Car>, with 2 bookings
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Null(viewResult.ViewName); // default value = null, if View Name not specified in return View()

                var model = Assert.IsAssignableFrom<List<CarBooking>>(viewResult.ViewData.Model);
                Assert.Equal(2, model.Count);

                // Assert: returned models match created bookings
                Assert.Equal(1, model[0].BookingId);
                Assert.Equal(2, model[1].BookingId);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Index_SearchCarBookingWithNoCarReference_ReturnNotFound()
        {

            using (var context = GetApplicationDbContext())
            {
                // Arrange: Cars, Company and Controller
                var stubCompany = new CarRentalCompany { CarRentalCompanyId = 1, CompanyName = "stubCompany", Location = "stubLocation" };

                var stubCar1 = new Car { CarId = 1, Make = "Toyota", Model = "Corolla", Type = "Sedan", CompanyId = 1, Company = stubCompany };
                var stubCar2 = new Car { CarId = 2, Make = "Toyota", Model = "Yaris", Type = "Hatchback", CompanyId = 1, Company = stubCompany };
                var stubCar3 = new Car { CarId = 3, Make = "Toyota", Model = "RAV4", Type = "SUV", CompanyId = 1, Company = stubCompany };

                await context.Cars.AddRangeAsync(stubCar1, stubCar2, stubCar3);

                await context.CarBookings.AddAsync(
                    new CarBooking { BookingId = 1, CarId = 1, Car = null, TripId = 1, StartDate = new DateTime(2020, 1, 1), EndDate = new DateTime(2020, 1, 2) }
                );
                await context.SaveChangesAsync();

                var controller = new CarBookingController(context);

                // Action: call Index()
                var result = await controller.Index(4);

                // Assert: returned ViewResult, model returned is List<Car>, with 2 bookings
                var viewResult = Assert.IsType<NotFoundResult>(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Details_ReturnsViewResult_AndCarBooking()
        {

            using (var context = GetApplicationDbContext())
            {
                // Arrange: Cars, Company and Controller
                var stubCompany = new CarRentalCompany { CarRentalCompanyId = 1, CompanyName = "stubCompany", Location = "stubLocation" };

                var stubCar1 = new Car { CarId = 1, Make = "Toyota", Model = "Corolla", Type = "Sedan", CompanyId = 1, Company = stubCompany };
                var stubCar2 = new Car { CarId = 2, Make = "Toyota", Model = "Yaris", Type = "Hatchback", CompanyId = 1, Company = stubCompany };
                var stubCar3 = new Car { CarId = 3, Make = "Toyota", Model = "RAV4", Type = "SUV", CompanyId = 1, Company = stubCompany };

                await context.Cars.AddRangeAsync(stubCar1, stubCar2, stubCar3);

                await context.CarBookings.AddRangeAsync(
                    new CarBooking { BookingId = 1, CarId = 1, Car = stubCar1, TripId = 1, StartDate = new DateTime(2020, 1, 1), EndDate = new DateTime(2020, 1, 2) },
                    new CarBooking { BookingId = 2, CarId = 1, Car = stubCar1, TripId = 1, StartDate = new DateTime(2020, 1, 2), EndDate = new DateTime(2020, 1, 3) },
                    new CarBooking { BookingId = 3, CarId = 2, Car = stubCar2, TripId = 1, StartDate = new DateTime(2020, 1, 1), EndDate = new DateTime(2020, 1, 2) }
                );
                await context.SaveChangesAsync();

                var controller = new CarBookingController(context);

                // Action: call Index()
                var result = await controller.Details(1);

                // Assert: returned ViewResult, model returned is List<Car>, with 2 bookings
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Null(viewResult.ViewName); // default value = null, if View Name not specified in return View()

                var model = Assert.IsAssignableFrom<CarBooking>(viewResult.ViewData.Model);
                Assert.Equal(1, model.BookingId);
                Assert.Equal(1, model.CarId);
                Assert.Equivalent(stubCar1, model.Car);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Details_NonExistentBookingId_ReturnNotFound()
        {

            using (var context = GetApplicationDbContext())
            {
                // Arrange: Cars, Company and Controller

                await context.SaveChangesAsync();

                var controller = new CarBookingController(context);

                // Action: call Index()
                var result = await controller.Details(1);

                // Assert: returned ViewResult, model returned is List<Car>, with 2 bookings
                var viewResult = Assert.IsType<NotFoundResult>(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task CreateGet_ReturnsViewResult_AndCarModel()
        {

            using (var context = GetApplicationDbContext())
            {
                // Arrange: Cars, Company and Controller
                SetupCarsAndRentalCompanyAsync(context);

                var controller = new CarBookingController(context);

                // Action: call Index()
                var result = await controller.Create(1, new DateTime(2020, 1, 1), new DateTime(2020, 1, 5));

                // Assert: returned ViewResult, model returned is List<Car>, with 2 bookings
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Null(viewResult.ViewName); // default value = null, if View Name not specified in return View()

                var model = Assert.IsAssignableFrom<CarBooking>(viewResult.ViewData.Model);

                // Assert: returned models match created bookings
                // *** MAY NEED UPDATE AFTER TRIP MANAGEMENT IMPLEMENTED ***
                Assert.Equal(1, model.CarId);
                Assert.Equal(1, model.TripId);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task CreateGet_NonExistentCarId_ReturnNotFound()
        {

            using (var context = GetApplicationDbContext())
            {
                // Arrange: Cars, Company and Controller
                var controller = new CarBookingController(context);

                // Action: call Index()
                var result = await controller.Create(1, new DateTime(2020, 1, 1), new DateTime(2020, 1, 5));

                // Assert: returned ViewResult, model returned is List<Car>, with 2 bookings
                var viewResult = Assert.IsType<NotFoundResult>(result);

                context.Database.EnsureDeleted();
            }
        }
    }
}
