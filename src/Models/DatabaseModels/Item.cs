using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GloomhavenTracker.Models.DatabaseModels
{
    [Table("Item")]
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int NumberAvailable { get; set; }
        public bool Available { get; set; }
        public ICollection<CharacterItem> CharacterItems { get; set; }
    }
}