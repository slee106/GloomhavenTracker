using System.Collections.Generic;
using GloomhavenTracker.Models.DatabaseModels;

namespace GloomhavenTracker.Models.ViewModels
{
    public class ItemIndexViewModel
    {
        public Item Item { get; set; }
        public int NumberInShop { get; set; }
        public List<Character> CharactersWithCopy { get; set; }
    }
}