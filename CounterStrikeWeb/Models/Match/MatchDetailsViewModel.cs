namespace CounterStrikeWeb.Models.Match
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CounterStrikeWeb.Data.Models;

    public class MatchDetailsViewModel
    {
        public int Id { get; init; }

        [Display(Name = "Start On")]
        public string StartTime { get; init; }

        public IEnumerable<Team> Teams { get; init; } = new List<Team>();
    }
}
