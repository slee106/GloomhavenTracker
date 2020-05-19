using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Data;
using GloomhavenTracker.Models.DatabaseModels;
using GloomhavenTracker.Services.Interfaces;

namespace GloomhavenTracker.Services.Classes
{
    public class PartyService : IPartyService
    {
        private readonly GloomhavenTrackerContext gloomhavenTrackerContext;

        public PartyService(GloomhavenTrackerContext gloomhavenTrackerContext)
        {
            this.gloomhavenTrackerContext = gloomhavenTrackerContext;
        }

        public void AddPartyItems(int partyId, decimal partyProsperity)
        {
            var listOfItemsForProsperity = gloomhavenTrackerContext.Items.Where(x => x.Prosperity <= partyProsperity).ToList();

            var partyItems = new List<PartyItem>();
            foreach (var item in listOfItemsForProsperity)
            {
                partyItems.Add(new PartyItem()
                {
                    ItemId = item.Id,
                    PartyId = partyId,
                    Unlocked = true
                });
            }
            gloomhavenTrackerContext.PartyItems.AddRange(partyItems);
            gloomhavenTrackerContext.SaveChanges();
        }
    }
}