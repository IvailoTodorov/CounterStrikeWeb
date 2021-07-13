namespace CounterStrikeWeb.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.Matches = new List<Match>();
        }

        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Logo { get; set; }

        public int? Rank { get; set; }

        public string CoachName { get; set; }

        public string Country { get; set; }

        public double AveragePlayersAge { get; set; }

        public IEnumerable<Player> Players { get; init; }

        public IEnumerable<Match> Matches { get; init; }
    }
}
