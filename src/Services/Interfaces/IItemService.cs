using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Models.DatabaseModels;
using GloomhavenTracker.Models.ViewModels;

namespace GloomhavenTracker.Services.Interfaces
{
    public interface IItemService
    {
        List<Item> GetItemsWithAdjustedAmounts(IList<Item> unadjustedItems, int partyId);
        int CalculateShopDiscount(int partyRepuation);
        List<ItemIndexDto> GetItemsIndex(int partyId, bool showOnlyAvailable);
    }
}