using Ale_Ink.Shared.DTOs;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.HttpServices
{
    public interface IItemHttpService
    {
        Task<List<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> AddItemAsync(Item item);
        Task<Item> AddItemFromNoteAsync(ItemFromNoteDTO dto);
        Task<Item> UpdateItemAsync(int id, Item item);
        Task DeleteItemAsync(int id);
    }
}
