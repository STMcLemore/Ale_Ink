using Ale_Ink.Shared.DTOs;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.API.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        //Task<Item> AddItemAsync(Item item);
        Task<Item> AddItemFromNoteAsync(ItemFromNoteDTO dto);
        Task UpdateItemAsync(int id, Item item);
        Task DeleteItemAsync(int id);
    }
}
