using System.Collections.Generic;
using GloomhavenTracker.Models.DatabaseModels;

namespace GloomhavenTracker.Models.ViewModels
{
    public class ItemIndexViewModel
    {
        public List<Item> Items { get; set; }
        public int CharacterId { get; set; }
    }
}