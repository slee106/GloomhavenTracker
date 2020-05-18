using System.ComponentModel.DataAnnotations.Schema;

namespace GloomhavenTracker.Models.DatabaseModels
{
    [Table("CharacterItem")]
    public class CharacterItem
    {
        public int CharacterId { get; set; }
        public int ItemId { get; set; }
        public Character Character { get; set; }
        public Item Item { get; set; }
        public bool Equipped { get; set; }
    }
}