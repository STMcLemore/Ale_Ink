using Ale_Ink.Shared.Models;
using Ale_Ink.API.Services;
using Microsoft.AspNetCore.Mvc;
using Ale_Ink.Shared.DTOs;
using SQLitePCL;

namespace Ale_Ink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly INoteAssignmentService _noteAssignmentService;

        public NoteController(INoteService noteService, INoteAssignmentService noteAssignmentService)
        {
            _noteService = noteService;
            _noteAssignmentService = noteAssignmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes()
        {
            try
            {
                var result = await _noteService.GetAllNotesAsync();
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
                var note = await _noteService.GetNoteByIdAsync(id);

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

                await _noteService.AddNoteAsync(note);
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
                if (note == null)
                {
                    return BadRequest("Note data is missing.");
                }

                if (id != note.NoteId)
                {
                    return BadRequest("Note ID mismatch.");
                }

                await _noteService.UpdateNoteAsync(id, note);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            try
            {
                await _noteService.DeleteNoteAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = $"Note with ID {id} not found.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }

        [HttpGet("search/{keyword}")]
        public async Task<ActionResult<IEnumerable<Note>>> SearchNotes(string keyword)
        {
            try
            {
                var notes = await _noteService.GetNotesByKeywordAsync(keyword);
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignNote([FromBody] NoteAssignmentDTO dto)
        {
            await _noteAssignmentService.AssignNoteAsync(dto.NoteId, dto.Type, dto.Name);
            return Ok();
        }
    }
}
