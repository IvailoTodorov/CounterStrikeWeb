namespace CounterStrikeWeb.Controllers
{
    using CounterStrikeWeb.Infrastrucure;
    using CounterStrikeWeb.Models.Players;
    using CounterStrikeWeb.Services.Players;
    using Microsoft.AspNetCore.Mvc;

    public class PlayersController : Controller
    {
        private readonly IPlayerService players;

        public PlayersController(IPlayerService players)
        {
            this.players = players;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(PlayerFormModel player)
        {
            if (!ModelState.IsValid)
            {
                return View(player);
            }

            this.players.Create(
                player.Name,
                player.InGameName,
                player.Age,
                player.Country,
                player.Picture,
                player.InstagramUrl,
                player.TwitterUrl);

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

        public IActionResult Details(int id)
        {
            var playerData = this.players.Details(id);

            if (playerData == null)
            {
                return BadRequest();
            }

            return View(playerData);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var player = this.players.Details(id);

            return View(new PlayerFormModel
            {
                Name = player.Name,
                InGameName = player.InGameName,
                Age = player.Age,
                Country = player.Country,
                Picture = player.Picture,
                InstagramUrl = player.InstagramUrl,
                TwitterUrl = player.TwitterUrl
            });
        }
        [HttpPost]
        public IActionResult Edit(int id, PlayerFormModel playerData)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            var playerIsEdited = this.players.Edit(
            id,
            playerData.Name,
            playerData.InGameName,
            playerData.Age,
            playerData.Country,
            playerData.Picture,
            playerData.InstagramUrl,
            playerData.TwitterUrl);

            if (!playerIsEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}