using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Data;
using GloomhavenTracker.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Services
{
    public class ItemService : IItemService
    {
        private readonly GloomhavenTrackerContext gloomhavenTrackerContext;

        public ItemService(GloomhavenTrackerContext gloomhavenTrackerContext)
        {
            this.gloomhavenTrackerContext = gloomhavenTrackerContext;
        }

        public int CalculateShopDiscount(int partyRepuation)
        {
            switch (partyRepuation)
            {
                case int n when (19 <= n):
                    {
                        return -5;
                    }
                case int n when (15 <= n && n <= 18):
                    {
                        return -4;
                    }
                case int n when (11 <= n && n <= 14):
                    {
                        return -3;
                    }
                case int n when (7 <= n && n <= 10):
                    {
                        return -2;
                    }
                case int n when (3 <= n && n <= 6):
                    {
                        return -1;
                    }
                case int n when (-2 <= n && n <= 2):
                    {
                        return 0;
                    }
                case int n when (-6 <= n && n <= -2):
                    {
                        return 1;
                    }
                case int n when (-10 <= n && n <= 7):
                    {
                        return 2;
                    }
                case int n when (-14 <= n && n <= -11):
                    {
                        return 3;
                    }
                case int n when (-18 <= n && n <= -15):
                    {
                        return 4;
                    }
                case int n when (n <= -19):
                    {
                        return 5;
                    }
            }
            return 0;
        }

        public List<Item> GetItemsWithAdjustedAmounts(List<Item> unadjustedItems, int partyId)
        {
            var items = new List<Item>();
            foreach (var item in unadjustedItems)
            {
                var ite = gloomhavenTrackerContext.Items.Include(x => x.CharacterItems).ThenInclude(x=>x.Character).Single(x => x.Id == item.Id);
                var numberOfItemsUsed = ite.CharacterItems.Count(x=>x.Character.PartyId == partyId);
                var adjustedItem = item;
                adjustedItem.NumberAvailable -= numberOfItemsUsed;
                if (adjustedItem.NumberAvailable > 0)
                {
                    items.Add(adjustedItem);
                }
            }
            return items;
        }
    }
}