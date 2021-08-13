namespace CounterStrikeWeb.Controllers
{
    using CounterStrikeWeb.Services.Players.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IPlayerService players;

        public HomeController(IPlayerService players) 
            => this.players = players;

        public IActionResult Index() 
        {
            var players = this.players.Latest();

            return View(players);
        }

        public IActionResult Error() => View();
    }
}
