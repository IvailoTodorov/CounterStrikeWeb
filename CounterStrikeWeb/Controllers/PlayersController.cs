﻿namespace CounterStrikeWeb.Controllers
{
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

            return RedirectToAction("Index", "Home");
        }
    }
}
