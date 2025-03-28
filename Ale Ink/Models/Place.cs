namespace Ale_Ink.Models
{
    public class Place
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public int NoteId { get; set; }
        public string? Description { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<Person> People { get; set; } = new List<Person>();
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
