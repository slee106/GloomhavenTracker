using System.Collections.Generic;
using GloomhavenTracker.Models.DatabaseModels;

namespace GloomhavenTracker.Services
{
    public interface IItemService
    {
        List<Item> GetItemsWithAdjustedAmounts(List<Item> unadjustedItems, int partyId);
        int CalculateShopDiscount(int partyRepuation);
    }
}