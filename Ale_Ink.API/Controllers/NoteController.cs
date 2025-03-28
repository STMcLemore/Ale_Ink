using Ale_Ink.Models;
using Ale_Ink.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ale_Ink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly IGenericRepository<Note> _noteRepository;

        public NoteController(IGenericRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes()
        {
            try
            {
                var result = await _noteRepository.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteById(int id)
        {
            try
            {
                var note = await _noteRepository.GetByIdAsync(id);

                if (note == null)
                {
                    return NotFound($"Note with ID {id} not found.");
                }

                return Ok(note);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            try
            {
                if (note == null)
                {
                    return BadRequest("Note data is missing.");
                }

                await _noteRepository.AddAsync(note);
                return Ok(note);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, Note note)
        {
            try
            {
                await _noteRepository.UpdateAsync(note);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(object id)
        {
            try
            {
                var note = await _noteRepository.GetByIdAsync(id);
                if (note == null)
                {
                    return NotFound();
                }

                await _noteRepository.DeleteAsync(note);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
    }
}
