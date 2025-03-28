using Ale_Ink.Models;
using Ale_Ink.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ale_Ink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IGenericRepository<Item> _itemRepository;

        public ItemController(IGenericRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItemsObjects()
        {
            try
            {
                var result = await _itemRepository.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            try
            {
                var note = await _itemRepository.GetByIdAsync(id);

                if (note == null)
                {
                    return NotFound($"Item with ID {id} not found.");
                }

                return Ok(note);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Note data is missing.");
                }

                await _itemRepository.AddAsync(item);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            try
            {
                await _itemRepository.UpdateAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(object id)
        {
            try
            {
                var note = await _itemRepository.GetByIdAsync(id);
                if (note == null)
                {
                    return NotFound();
                }

                await _itemRepository.DeleteAsync(note);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
