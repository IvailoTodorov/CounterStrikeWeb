namespace CounterStrikeWeb.Models.Teams
{
    using System.Collections.Generic;
    using CounterStrikeWeb.Data.Models;

    public class TeamDetailsViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Logo { get; init; }

        public int? Rank { get; init; }

        public string CoachName { get; init; }

        public string Country { get; init; }

        public double AveragePlayersAge { get; init; }

        public IEnumerable<Player> Players { get; init; }
    }
}
