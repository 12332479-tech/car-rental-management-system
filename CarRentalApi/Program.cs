using CarRentalApi.Data;
using CarRentalApi.Models;
using CarRentalApi.Repositories;
using CarRentalApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Interfaces + dependency injection requirement.
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddSingleton<IFileLogService, FileLogService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // EF Core Code First requirement: apply migrations to SQL Server.
    db.Database.Migrate();

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User { FullName = "Administrator", Email = "admin@car.com", Phone = "00000000", Role = "Admin" },
            new User { FullName = "Demo User", Email = "user@car.com", Phone = "71111111", Role = "Customer" }
        );
    }

    if (!db.Cars.Any())
    {
        db.Cars.AddRange(
            new Car { Brand = "Toyota", Model = "Corolla", Color = "White", CarClass = "Economy", PricePerDay = 35, IsAvailable = true },
            new Car { Brand = "Honda", Model = "Civic", Color = "Black", CarClass = "Sedan", PricePerDay = 45, IsAvailable = true },
            new Car { Brand = "Hyundai", Model = "Tucson", Color = "Silver", CarClass = "SUV", PricePerDay = 70, IsAvailable = true },
            new Car { Brand = "BMW", Model = "X5", Color = "Blue", CarClass = "Luxury", PricePerDay = 120, IsAvailable = true }
        );
    }

    db.SaveChanges();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
