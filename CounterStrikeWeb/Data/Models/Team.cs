namespace CounterStrikeWeb.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

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

        [Required]
        public string CoachName { get; set; }

        [Required]
        public string Country { get; set; }

        public bool IsPublic { get; set; }

        public int? EventId { get; set; }

        public Event Event { get; set; }

        public IEnumerable<Player> Players { get; init; }

        public IEnumerable<Match> Matches { get; init; }
    }
}
