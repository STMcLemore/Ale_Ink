namespace Ale_Ink.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public int NoteId { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<Place> Places { get; set; } = new List<Place>();
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
