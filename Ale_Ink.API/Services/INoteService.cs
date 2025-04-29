using Ale_Ink.Shared.Models;

namespace Ale_Ink.API.Services
{
        public interface INoteService
        {
            Task<IEnumerable<Note>> GetAllNotesAsync();
            Task<Note?> GetNoteByIdAsync(int id);
            Task<Note> AddNoteAsync(Note note);
            Task UpdateNoteAsync(int id, Note note);
            Task DeleteNoteAsync(int id);
            Task<List<Note>> GetNotesByKeywordAsync(string keyword);
        }
}
