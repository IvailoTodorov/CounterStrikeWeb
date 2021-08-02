namespace CounterStrikeWeb.Models.Api.Players
{
    public class AllPlayersApiRequestModel
    {
        public int PlayersPerPage { get; init; } = 4;

        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

    }
}
