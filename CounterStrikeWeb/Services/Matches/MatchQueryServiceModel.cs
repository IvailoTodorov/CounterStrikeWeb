namespace CounterStrikeWeb.Services.Matches
{
    using System.Collections.Generic;

    public class MatchQueryServiceModel
    {
        public IEnumerable<MatchServiceModel> Matches { get; init; }
    }
}
