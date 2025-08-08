using Ale_Ink.Shared.Models;
using Ale_Ink.API.Services;
using Microsoft.AspNetCore.Mvc;
using Ale_Ink.Shared.DTOs;

namespace Ale_Ink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
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


        [HttpPost("from-note")]
        public async Task<ActionResult<Item>> AddItemFromNoteAsync(ItemFromNoteDTO dto)
        {
            try
            {
                var item = await _itemService.AddItemFromNoteAsync(dto);
                return Ok(item);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
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
                if (item == null)
                {
                    return BadRequest("Item data is missing.");
                }

                if (id != item.ItemId)
                {
                    return BadRequest("Item ID mismatch.");
                }

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
                await _itemService.DeleteItemAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = $"Item with ID {id} not found.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
