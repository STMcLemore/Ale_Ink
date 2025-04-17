using Ale_Ink.Shared.Models;
using Ale_Ink.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.API.Services
{
    public class PersonService(AppDbContext context) : IPersonService
    {
        private readonly AppDbContext _context = context;
        public async Task<IEnumerable<Person>> GetAllPersonsAsync() =>
            await _context.People.ToListAsync();
        public async Task<Person?> GetPersonByIdAsync(int id) =>
            await _context.People.SingleOrDefaultAsync(x => x.PersonId == id);
        public async Task<Person> AddPersonAsync(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }
        public async Task UpdatePersonAsync(int id, Person person)
        {
            if (_context.People.Any(x => x.PersonId == id))
            {
                _context.People.Update(person);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Unable to process the operation.");
            }
        }
        public async Task DeletePersonAsync(int id)
        {
            var person = _context.People.Single(x => x.PersonId == id);
            if (person != null)
            {
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Unable to process the operation.");
            }
        }
    }
}
