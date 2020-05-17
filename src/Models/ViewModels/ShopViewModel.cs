using System.Collections.Generic;
using GloomhavenTracker.Models.DatabaseModels;

namespace GloomhavenTracker.Models.ViewModels
{
    public class ShopViewModel
    {
        public List<Item> Items { get; set; }
        public int CharacterId { get; set; }
        public int PartyDiscount { get; set; }
    }
}