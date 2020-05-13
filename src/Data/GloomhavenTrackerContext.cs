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
        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PartyUser>()
                   .HasKey(pu => new { pu.PartyId, pu.UserId });

            builder.Entity<PartyUser>()
                   .HasOne(pu => pu.Party)
                   .WithMany(p => p.PartyUsers)
                   .HasForeignKey(pu => pu.PartyId);

            builder.Entity<PartyUser>()
                   .HasOne(pu => pu.User)
                   .WithMany(u => u.PartyUsers)
                   .HasForeignKey(pu => pu.UserId);

            builder.Entity<Character>()
                   .HasOne(c => c.Party)
                   .WithMany(p => p.Characters)
                   .HasForeignKey(c => c.PartyId);

            builder.Entity<CharacterItem>()
                   .HasKey(ci => new { ci.CharacterId, ci.ItemId });

            builder.Entity<CharacterItem>()
                   .HasOne(ci => ci.Character)
                   .WithMany(c => c.CharacterItems)
                   .HasForeignKey(ci => ci.CharacterId);

            builder.Entity<CharacterItem>()
                   .HasOne(ci => ci.Item)
                   .WithMany(i => i.CharacterItems)
                   .HasForeignKey(ci => ci.ItemId);

            base.OnModelCreating(builder);
        }
    }
}