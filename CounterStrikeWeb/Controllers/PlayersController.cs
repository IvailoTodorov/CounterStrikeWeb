namespace CounterStrikeWeb.Controllers
{
    using AutoMapper;
    using CounterStrikeWeb.Infrastrucure;
    using CounterStrikeWeb.Models.Players;
    using CounterStrikeWeb.Services.Players.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PlayersController : Controller
    {
        private readonly IPlayerService players;
        private readonly IMapper mapper;

        public PlayersController(
            IPlayerService players,
            IMapper mapper)
        {
            this.players = players;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add() => View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(PlayerFormModel player)
        {
            if (!ModelState.IsValid)
            {
                return View(player);
            }

            var playerId = this.players.Create(
                player.Name,
                player.InGameName,
                player.Age,
                player.Country,
                player.Picture,
                player.InstagramUrl,
                player.TwitterUrl);

            return RedirectToAction(nameof(Details), new { id = playerId, information = player.InGameName });
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

        public IActionResult Details(int id, string information)
        {
            var playerData = this.players.Details(id);

            if (playerData == null)
            {
                return BadRequest();
            }

            if (!information.Contains(playerData.InGameName))
            {
                return BadRequest();
            }

            return View(playerData);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            var player = this.players.Details(id);

            var playerForm = this.mapper.Map<PlayerFormModel>(player);

            return View(playerForm);
        }

        [Authorize]
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
            playerData.TwitterUrl,
            this.User.IsAdmin());

            if (!playerIsEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.players.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}