using Microsoft.AspNetCore.Mvc;
using SchoolOfDevs.Services;
using SchoolOfDevs.Dto.Note;
using SchoolOfDevs.Authorization;
using SchoolOfDevs.Enums;

namespace SchoolOfDevs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _service;

        public NoteController(INoteService service)
        {
            _service = service;
        }

        [Authorize(TypeUser.Teacher, TypeUser.Both)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteRequest note) =>
            Ok(await _service.Create(note));

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [Authorize(TypeUser.Teacher, TypeUser.Both)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] NoteRequest note, int id)
        {
            await _service.Update(note, id);
            return NoContent();
        }

        [Authorize(TypeUser.Teacher, TypeUser.Both)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}