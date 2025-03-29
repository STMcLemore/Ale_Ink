namespace Ale_Ink.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<Person> People { get; set; } = new List<Person>();
        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}
