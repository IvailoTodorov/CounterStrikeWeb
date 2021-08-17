namespace CounterStrikeWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using CounterStrikeWeb.Services.Players.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using static WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly IPlayerService players;
        private readonly IMemoryCache cache;

        public HomeController(
            IPlayerService players,
            IMemoryCache cache)
        {
            this.players = players;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var latestPlayers = this.cache.Get<IEnumerable<PlayerServiceModel>>(LatestPlayersCacheKey);

            if (latestPlayers == null)
            {
                latestPlayers = this.players.Latest();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestPlayersCacheKey, latestPlayers, cacheOptions);
            }


            return View(latestPlayers);
        }

        public IActionResult Error() => View();
    }
}
