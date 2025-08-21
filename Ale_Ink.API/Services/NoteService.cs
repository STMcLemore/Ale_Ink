using Ale_Ink.Shared.Models;
using Ale_Ink.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.API.Services
{

    public class NoteService(AppDbContext context) : INoteService
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _context.Notes
                .Include(n => n.Items)
                .Include(n => n.People)
                .Include(n => n.Places)
                .ToListAsync();
        }

        public async Task<Note?> GetNoteByIdAsync(int NoteId)
        {
            return await _context.Notes
               .Include(n => n.Items)
               .Include(n => n.People)
               .Include(n => n.Places)
               .SingleOrDefaultAsync(x => x.NoteId == NoteId);
        }

        public async Task<Note> AddNoteAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task UpdateNoteAsync(int id, Note note)
        {
            if (_context.Notes.Any(x => x.NoteId == id))
            {
                _context.Notes.Update(note);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Unable to process the operation.");
            }
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _context.Notes
                .Include(n => n.Items)
                .Include(n => n.People)
                .Include(n => n.Places)
                .SingleOrDefaultAsync(x => x.NoteId == id);

            if (note == null)
            {
                throw new InvalidOperationException($"Note with ID {id} not found.");
            }

            // For each item in the note, check if it is linked to other notes
            foreach (var item in note.Items.ToList())
            {
                bool isLinkedToOtherNotes = await _context.Notes
                    .AnyAsync(n => n.NoteId != id && n.Items.Any(i => i.ItemId == item.ItemId));

                if (!isLinkedToOtherNotes)
                {
                    _context.Items.Remove(item); // Remove the item if it is not linked to any other notes
                }

                else
                {
                    note.Items.Remove(item);  // Remove the item from the note, but keep it in the database
                }
            }

            // Do the same for people and places
            foreach (var person in note.People.ToList())
            {
                bool isLinkedToOtherNotes = await _context.Notes
                    .AnyAsync(n => n.NoteId != id && n.People.Any(p => p.PersonId == person.PersonId));
                if (!isLinkedToOtherNotes)
                {
                    _context.People.Remove(person); // Remove the person if it is not linked to any other notes
                }
                else
                {
                    note.People.Remove(person);  // Remove the person from the note, but keep it in the database
                }
            }

            foreach (var place in note.Places.ToList())
            {
                bool isLinkedToOtherNotes = await _context.Notes
                    .AnyAsync(n => n.NoteId != id && n.Places.Any(p => p.PlaceId == place.PlaceId));
                if (!isLinkedToOtherNotes)
                {
                    _context.Places.Remove(place); // Remove the place if it is not linked to any other notes
                }
                else
                {
                    note.Places.Remove(place);  // Remove the place from the note, but keep it in the database
                }
            }

            _context.Notes.Remove(note);

            await _context.SaveChangesAsync();

        }
        public async Task<List<Note>> GetNotesByKeywordAsync(string keyword)
        {
            return await _context.Notes
                .Where(n => n.Content.Contains(keyword))
                .ToListAsync();
        }
    }

}
