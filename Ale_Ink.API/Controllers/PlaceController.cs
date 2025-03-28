using Ale_Ink.Models;
using Ale_Ink.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ale_Ink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IGenericRepository<Place> _placeRepository;

        public PlaceController(IGenericRepository<Place> placeRepository)
        {
            _placeRepository = placeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetAllPlaces()
        {
            try
            {
                var result = await _placeRepository.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> GetPlaceById(int id)
        {
            try
            {
                var place = await _placeRepository.GetByIdAsync(id);

                if (place == null)
                {
                    return NotFound($"Place with ID {id} not found.");
                }

                return Ok(place);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Place>> PostPlace(Place place)
        {
            try
            {
                if (place == null)
                {
                    return BadRequest("Place data is missing.");
                }

                await _placeRepository.AddAsync(place);
                return Ok(place);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(int id, Place place)
        {
            try
            {
                await _placeRepository.UpdateAsync(place);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(object id)
        {
            try
            {
                var place = await _placeRepository.GetByIdAsync(id);
                if (place == null)
                {
                    return NotFound();
                }

                await _placeRepository.DeleteAsync(place);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
