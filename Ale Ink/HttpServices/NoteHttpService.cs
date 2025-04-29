using System.Net.Http.Json;
using Ale_Ink.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.HttpServices
{
    public class NoteHttpService
    {
        private readonly HttpClient _httpClient;

        public NoteHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            var response = await _httpClient.GetAsync("api/Note");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Note>>();
            }
            else
            {
                throw new Exception($"Error fetching notes: {response.ReasonPhrase}");
            }
        }

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Note/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Note>();
            }
            else
            {
                throw new Exception($"Error fetching note with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<Note> AddNoteAsync(Note note)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Note", note);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Note>();
            }
            else
            {
                throw new Exception($"Error adding note: {response.ReasonPhrase}");
            }
        }

        public async Task UpdateNoteAsync(int id, Note note)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Note/{id}", note);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error updating note with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task DeleteNoteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Note/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error deleting note with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Note>> GetNotesByPersonIdAsync(int personId)
        {
            var response = await _httpClient.GetAsync($"api/Note/person/{personId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Note>>();
            }
            else
            {
                throw new Exception($"Error fetching notes for person with ID {personId}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Note>> GetNotesByItemIdAsync(int itemId)
        {
            var response = await _httpClient.GetAsync($"api/Note/item/{itemId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Note>>();
            }
            else
            {
                throw new Exception($"Error fetching notes for item with ID {itemId}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Note>> GetNotesByPlaceIdAsync(int placeId)
        {
            var response = await _httpClient.GetAsync($"api/Note/place/{placeId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Note>>();
            }
            else
            {
                throw new Exception($"Error fetching notes for place with ID {placeId}: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Note>> GetNotesByKeywordAsync(string keyword)
        {
            var response = await _httpClient.GetAsync($"api/Note/search/{keyword}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Note>>();
            }
            else
            {
                throw new Exception($"Error fetching notes with keyword '{keyword}': {response.ReasonPhrase}");
            }
        }
    }
}
