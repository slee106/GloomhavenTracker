using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GloomhavenTracker.Models.ViewModels
{
    public class CharacterCreateViewModel : Character
    {
        public int? PartyId { get; set; }
    }
}