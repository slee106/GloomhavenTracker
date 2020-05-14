using System.Collections.Generic;
using GloomhavenTracker.Models.DatabaseModels;

namespace GloomhavenTracker.Models.ViewModels
{
    public class CharacterDetailViewModel
    {
        public Character Character { get; set; }
        public List<Item> Items { get; set; }
    }
}