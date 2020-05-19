using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Models.DatabaseModels;

namespace GloomhavenTracker.Models.ViewModels
{
    public class PartyCreateViewModel
    {
        [Display(Name = "Users in party")]
        public List<ApplicationUserSelected> Users { get; set; }
        public Party Party { get; set; }
    }

    public class ApplicationUserSelected
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public bool Selected { get; set; }
    }

}