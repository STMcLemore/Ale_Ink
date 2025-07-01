using System.Net.Http.Json;
using Ale_Ink.Shared.DTOs;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.HttpServices
{
    public class PlaceHttpService
    {
        private readonly HttpClient _httpClient;

        public PlaceHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Place>> GetAllPlacesAsync()
        {
            var response = await _httpClient.GetAsync("api/Place");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Place>>();
            }
            else
            {
                throw new Exception($"Error fetching places: {response.ReasonPhrase}");
            }
        }

        public async Task<Place> GetPlaceByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Place/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Place>();
            }
            else
            {
                throw new Exception($"Error fetching place with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<Place> AddPlaceFromNoteAsync(PlaceFromNoteDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Place/from-note", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Place>();
            }
            else
            {
                throw new Exception($"Error adding place from note: {response.ReasonPhrase}");
            }
        }

        public async Task UpdatePlaceAsync(int id, Place place)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Place/{id}", place);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error updating place with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task DeletePlaceAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Place/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error deleting place with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Place>> GetPlacesByPersonIdAsync(int personId)
        {
            var response = await _httpClient.GetAsync($"api/Place/person/{personId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Place>>();
            }
            else
            {
                throw new Exception($"Error fetching places for person with ID {personId}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Place>> GetPlacesByItemIdAsync(int itemId)
        {
            var response = await _httpClient.GetAsync($"api/Place/item/{itemId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Place>>();
            }
            else
            {
                throw new Exception($"Error fetching places for item with ID {itemId}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Place>> GetPlacesByNoteIdAsync(int noteId)
        {
            var response = await _httpClient.GetAsync($"api/Place/note/{noteId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Place>>();
            }
            else
            {
                throw new Exception($"Error fetching places for note with ID {noteId}: {response.ReasonPhrase}");
            }
        }
    }
}
