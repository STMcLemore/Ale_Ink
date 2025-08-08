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
            //var note = _context.Notes.Single(x => x.NoteId == id);
            //if (note != null)
            //{
            //    _context.Notes.Remove(note);
            //    await _context.SaveChangesAsync();
            //}
            //else
            //{
            //    throw new InvalidOperationException($"Unable to process the operation.");
            //}

            var note = await _context.Notes
                .Include(n => n.Items)
                .Include(n => n.People)
                .Include(n => n.Places)
                .SingleOrDefaultAsync(x => x.NoteId == id);

            if (note == null)
            {
                throw new InvalidOperationException($"Note with ID {id} not found.");
            }

            _context.Items.RemoveRange(note.Items);
            _context.People.RemoveRange(note.People);
            _context.Places.RemoveRange(note.Places);

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
