namespace CounterStrikeWeb.Controllers
{
    using System.Linq;
    using System.Diagnostics;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Models;
    using CounterStrikeWeb.Services.Players.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly CounterStrikeDbContext data;
        private readonly IMapper mapper;

        public HomeController(CounterStrikeDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IActionResult Index() 
        {
            var players = this.data
              .Players
              .OrderByDescending(x => x.Id)
              .ProjectTo<PlayerServiceModel>(this.mapper.ConfigurationProvider)
              .Take(5)
              .ToList();

            return View(players);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
