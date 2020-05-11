using System.Collections.Generic;
using GloomhavenTracker.Models.DatabaseModels;

namespace GloomhavenTracker.Models.ViewModels
{
    public class CharacterViewModel
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public List<Character> Characters { get; set; }
    }
}