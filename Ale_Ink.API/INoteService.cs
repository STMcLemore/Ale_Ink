

using Ale_Ink.Shared.Models;

namespace Ale_Ink.API
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<Note> GetNoteByIdAsync(int id);
        Task AddNoteAsync(Note note);
        Task UpdateNoteAsync(Note note);
        Task DeleteNoteAsync(int id);
        Task<IEnumerable<Note>> GetNotesByPersonIdAsync(int personId);
        Task<IEnumerable<Note>> GetNotesByPlaceIdAsync(int placeId);
        Task<IEnumerable<Note>> GetNotesByItemIdAsync(int itemId);
    }
}
