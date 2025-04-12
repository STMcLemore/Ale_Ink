using Ale_Ink.Shared.Models;
using Ale_Ink.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ale_Ink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IGenericRepository<Person> _personRepository;

        public PersonController(IGenericRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPeople()
        {
            try
            {
                var result = await _personRepository.GetAllAsync();
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
                var person = await _personRepository.GetByIdAsync(id);

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

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            try
            {
                if (person == null)
                {
                    return BadRequest("Person data is missing.");
                }

                await _personRepository.AddAsync(person);
                return Ok(person);
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
                await _personRepository.UpdateAsync(person);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(object id)
        {
            try
            {
                var person = await _personRepository.GetByIdAsync(id);
                if (person == null)
                {
                    return NotFound();
                }

                await _personRepository.DeleteAsync(person);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
