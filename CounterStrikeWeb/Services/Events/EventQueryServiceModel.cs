namespace CounterStrikeWeb.Services.Events
{
    using CounterStrikeWeb.Services.Teams;
    using System.Collections.Generic;

    public class EventQueryServiceModel
    {
        public int TeamsPerPage { get; init; }

        public int CurrentPage { get; init; }

        public int TotalTeams { get; init; }

        public int EventId { get; init; }

        public IEnumerable<TeamServiceModel> Teams { get; init; }
    }
}
