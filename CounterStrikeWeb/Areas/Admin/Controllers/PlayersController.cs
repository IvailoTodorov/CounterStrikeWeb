using Microsoft.AspNetCore.Mvc;

namespace CounterStrikeWeb.Areas.Admin.Controllers
{
    public class PlayersController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
