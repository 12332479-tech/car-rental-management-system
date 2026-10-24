using CarRentalApi.Data;
using CarRentalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Repositories;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;
    public CarRepository(AppDbContext context) => _context = context;

    public async Task<List<Car>> GetAllAsync(string? search, string? carClass, decimal? maxPrice)
    {
        var query = _context.Cars.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => c.Brand.Contains(search) || c.Model.Contains(search) || c.Color.Contains(search));
        if (!string.IsNullOrWhiteSpace(carClass))
            query = query.Where(c => c.CarClass == carClass);
        if (maxPrice.HasValue)
            query = query.Where(c => c.PricePerDay <= maxPrice.Value);
        return await query.OrderBy(c => c.Brand).ThenBy(c => c.Model).ToListAsync();
    }

    public Task<Car?> GetByIdAsync(int id) => _context.Cars.FirstOrDefaultAsync(c => c.CarId == id);

    public async Task<Car> AddAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return car;
    }

    public async Task<bool> UpdateAsync(int id, Car car)
    {
        if (id != car.CarId) return false;
        var exists = await _context.Cars.AnyAsync(c => c.CarId == id);
        if (!exists) return false;
        _context.Entry(car).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return false;
        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        return true;
    }
}
