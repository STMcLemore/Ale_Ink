using Ale_Ink.Shared.Models;
using Ale_Ink.Shared.DTOs;
using Ale_Ink.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ale_Ink.API.Services
{
    public class ItemService : IItemService
    {
        private readonly AppDbContext _context;

        public ItemService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync() =>
            await _context.Items.ToListAsync();

        public async Task<Item?> GetItemByIdAsync(int id) =>
            await _context.Items.SingleOrDefaultAsync(x => x.ItemId == id);

        public async Task<Item> AddItemFromNoteAsync(ItemFromNoteDTO dto)
        {
            var note = await _context.Notes.FindAsync(dto.NoteId);
            if (note == null)
            {
                throw new InvalidOperationException($"Note with ID {dto.NoteId} not found.");
            }

            var item = new Item
            {
                Name = dto.Name,
                Notes = new List<Note> { note }
            };
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateItemAsync(int id, Item updatedItem)
        {
            var existingItem = await _context.Items
                .Include(i => i.Notes)
                .FirstOrDefaultAsync(i => i.ItemId == id);

            if (existingItem == null)
                throw new InvalidOperationException("Item not found.");

            // Clear and re-add notes (ensures EF Core tracks changes in the join table)
            existingItem.Notes.Clear();
            foreach (var note in updatedItem.Notes)
            {
                existingItem.Notes.Add(note);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = _context.Items
                .Include(i => i.Notes)
                .SingleOrDefault(x => x.ItemId == id);

            if (item != null)
            {
                foreach (var note in item.Notes)
                {
                    note.Items.Remove(item);
                }
                await _context.SaveChangesAsync();

                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }

            else
            {
                throw new InvalidOperationException($"Unable to process the operation. Item with ID {id} not found.");
            }

        }
    }
}
