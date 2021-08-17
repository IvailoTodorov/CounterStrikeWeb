namespace CounterStrikeWeb.Test.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using CounterStrikeWeb.Data.Models;

    public static class Events
    {
        public static IEnumerable<Event> TenEvents
            => Enumerable.Range(0, 10).Select(i => new Event());
    }
}
