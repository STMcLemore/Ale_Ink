using System.Collections.Generic;
using System.Threading.Tasks;
using Ale_Ink.Models;
using Ale_Ink.Repositories;

namespace Ale_Ink.API.Services
{
    public class NoteService
    {
        private readonly IGenericRepository<Note> _noteRepository;

        public NoteService(IGenericRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            return await _noteRepository.GetAllAsync();
        }

        public async Task<Note> GetNoteById(int id)
        {
            return await _noteRepository.GetByIdAsync(id);
        }

        public async Task AddNoteAsync(Note note)
        {
            await _noteRepository.AddAsync(note);
        }

        public async Task UpdateNoteAsync(Note note)
        {
            await _noteRepository.UpdateAsync(note);
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note != null)
            {
                await _noteRepository.DeleteAsync(note);
            }
        }
    }
}
