using Ale_Ink.Shared.Models;
using Ale_Ink.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ale_Ink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            try
            {
                var result = await _itemService.GetAllItemsAsync();
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
                var note = await _itemService.GetItemByIdAsync(id);

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
        public async Task<ActionResult<Item>> AddItem(Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Note data is missing.");
                }

                await _itemService.AddItemAsync(item);
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
                await _itemService.UpdateItemAsync(id, item);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var note = await _itemService.GetItemByIdAsync(id);
                if (note == null)
                {
                    return NotFound();
                }

                await _itemService.DeleteItemAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
