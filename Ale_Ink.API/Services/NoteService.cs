using Ale_Ink.Shared.Models;
using Ale_Ink.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.API.Services
{

    public class NoteService(AppDbContext context) : INoteService
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Note>> GetAllNotesAsync() =>
            await _context.Notes.ToListAsync();

        public async Task<Note?> GetNoteByIdAsync(int id) =>
            await _context.Notes.SingleOrDefaultAsync(x => x.NoteId == id);

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
            var note = _context.Notes.Single(x => x.NoteId == id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Unable to process the operation.");
            }
        }
    }

}
