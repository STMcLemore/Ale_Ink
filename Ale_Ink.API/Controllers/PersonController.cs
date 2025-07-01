using Ale_Ink.Shared.Models;
using Ale_Ink.API.Services;
using Microsoft.AspNetCore.Mvc;
using Ale_Ink.Shared.DTOs;

namespace Ale_Ink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPeople()
        {
            try
            {
                var result = await _personService.GetAllPersonsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            try
            {
                var person = await _personService.GetPersonByIdAsync(id);

                if (person == null)
                {
                    return NotFound($"Person with ID {id} not found.");
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("from-note")]
        public async Task<ActionResult<Person>> AddItemFromNoteAsync(PersonFromNoteDTO dto)
        {
            try
            {
                var person = await _personService.AddPersonFromNoteAsync(dto);
                return Ok(person);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message, Details = "Ensure the note ID exists." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, Person person)
        {
            try
            {
                if (person == null)
                {
                    return BadRequest("Note data is missing.");
                }

                if (id != person.PersonId)
                {
                    return BadRequest("Note ID mismatch.");
                }

                await _personService.UpdatePersonAsync(id, person);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                await _personService.DeletePersonAsync(id);
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
    }
}
