using Ale_Ink.Shared.Models;

namespace Ale_Ink.API.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person?> GetPersonByIdAsync(int id);
        Task<Person> AddPersonAsync(Person person);
        Task UpdatePersonAsync(int id, Person person);
        Task DeletePersonAsync(int id);
    }
}