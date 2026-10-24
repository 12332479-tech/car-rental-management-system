using CarRentalApi.Models;

namespace CarRentalApi.Repositories;

// Interface requirement: car data operations contract.
public interface ICarRepository
{
    Task<List<Car>> GetAllAsync(string? search, string? carClass, decimal? maxPrice);
    Task<Car?> GetByIdAsync(int id);
    Task<Car> AddAsync(Car car);
    Task<bool> UpdateAsync(int id, Car car);
    Task<bool> DeleteAsync(int id);
}
