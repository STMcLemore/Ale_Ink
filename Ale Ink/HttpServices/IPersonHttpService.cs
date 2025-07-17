using Ale_Ink.Shared.DTOs;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.HttpServices
{
    public interface IPersonHttpService
    {
        Task<List<Person>> GetAllPersonsAsync();
        Task<Person> GetPersonByIdAsync(int id);
        Task<Person> AddPersonFromNoteAsync(PersonFromNoteDTO dto);
        Task UpdatePersonAsync(int id, Person person);
        Task DeletePersonAsync(int id);
    }
}
