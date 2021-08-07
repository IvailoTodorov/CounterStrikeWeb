namespace CounterStrikeWeb.Models.Players
{
    using CounterStrikeWeb.Services.Players.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddPlayerToTeamViewModel
    {
        public const int PlayersPerPage = 4;

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPlayers { get; set; }

        public int TeamId { get; set; }

        public IEnumerable<PlayerServiceModel> Players { get; set; }
    }
}
