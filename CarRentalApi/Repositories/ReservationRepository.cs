using CarRentalApi.Data;
using CarRentalApi.Dtos;
using CarRentalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;
    public ReservationRepository(AppDbContext context) => _context = context;

    public async Task<List<object>> GetAllAsync()
    {
        var data = await _context.Reservations
            .Include(r => r.User).Include(r => r.Car)
            .OrderByDescending(r => r.ReservationId)
            .Select(r => new {
                r.ReservationId, r.StartDate, r.EndDate, r.TotalPrice, r.Status,
                Customer = r.User!.FullName,
                Car = r.Car!.Brand + " " + r.Car.Model
            }).ToListAsync();
        return data.Cast<object>().ToList();
    }

    public async Task<List<object>> GetByUserAsync(int userId)
    {
        var data = await _context.Reservations
            .Include(r => r.Car)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.ReservationId)
            .Select(r => new {
                r.ReservationId, r.StartDate, r.EndDate, r.TotalPrice, r.Status,
                CarName = r.Car!.Brand + " " + r.Car.Model
            }).ToListAsync();
        return data.Cast<object>().ToList();
    }

    public async Task<(Reservation? Reservation, string? Error)> CreateAsync(CreateReservationDto dto)
    {
        var user = await _context.Users.FindAsync(dto.UserId);
        if (user == null) return (null, "User not found.");

        var car = await _context.Cars.FindAsync(dto.CarId);
        if (car == null || !car.IsAvailable) return (null, "Car is not available.");
        if (dto.EndDate.Date < dto.StartDate.Date) return (null, "End date must be after start date.");

        var days = Math.Max(1, (dto.EndDate.Date - dto.StartDate.Date).Days + 1);
        var reservation = new Reservation
        {
            UserId = dto.UserId,
            CarId = dto.CarId,
            StartDate = dto.StartDate.Date,
            EndDate = dto.EndDate.Date,
            TotalPrice = days * car.PricePerDay,
            Status = "Booked"
        };
        car.IsAvailable = false;
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return (reservation, null);
    }

    public async Task<bool> CancelAsync(int id)
    {
        var reservation = await _context.Reservations.Include(r => r.Car).FirstOrDefaultAsync(r => r.ReservationId == id);
        if (reservation == null) return false;
        reservation.Status = "Cancelled";
        if (reservation.Car != null) reservation.Car.IsAvailable = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
