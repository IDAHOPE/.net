using API.Data;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await _roomRepository.Get();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Room>> GetRooms(int id)
        {
            return await _roomRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Room>> PostRooms([FromBody] Room room)
        {
            var newRoom = await _roomRepository.Create(room);
            return CreatedAtAction(nameof(GetRooms), new { Id = newRoom.RoomId }, newRoom);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutRooms(int id, [FromBody] Room room)
        {
            if (id != room.RoomId)
            {
                return BadRequest();
            }
            await _roomRepository.Update(room);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var roomToDelete = await _roomRepository.Get(id);
            if (roomToDelete == null)
                return NotFound();

            await _roomRepository.Delete(roomToDelete.RoomId);
            return NoContent();
        }
    }
}
