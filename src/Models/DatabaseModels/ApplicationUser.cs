using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GloomhavenTracker.Models.DatabaseModels
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<PartyUser> PartyUsers { get; set; }
    }
}