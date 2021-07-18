namespace CounterStrikeWeb.Controllers
{
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Teams;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class TeamsController : Controller
    {
        private readonly CounterStrikeDbContext data;

        public TeamsController(CounterStrikeDbContext data)
           => this.data = data;

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddTeamFormModel team)
        {
            if (!ModelState.IsValid)
            {
                return View(team);
            }

            var teamData = new Team
            {
                Name = team.Name,
                Logo = team.Logo,
                CoachName = team.CoachName,
                Country = team.Country,
            };

            this.data.Teams.Add(teamData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            var teams = this.data
               .Teams
               .OrderByDescending(x => x.Id)
               .Select(t => new TeamListingViewModel
               {
                   Id = t.Id,
                   Name = t.Name,
                   Logo = t.Logo,
               })
               .ToList();

            return View(teams);
        }
    }
}
