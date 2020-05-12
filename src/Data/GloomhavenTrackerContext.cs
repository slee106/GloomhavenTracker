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

        public DbSet<Party> Parties { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<PartyUser> PartyUsers {get;set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
}