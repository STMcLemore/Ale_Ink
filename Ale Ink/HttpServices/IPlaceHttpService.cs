using Ale_Ink.Shared.DTOs;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.HttpServices
{
    public interface IPlaceHttpService
    {
        Task<List<Place>> GetAllPlacesAsync();
        Task<Place> GetPlaceByIdAsync(int id);
        Task<Place> AddPlaceFromNoteAsync(PlaceFromNoteDTO dto);
        Task UpdatePlaceAsync(int id, Place place);
        Task DeletePlaceAsync(int id);
    }
}
