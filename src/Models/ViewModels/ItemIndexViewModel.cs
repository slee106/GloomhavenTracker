using System.Collections.Generic;

namespace GloomhavenTracker.Models.ViewModels
{
    public class ItemIndexViewModel
    {
        public int PartyId { get; set; }
        public List<ItemIndexDto> ItemIndexDtos { get; set; }
    }
}