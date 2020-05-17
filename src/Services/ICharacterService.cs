using System.Collections.Generic;

namespace GloomhavenTracker.Services
{
    public interface ICharacterService
    {
        int CalculateExperienceBasedOnLevel(int level);
        int CalculateGoldBasedOnLevel(int level);
        List<int> GetAvailableLevels(int partyId);
    }
}