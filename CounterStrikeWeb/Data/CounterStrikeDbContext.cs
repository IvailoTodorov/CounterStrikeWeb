namespace CounterStrikeWeb.Data
{
    using CounterStrikeWeb.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CounterStrikeDbContext : IdentityDbContext<User>
    {
        public CounterStrikeDbContext(DbContextOptions<CounterStrikeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; init; }

        public DbSet<Match> Matches { get; init; }

        public DbSet<Player> Players { get; init; }

        public DbSet<Team> Teams { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Team>()
                .HasOne(c => c.Event)
                .WithMany(c => c.Teams)
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Match>()
                .HasOne(c => c.Event)
                .WithMany(c => c.Matches)
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Player>()
                .HasOne(c => c.Team)
                .WithMany(c => c.Players)
                .HasForeignKey(c => c.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Team>()
                .HasMany(m => m.Matches)
                .WithMany(t => t.Teams);
                

            base.OnModelCreating(builder);
        }

    }
}
