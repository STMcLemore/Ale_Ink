namespace Ale_Ink.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Person> People { get; set; } = new List<Person>();
        public ICollection<Place> Places { get; set; } = new List<Place>();
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
