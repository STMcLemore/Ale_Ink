using System.Net.Http.Json;
using Ale_Ink.Shared.DTOs;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.HttpServices
{
    public class PersonHttpService : IPersonHttpService
    {
        private readonly HttpClient _httpClient;

        public PersonHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            var response = await _httpClient.GetAsync("api/Person");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Person>>();
            }
            else
            {
                throw new Exception($"Error fetching persons: {response.ReasonPhrase}");
            }
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Person/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Person>();
            }
            else
            {
                throw new Exception($"Error fetching person with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<Person> AddPersonFromNoteAsync(PersonFromNoteDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Person/from-note", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Person>();
            }
            else
            {
                throw new Exception($"Error adding person from note: {response.ReasonPhrase}");
            }
        }

        public async Task UpdatePersonAsync(int id, Person person)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Person/{id}", person);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error updating person with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task DeletePersonAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Person/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error deleting person with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Person>> GetAllPeopleByNoteIdAsync(int noteId)
        {
            var response = await _httpClient.GetAsync($"api/Person/Note/{noteId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Person>>();
            }
            else
            {
                throw new Exception($"Error fetching persons by note ID {noteId}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Person>> GetAllPeopleByItemIdAsync(int itemId)
        {
            var response = await _httpClient.GetAsync($"api/Person/Item/{itemId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Person>>();
            }
            else
            {
                throw new Exception($"Error fetching persons by item ID {itemId}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Person>> GetAllPeopleByPlaceIdAsync(int placeId)
        {
            var response = await _httpClient.GetAsync($"api/Person/Place/{placeId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Person>>();
            }
            else
            {
                throw new Exception($"Error fetching persons by place ID {placeId}: {response.ReasonPhrase}");
            }
        }
    }
}
