using Ale_Ink.Shared.Models;
using Ale_Ink.API.Data;
using Microsoft.EntityFrameworkCore;
using Ale_Ink.Shared.DTOs;

namespace Ale_Ink.API.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly AppDbContext _context;

        public PlaceService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Place>> GetAllPlacesAsync() =>
            await _context.Places.ToListAsync();

        public async Task<Place?> GetPlaceByIdAsync(int id) =>
            await _context.Places.SingleOrDefaultAsync(x => x.PlaceId == id);

        public async Task<Place> AddPlaceFromNoteAsync(PlaceFromNoteDTO dto)
        {
            var note = await _context.Notes.FindAsync(dto.NoteId);
            if (note == null)
            {
                throw new InvalidOperationException($"Note with ID {dto.NoteId} not found.");
            }

            var place = new Place
            {
                Name = dto.Name,
                Notes = new List<Note> { note }
            };

            _context.Places.Add(place);
            await _context.SaveChangesAsync();
            return place;
        }

        public async Task UpdatePlaceAsync(int id, Place place)
        {
            if (_context.Places.Any(x => x.PlaceId == id))
            {
                _context.Places.Update(place);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Unable to process the operation.");
            }
        }

        public async Task DeletePlaceAsync(int id)
        {
            var place = _context.Places.Single(x => x.PlaceId == id);
            if (place != null)
            {
                _context.Places.Remove(place);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Unable to process the operation.");
            }
        }
    }
}
