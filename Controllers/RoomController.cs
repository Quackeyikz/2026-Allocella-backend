using _2026_Allocella_backend.Models;
using _2026_Allocella_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace _2026_Allocella_backend.Controllers;

[ApiController]
[Route("[controller]")]

public class RoomController : ControllerBase
{
    public RoomController()
    {
        
    }

    // GET all ~ Eyyikz wuz here!
    [HttpGet]
    public ActionResult<List<Room>> GetAll() =>
        RoomService.GetAll();

    // GET by Id
    [HttpGet("{id}")]
    public ActionResult<Room> Get(int id)
    {
        var room = RoomService.Get(id);

        if(room == null)
            return NotFound();

        return room;
    }

    // POST
    [HttpPost]
    public IActionResult Create(Room room)
    {
        RoomService.Add(room);
        return CreatedAtAction(nameof(Get), new { id = room.Id }, room);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, Room room)
    {
        if (id != room.Id)
            return BadRequest();

        var existingRoom = RoomService.Get(id);

        if (existingRoom is null)
            return NotFound();

        RoomService.Update(room);

        return NoContent();
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = RoomService.Get(id);

        if (room is null)
            return NotFound();

        RoomService.Delete(id);

        return NoContent();
    }
}