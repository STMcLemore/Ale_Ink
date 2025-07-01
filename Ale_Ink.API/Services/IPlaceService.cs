using Ale_Ink.Shared.DTOs;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.API.Services
{
    public interface IPlaceService
    {
        Task<IEnumerable<Place>> GetAllPlacesAsync();
        Task<Place?> GetPlaceByIdAsync(int id);
        Task<Place> AddPlaceFromNoteAsync(PlaceFromNoteDTO dto);
        Task UpdatePlaceAsync(int id, Place place);
        Task DeletePlaceAsync(int id);
    }
}