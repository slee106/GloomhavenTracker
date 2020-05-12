using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Models.DatabaseModels;

namespace GloomhavenTracker.Models
{
    public class PartyCreateViewModel : Party
    {
        [Display(Name = "Users in party")]
        public List<ApplicationUserSelected> Users { get; set; }
    }

    public class ApplicationUserSelected
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public bool Selected { get; set; }
    }

}