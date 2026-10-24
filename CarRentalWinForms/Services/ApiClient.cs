using System.Net.Http.Json;
using CarRentalWinForms.Models;
using CarRentalWinForms.Interfaces;
using CarRentalWinForms.Utilities;

namespace CarRentalWinForms.Services;

public class ApiClient : IApiClient
{
    public const string ApiBaseUrl = "http://localhost:60336/api/";
    private readonly HttpClient _http = new() { BaseAddress = new Uri(ApiBaseUrl) };

    public async Task<UserDto?> LoginAsync(string email)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("auth/login", new { email });
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
        catch (Exception ex) { LocalFileService.WriteLog("API error: " + ex.Message); return null; }
    }

    public async Task<UserDto?> RegisterAsync(string name, string email, string phone)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("auth/register", new { fullName = name, email, phone });
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
        catch (Exception ex) { LocalFileService.WriteLog("API error: " + ex.Message); return null; }
    }

    public async Task<List<Car>> GetCarsAsync(string search = "")
    {
        try { return await _http.GetFromJsonAsync<List<Car>>($"cars?search={Uri.EscapeDataString(search)}") ?? new(); }
        catch (Exception ex) { LocalFileService.WriteLog("API error: " + ex.Message); return new(); }
    }

    public async Task<bool> AddCarAsync(Car car)
    {
        try { return (await _http.PostAsJsonAsync("cars", car)).IsSuccessStatusCode; }
        catch (Exception ex) { LocalFileService.WriteLog("API error: " + ex.Message); return false; }
    }

    public async Task<bool> UpdateCarAsync(Car car)
    {
        try { return (await _http.PutAsJsonAsync($"cars/{car.CarId}", car)).IsSuccessStatusCode; }
        catch (Exception ex) { LocalFileService.WriteLog("API error: " + ex.Message); return false; }
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
        try { return (await _http.DeleteAsync($"cars/{id}")).IsSuccessStatusCode; }
        catch (Exception ex) { LocalFileService.WriteLog("API error: " + ex.Message); return false; }
    }

    public async Task<bool> BookAsync(int userId, int carId, DateTime start, DateTime end)
    {
        try { return (await _http.PostAsJsonAsync("reservations", new { userId, carId, startDate = start, endDate = end })).IsSuccessStatusCode; }
        catch (Exception ex) { LocalFileService.WriteLog("API error: " + ex.Message); return false; }
    }

    public async Task<List<ReservationView>> GetReservationsAsync()
    {
        try { return await _http.GetFromJsonAsync<List<ReservationView>>("reservations") ?? new(); }
        catch (Exception ex) { LocalFileService.WriteLog("API error: " + ex.Message); return new(); }
    }

    public async Task<List<UserReservationView>> GetMyReservationsAsync(int userId)
    {
        try { return await _http.GetFromJsonAsync<List<UserReservationView>>($"reservations/user/{userId}") ?? new(); }
        catch (Exception ex) { LocalFileService.WriteLog("API error: " + ex.Message); return new(); }
    }

    public async Task<bool> CancelReservationAsync(int reservationId)
    {
        try { return (await _http.PutAsync($"reservations/cancel/{reservationId}", null)).IsSuccessStatusCode; }
        catch (Exception ex) { LocalFileService.WriteLog("API error: " + ex.Message); return false; }
    }
}
