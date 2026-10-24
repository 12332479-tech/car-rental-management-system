using CarRentalWinForms.Models;

namespace CarRentalWinForms.Interfaces;

// Interface requirement: WinForms communicates with database only through REST API methods.
public interface IApiClient
{
    Task<UserDto?> LoginAsync(string email);
    Task<UserDto?> RegisterAsync(string name, string email, string phone);
    Task<List<Car>> GetCarsAsync(string search = "");
    Task<bool> AddCarAsync(Car car);
    Task<bool> UpdateCarAsync(Car car);
    Task<bool> DeleteCarAsync(int id);
    Task<bool> BookAsync(int userId, int carId, DateTime start, DateTime end);
    Task<List<ReservationView>> GetReservationsAsync();
    Task<List<UserReservationView>> GetMyReservationsAsync(int userId);
    Task<bool> CancelReservationAsync(int reservationId);
}
