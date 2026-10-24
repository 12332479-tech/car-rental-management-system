using CarRentalApi.Models;
using CarRentalApi.Repositories;
using CarRentalApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarRepository _cars;
    private readonly IFileLogService _log;

    public CarsController(ICarRepository cars, IFileLogService log)
    {
        _cars = cars;
        _log = log;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Car>>> GetCars(string? search, string? carClass, decimal? maxPrice)
    {
        try
        {
            return await _cars.GetAllAsync(search, carClass, maxPrice);
        }
        catch (Exception ex)
        {
            await _log.WriteAsync("Get cars error: " + ex.Message);
            return StatusCode(500, "Error loading cars.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetCar(int id)
    {
        var car = await _cars.GetByIdAsync(id);
        return car == null ? NotFound() : car;
    }

    [HttpPost]
    public async Task<ActionResult<Car>> AddCar(Car car)
    {
        if (string.IsNullOrWhiteSpace(car.Brand) || string.IsNullOrWhiteSpace(car.Model) || car.PricePerDay <= 0)
            return BadRequest("Brand, model, and valid price are required.");

        var saved = await _cars.AddAsync(car);
        await _log.WriteAsync($"Car added: {saved.Brand} {saved.Model}");
        return CreatedAtAction(nameof(GetCar), new { id = saved.CarId }, saved);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar(int id, Car car)
    {
        if (string.IsNullOrWhiteSpace(car.Brand) || string.IsNullOrWhiteSpace(car.Model) || car.PricePerDay <= 0)
            return BadRequest("Brand, model, and valid price are required.");

        var updated = await _cars.UpdateAsync(id, car);
        if (!updated) return BadRequest("Invalid car id.");
        await _log.WriteAsync($"Car updated: ID {id}");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var deleted = await _cars.DeleteAsync(id);
        if (!deleted) return NotFound();
        await _log.WriteAsync($"Car deleted: ID {id}");
        return NoContent();
    }
}
