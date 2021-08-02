namespace CounterStrikeWeb.Controllers
{
    using System.Linq;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Players;
    using CounterStrikeWeb.Services.Players;
    using Microsoft.AspNetCore.Mvc;

    public class PlayersController : Controller
    {
        private readonly IPlayerService players;
        private readonly CounterStrikeDbContext data;

        public PlayersController(IPlayerService players, CounterStrikeDbContext data) 
        {
            this.players = players;
            this.data = data;
        }

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

        public IActionResult All([FromQuery] AllPlayersQueryModel query)
        {
            var queryResult = this.players.All(
                query.SearchTerm,
                query.CurrentPage,
                AllPlayersQueryModel.PlayersPerPage);

            query.TotalPlayers = queryResult.TotalPlayers;
            query.Players = queryResult.Players;

            return View(query);
        }

        public IActionResult Details(int Id)
        {
            var player = this.data
                .Players
                .Find(Id);

            if (player == null)
            {
                return NotFound();
            }

            var playerData = new PlayerDetailsViewModel
            {
                Id = Id,
                Name = player.Name,
                InGameName = player.InGameName,
                Age = player.Age,
                Country = player.Country,
                Picture = player.Picture,
                InstagramUrl = player.InstagramUrl,
                TwitterUrl = player.TwitterUrl,
                //TeamName = player.Team.Name,
                //TeamLogo = player.Team.Logo,
            };

            return View(playerData);
        }

        public IActionResult Mine(int Id)
        {
            return View();
        }
    }
}
