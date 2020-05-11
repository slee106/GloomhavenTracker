using GloomhavenTracker.Models;
using GloomhavenTracker.Models.DatabaseModels;
using GloomhavenTracker.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Data
{
    public class GloomhavenTrackerContext : IdentityDbContext<ApplicationUser>
    {
        public GloomhavenTrackerContext(DbContextOptions<GloomhavenTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<PartyViewModel> Parties { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}