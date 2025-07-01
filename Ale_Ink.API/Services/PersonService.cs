using Ale_Ink.Shared.Models;
using Ale_Ink.Shared.DTOs;
using Ale_Ink.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.API.Services
{
    public class PersonService : IPersonService
    {
        private readonly AppDbContext _context;

        public PersonService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync() =>
            await _context.People.ToListAsync();

        public async Task<Person?> GetPersonByIdAsync(int id) =>
            await _context.People.SingleOrDefaultAsync(x => x.PersonId == id);

        public async Task<Person> AddPersonFromNoteAsync(PersonFromNoteDTO dto)
        {
            var note = await _context.Notes.FindAsync(dto.NoteId);
            if (note == null)
            {
                throw new InvalidOperationException($"Note with ID {dto.NoteId} not found.");
            }

            var person = new Person
            {
                Name = dto.Name,
                Notes = new List<Note> { note }
            };
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
