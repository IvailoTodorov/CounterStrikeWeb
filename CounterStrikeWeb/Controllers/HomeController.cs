namespace CounterStrikeWeb.Controllers
{
    using System.Diagnostics;
    using CounterStrikeWeb.Models;
    using CounterStrikeWeb.Services.Players.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IPlayerService players;

        public HomeController(IPlayerService players)
        {
            this.players = players;
        }

        public IActionResult Index() 
        {
            var players = this.players.Latest();

            return View(players);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
