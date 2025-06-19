using System.Net.Http.Json;
using Ale_Ink.Shared.DTOs;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.HttpServices
{
    public class ItemHttpService
    {
        private readonly HttpClient _httpClient;

        public ItemHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            var response = await _httpClient.GetAsync("api/Item");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Item>>();
            }
            else
            {
                throw new Exception($"Error fetching items: {response.ReasonPhrase}");
            }
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Item/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Item>();
            }
            else
            {
                throw new Exception($"Error fetching item with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Item", item);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Item>();
            }
            else
            {
                throw new Exception($"Error adding item: {response.ReasonPhrase}");
            }
        }

        public async Task<Item> AddItemFromNoteAsync(ItemFromNoteDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Item/from-note", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Item>();
            }
            else
            {
                throw new Exception($"Error adding item: {response.ReasonPhrase}");
            }
        }


        public async Task UpdateItemAsync(int id, Item item)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Item/{id}", item);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error updating item with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task DeleteItemAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Item/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error deleting item with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Item>> GetItemsByPersonIdAsync(int personId)
        {
            var response = await _httpClient.GetAsync($"api/Item/person/{personId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Item>>();
            }
            else
            {
                throw new Exception($"Error fetching items for person with ID {personId}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Item>> GetItemsByNoteIdAsync(int noteId)
        {
            var response = await _httpClient.GetAsync($"api/Item/note/{noteId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Item>>();
            }
            else
            {
                throw new Exception($"Error fetching items for note with ID {noteId}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Item>> GetItemsByPlaceIdAsync(int placeId)
        {
            var response = await _httpClient.GetAsync($"api/Item/place/{placeId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Item>>();
            }
            else
            {
                throw new Exception($"Error fetching items for place with ID {placeId}: {response.ReasonPhrase}");
            }
        }
    }
}
