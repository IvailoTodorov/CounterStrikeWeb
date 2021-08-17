﻿namespace CounterStrikeWeb.Test.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using CounterStrikeWeb.Data.Models;

    public static class Players
    {
        public static IEnumerable<Player> TenPublicPlayers
            => Enumerable.Range(0, 10).Select(i => new Player
            {
                Name = "test",
                InGameName = "test",
                IsPublic = true
            });
    }
}
