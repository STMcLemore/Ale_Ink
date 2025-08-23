using Ale_Ink.API.Data;
using Ale_Ink.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.API.Services
{
    public class NoteAssignmentService : INoteAssignmentService
    {
        private readonly AppDbContext _context;

        public NoteAssignmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AssignNoteAsync(int noteId, string type, string name)
        {
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Type and name must be provided.");
            }

            //Check if the note exists
            object? existingEntity = type.ToLower() switch
            {
                "item" => await _context.Items.FirstOrDefaultAsync(i => i.Name == name),
                "person" => await _context.People.FirstOrDefaultAsync(p => p.Name == name),
                "place" => await _context.Places.FirstOrDefaultAsync(p => p.Name == name),
                _ => throw new ArgumentException($"Invalid type: {type}")
            };

            // Create new entity if not found
            if (existingEntity == null)
            {
                existingEntity = type.ToLower() switch
                {
                    "item" => new Item { Name = name },
                    "person" => new Person { Name = name },
                    "place" => new Place { Name = name },
                    _ => throw new ArgumentException($"Invalid type: {type}")
                };

                _context.Add(existingEntity);
                await _context.SaveChangesAsync();
            }

            //Link note to entity (avoiding duplicates)
            bool exists = type.ToLower() switch
            {
                "item" => await _context.Notes
                .Where(n => n.NoteId == noteId)
                .AnyAsync(n => n.Items.Any(i => i.ItemId == ((Item)existingEntity).ItemId)),
                "person" => await _context.Notes
                .Where(n => n.NoteId == noteId)
                .AnyAsync(n => n.People.Any(p => p.PersonId == ((Person)existingEntity).PersonId)),
                "place" => await _context.Notes
                .Where(n => n.NoteId == noteId)
                .AnyAsync(n => n.Places.Any(p => p.PlaceId == ((Place)existingEntity).PlaceId)),
                _ => false
            };

            if (!exists)
            {
                var note = await _context.Notes
                    .Include(n => n.Items)
                    .Include(n => n.People)
                    .Include(n => n.Places)
                    .FirstOrDefaultAsync(n => n.NoteId == noteId);

                if (note != null)
                {
                    switch (type.ToLower())
                    {
                        case "item":
                            note.Items.Add((Item)existingEntity);
                            break;
                        case "person":
                            note.People.Add((Person)existingEntity);
                            break;
                        case "place":
                            note.Places.Add((Place)existingEntity);
                            break;
                    }
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
