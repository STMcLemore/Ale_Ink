using Ale_Ink.Shared.Models;
using Ale_Ink.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ale_Ink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetAllPlaces()
        {
            try
            {
                var result = await _placeService.GetAllPlacesAsync();
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
                var place = await _placeService.GetPlaceByIdAsync(id);

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

                await _placeService.AddPlaceAsync(place);
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
                if (place == null)
                {
                    return BadRequest("Place data is missing.");
                }

                if (id != place.PlaceId)
                {
                    return BadRequest("Place ID mismatch.");
                }

                await _placeService.UpdatePlaceAsync(id, place);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            try
            {
                await _placeService.DeletePlaceAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = $"Place with ID {id} not found.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
