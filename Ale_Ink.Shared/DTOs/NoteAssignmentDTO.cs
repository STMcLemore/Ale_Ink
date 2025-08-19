using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ale_Ink.Shared.DTOs
{
    public class NoteAssignmentDTO
    {
        public string Type { get; set; } // "Place", "Person", or "Item"
        public string Name { get; set; } // Name of the entity to assign
        public int NoteId { get; set; } // ID of the note to which the entity is assigned
    }
}
