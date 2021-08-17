namespace CounterStrikeWeb.Test.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Match;
    using CounterStrikeWeb.Services.Matches;

    public static class Matches
    {
        public static IEnumerable<Match> TenMatches
            => Enumerable.Range(0, 10).Select(i => new Match());
    }
}
