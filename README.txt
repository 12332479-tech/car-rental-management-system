mhmd - Car Rental Project - Phase 2 Fixed Version

What is included for the Phase 2 requirements:
1. Windows Forms GUI project: CarRentalWinForms
2. ASP.NET Core REST API project: CarRentalApi
3. EF Core with SQL Server LocalDB
4. Code First classes: User, Car, Reservation
5. Migration folder included:
   - CarRentalApi/Migrations/20260530195000_InitialCreate.cs
   - CarRentalApi/Migrations/AppDbContextModelSnapshot.cs
6. Program.cs uses db.Database.Migrate(); NOT EnsureCreated().
7. Interfaces included:
   - ICarRepository
   - IReservationRepository
   - IFileLogService
   - IApiClient
8. Exception handling included with try/catch in API and WinForms API client.
9. File handling included in Logs/FileLogService and WinForms LocalFileService.

How to know Migrations are present:
Open Visual Studio > Solution Explorer > CarRentalApi > Migrations.
You must see files like:
- 20260530195000_InitialCreate.cs
- AppDbContextModelSnapshot.cs

How to run:
1. Open CarRentalApplication.sln in Visual Studio 2022.
2. Make sure SQL Server LocalDB is installed.
3. Right click the solution > Restore NuGet Packages.
4. Set multiple startup projects:
   - CarRentalApi = Start
   - CarRentalWinForms = Start
5. Run the project.

Default login without password:
Admin email: admin@car.com
User email: user@car.com

Important:
If SQL Server LocalDB is not installed on the computer, the API will not connect to the database. This is normal because the assignment requires SQL Server.
