using Ale_Ink.Shared.Models;
using Ale_Ink.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.API.Services
{
    public class ItemService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Item>> GetAllItemsAsync() =>
            await _context.Items.ToListAsync();

        public async Task<Item?> GetItemByIdAsync(int id) =>
            await _context.Items.SingleOrDefaultAsync(x => x.ItemId == id);

        public async Task<Item> AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateItemAsync(int id, Item item)
        {
            if (_context.Items.Any(x => x.ItemId == id))
            {
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Unable to process the operation.");
            }
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = _context.Items.Single(x => x.ItemId == id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Unable to process the operation.");
            }
        }
    }
}
