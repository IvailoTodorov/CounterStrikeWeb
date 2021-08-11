namespace CounterStrikeWeb.Controllers
{
    using CounterStrikeWeb.Models.Match;
    using CounterStrikeWeb.Models.Matches;
    using CounterStrikeWeb.Services.Matches;
    using Microsoft.AspNetCore.Mvc;

    public class MatchController : Controller
    {
        private readonly IMatchService matches;

        public MatchController(IMatchService matches) 
            => this.matches = matches;

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddMatchFormModel match)
        {
            if (!ModelState.IsValid)
            {
                return View(match);
            }

            this.matches.Add(match);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllMatchesQueryModel query)
        {
            var queryResult = this.matches.All(query.SearchTerm);

            query.Matches = queryResult.Matches;

            return View(query);
        }

        public IActionResult Details(int Id, string firstTeam, string secondTeam, string startTime)
        {
            var matchData = this.matches.Details(Id, firstTeam, secondTeam, startTime);

            return View(matchData);
        }
    }
}
