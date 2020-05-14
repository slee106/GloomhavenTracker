using GloomhavenTracker.Models.DatabaseModels;

namespace GloomhavenTracker.Models.ViewModels
{
    public class ItemDetailViewModel
    {
        public int CharacterId { get; set; }
        public Item Item { get; set; }
    }
}