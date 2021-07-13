namespace CounterStrikeWeb.Data
{
    using CounterStrikeWeb.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CounterStrikeDbContext : IdentityDbContext
    {
        public CounterStrikeDbContext(DbContextOptions<CounterStrikeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; init; }

        public DbSet<Match> Matches { get; init; }

        public DbSet<Player> Players { get; init; }

        public DbSet<Team> Teams { get; init; }

      
    }
}
