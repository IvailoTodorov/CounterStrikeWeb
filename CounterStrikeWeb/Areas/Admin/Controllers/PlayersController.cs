﻿using CounterStrikeWeb.Services.Players.Models;
using Microsoft.AspNetCore.Mvc;

namespace CounterStrikeWeb.Areas.Admin.Controllers
{
    public class PlayersController : AdminController
    {
        private readonly IPlayerService players;

        public PlayersController(IPlayerService players) 
            => this.players = players;

        public IActionResult All() => View(this.players.All(publicOnly: false).Players);

        public IActionResult ChangeVisibility(int id)
        {
            this.players.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
