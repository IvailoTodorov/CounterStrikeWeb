namespace CounterStrikeWeb.Models.Teams
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CounterStrikeWeb.Services.Teams;

    public class AllTeamsQueryModel
    {
        public const int TeamsPerPage = 4;

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalTeams { get; set; }

        public IEnumerable<TeamServiceModel> Teams { get; set; }
    }
}
