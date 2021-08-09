namespace CounterStrikeWeb.Services.Players.Models
{
    using System.Collections.Generic;

    public class PlayerQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int PlayersPerPage { get; init; }

        public int TotalPlayers { get; init; }

        public IEnumerable<PlayerServiceModel> Players { get; init; }
    }
}
