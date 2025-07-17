using Ale_Ink.Shared.DTOs;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.HttpServices
{
    public class NoteAssignmentService
    {
        private readonly IItemHttpService _itemService;
        private readonly IPersonHttpService _personService;
        private readonly IPlaceHttpService _placeService;

        private string EntityName = "";
        private string SelectedType = "";
        private string? ErrorMessage;
        private Note? NoteToAssign;

        public NoteAssignmentService(IItemHttpService itemService, IPersonHttpService personService, IPlaceHttpService placeService)
        {
            _itemService = itemService;
            _personService = personService;
            _placeService = placeService;
        }

        public async Task AssignEntityToNoteAsync(string type, string name, int noteId)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type) || noteId <= 0)
            {
                ErrorMessage = "Name and Note ID must be provided.";
                return;
            }

            ErrorMessage = null;

            try
            {

                switch (type)
                {
                    case "Person":
                        await _personService.AddPersonFromNoteAsync(new PersonFromNoteDTO { Name = name, NoteId = noteId });
                        break;
                    case "Item":
                        await _itemService.AddItemFromNoteAsync(new ItemFromNoteDTO { Name = name, NoteId = noteId });
                        break;
                    case "Place":
                        await _placeService.AddPlaceFromNoteAsync(new PlaceFromNoteDTO { Name = name, NoteId = noteId });
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error assigning {type} to note: {ex.Message}";
            }
        }
    }
}
