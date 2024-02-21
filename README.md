210224 Merge Change Log
- Added Room Booking base class (Room:RoomBooking = 1:M)
- Updated Car search to use location, car capacity, start and end date
- Added Hotel search by location, room capacity, check in and check out date
- Added Room search by room capacity, check in and check out date (and hotelId embedded)


190224 Merge Change log
- Base Classes (Car, Car Rental Company, Hotel, Room, Booking, CarBooking)
- All basic CRUD for above classes
- Incomplete: Room Booking
- Hotel Search by location and capacity
- Car search by location, start and end date
- Car booking

130224 Changelog
- Added CarRentalCompany Model & Controller
- Added Car Model & Controller

120224 Changelog
- Added Hotel model
- Added Room model
- Added Hotel controller (basic CRUD)
- Added Room controller (basic CRUD)
- Setup Data/ApplicationDbContext.cs (two tables and 1:M relationship)
- Setup appsettings.json (added ConnectionStrings)
- Setup Program.cs (config app to take DbContext file with ConnectionString)
- Ran Add-Migration / Update-Database commands
