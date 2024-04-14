﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Data;




namespace TravelGroupAssignment1.ViewComponents
{
    public class DashboardSummary : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public DashboardSummary(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> TotalNumber()
        {

            int totalHotel = await _context.Hotels.CountAsync();
            int totalCar = await _context.Cars.CountAsync();
            int totalFlight = await _context.Flights.CountAsync();


            Dictionary<string, int> result = new Dictionary<string, int>
            {
                { "Hotel", totalHotel },
                { "Car", totalCar },
                { "Flight", totalFlight }
            };

            return (IActionResult)View(result);
        }
    }
}
