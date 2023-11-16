using Microsoft.EntityFrameworkCore;
using ScorecardAPI.Models;

namespace ScorecardAPI
{
    public class ScorecardContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<MatchPreset> MatchPresets { get; set; }

        public ScorecardContext (DbContextOptions<ScorecardContext> options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=scorecard.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
            .HasMany(m => m.Events)
            .WithOne(e => e.Match)
            .HasForeignKey(e => e.MatchId);

            modelBuilder.Entity<MatchEvent>()
                .HasDiscriminator<string>("EventType")
                .HasValue<StrikeEvent>("Strike")
                .HasValue<PenaltyEvent>("Penalty")
                .HasValue<DisqualificationEvent>("Disqualification");

            modelBuilder.Entity<Tournament>()
            .HasMany(m => m.Matches)
            .WithOne(t => t.Tournament)
            .HasForeignKey(t => t.TournamentId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Fighters)
                .WithMany(u => u.Tournaments);
        }
    }
}
