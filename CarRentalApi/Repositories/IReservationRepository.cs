using CarRentalApi.Dtos;
using CarRentalApi.Models;

namespace CarRentalApi.Repositories;

// Interface requirement: reservation data operations contract.
public interface IReservationRepository
{
    Task<List<object>> GetAllAsync();
    Task<List<object>> GetByUserAsync(int userId);
    Task<(Reservation? Reservation, string? Error)> CreateAsync(CreateReservationDto dto);
    Task<bool> CancelAsync(int id);
}
