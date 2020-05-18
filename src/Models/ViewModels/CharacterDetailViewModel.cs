using System.Collections.Generic;
using GloomhavenTracker.Models.DatabaseModels;

namespace GloomhavenTracker.Models.ViewModels
{
    public class CharacterDetailViewModel
    {
        public Character Character { get; set; }
        public int ExperiencePointsForNextLevel { get; set; }
        public int NumberOfEquippedConsumables { get; set; }
        public int NumberOfFreeConsumableSpaces { get; set; }
    }
}