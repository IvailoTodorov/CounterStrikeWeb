namespace CounterStrikeWeb.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Models;
    using CounterStrikeWeb.Models.Players;

    public class HomeController : Controller
    {
        private readonly CounterStrikeDbContext data;

        public HomeController(CounterStrikeDbContext data)
            => this.data = data;

        public IActionResult Index() 
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
              .Take(5)
              .ToList();

            return View(players);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
