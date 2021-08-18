namespace CounterStrikeWeb.Controllers
{
    using AutoMapper;
    using CounterStrikeWeb.Infrastrucure;
    using CounterStrikeWeb.Models.Match;
    using CounterStrikeWeb.Models.Matches;
    using CounterStrikeWeb.Services.Matches;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MatchController : Controller
    {
        private readonly IMatchService matches;
        private readonly IMapper mapper;

        public MatchController(
            IMatchService matches,
            IMapper mapper)
        {
            this.matches = matches;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add() => View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(MatchFormModel match)
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

        [Authorize]
        [HttpGet]
        public IActionResult Edit(
            int id, 
            string firstTeam,
            string secondTeam,
            string startTime)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            var match = this.matches.Details(id, firstTeam, secondTeam, startTime);

            if (match == null)
            {
                return BadRequest();
            }

            var matchForm = this.mapper.Map<MatchFormModel>(match);

            return View(matchForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, MatchFormModel matchData)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            var matchIsEdited = this.matches.Edit(
            id,
            matchData.FirstTeam,
            matchData.SecondTeam,
            matchData.StartTime);

            if (!matchIsEdited)
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

            this.matches.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
