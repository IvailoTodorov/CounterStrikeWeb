namespace CounterStrikeWeb.Models.Teams
{
    using System.Collections.Generic;

    public class AllTeamsViewModel
    {
        public string SearchTerm { get; init; }
        public IEnumerable<TeamListingViewModel> Teams { get; init; }
    }
}
