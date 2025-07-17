using Ale_Ink.Shared.DTOs;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.HttpServices
{
    public interface INoteHttpService
    {
        Task<List<Note>> GetAllNotesAsync();
        Task<Note> GetNoteByIdAsync(int id);
        Task<Note> AddNoteAsync(Note note);
        Task UpdateNoteAsync(int id, Note note);
        Task DeleteNoteAsync(int id);
        Task<List<Note>> GetNotesByKeywordAsync(string keyword);
        Task<List<Note>> GetNotesByItemIdAsync(int itemId);
    }
}
