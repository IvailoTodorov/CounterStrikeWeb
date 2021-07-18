namespace CounterStrikeWeb.Controllers
{
    using System.Linq;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Players;
    using Microsoft.AspNetCore.Mvc;

    public class PlayersController : Controller
    {
        private readonly CounterStrikeDbContext data;

        public PlayersController(CounterStrikeDbContext data) 
            => this.data = data;

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddPlayerFormModel player)
        {
            if (!ModelState.IsValid)
            {
                return View(player);
            }

            var playerData = new Player
            {
                Name = player.Name,
                InGameName = player.InGameName,
                Age = player.Age,
                Country = player.Country,
                Picture = player.Picture,
                InstagramUrl = player.InstagramUrl,
                TwitterUrl = player.TwitterUrl,
            };

            this.data.Players.Add(playerData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            var players = this.data
                .Players
                .OrderByDescending(x => x.Id)
                .Select(p => new PlayerListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    InGameName = p.InGameName,
                    Age = p.Age,
                    Picture = p.Picture,
                })
                .ToList();

            return View(players);
        }

        public IActionResult Details(int PlayerId)
        {
            var player = this.data
                .Players
                .Find(PlayerId);

            if (player == null)
            {
                return NotFound();
            }

            var playerData = new PlayerDetailsViewModel
            {
                Name = player.Name,
                InGameName = player.InGameName,
                Age = player.Age,
                Country = player.Country,
                Picture = player.Picture,
                InstagramUrl = player.InstagramUrl,
                TwitterUrl = player.TwitterUrl,
                TeamName = player.Team.Name,
                TeamLogo = player.Team.Logo,
            };

            return View(playerData);
        }
    }
}
