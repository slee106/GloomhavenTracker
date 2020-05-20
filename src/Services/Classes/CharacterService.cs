using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Data;
using GloomhavenTracker.Services.Interfaces;

namespace GloomhavenTracker.Services.Classes
{
    public class CharacterService : ICharacterService
    {
        private readonly IServiceProvider provider;
        public CharacterService(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public List<int> GetAvailableLevels(int partyId)
        {
            using (var gloomhavenTrackerContext = (GloomhavenTrackerContext)provider.GetService(typeof(GloomhavenTrackerContext)))
            {
                var prosperity = gloomhavenTrackerContext.Parties.Single(x => x.Id == partyId).Prosperity;
                var listOfAvailableLevels = new List<int>();
                for (int i = 1; i <= prosperity; i++)
                {
                    listOfAvailableLevels.Add(i);
                }
                return listOfAvailableLevels;
            }
        }

        public int CalculateExperienceBasedOnLevel(int level)
        {
            var list = new List<int>();
            for (int i = 1; i <= level; i++)
            {
                var x = i - 2;
                if (x < 0)
                {
                    x = 0;
                }
                list.Add(x);
            }
            return 45 * (level - 1) + 5 * list.Sum();
        }

        public int CalculateGoldBasedOnLevel(int level)
        {
            return 15 * (level + 1);
        }
    }
}