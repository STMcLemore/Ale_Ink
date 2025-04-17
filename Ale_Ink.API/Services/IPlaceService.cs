using Ale_Ink.Shared.Models;

namespace Ale_Ink.API.Services
{
    public interface IPlaceService
    {
        Task<IEnumerable<Place>> GetAllPlacesAsync();
        Task<Place?> GetPlaceByIdAsync(int id);
        Task<Place> AddPlaceAsync(Place place);
        Task UpdatePlaceAsync(int id, Place place);
        Task DeletePlaceAsync(int id);
    }
}