using System.ComponentModel.DataAnnotations.Schema;

namespace GloomhavenTracker.Models.DatabaseModels
{
    [Table("PartyItem")]
    public class PartyItem
    {
        public int PartyId { get; set; }
        public Party Party { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public bool Unlocked { get; set; }
    }
}