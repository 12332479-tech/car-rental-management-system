using CarRentalApi.Dtos;
using CarRentalApi.Models;
using CarRentalApi.Repositories;
using CarRentalApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationRepository _reservations;
    private readonly IFileLogService _log;

    public ReservationsController(IReservationRepository reservations, IFileLogService log)
    {
        _reservations = reservations;
        _log = log;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAll()
    {
        return await _reservations.GetAllAsync();
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetByUser(int userId)
    {
        return await _reservations.GetByUserAsync(userId);
    }

    [HttpPost]
    public async Task<ActionResult<Reservation>> Create(CreateReservationDto dto)
    {
        var result = await _reservations.CreateAsync(dto);
        if (result.Error != null) return BadRequest(result.Error);
        await _log.WriteAsync($"Reservation created: User {dto.UserId}, Car {dto.CarId}");
        return result.Reservation!;
    }

    [HttpPut("cancel/{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        var cancelled = await _reservations.CancelAsync(id);
        if (!cancelled) return NotFound();
        await _log.WriteAsync($"Reservation cancelled: ID {id}");
        return NoContent();
    }
}
