namespace Ale_Ink.API.Services
{
    public interface INoteAssignmentService
    {
        Task AssignNoteAsync(int noteId, string type, string name);
    }
}
