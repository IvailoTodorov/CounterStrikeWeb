namespace CounterStrikeWeb.Models.Players
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllPlayersQueryModel
    {
        public const int PlayersPerPage = 1;

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPlayers { get; set; }

        public IEnumerable<PlayerListingViewModel> Players { get; set; }
    }
}
