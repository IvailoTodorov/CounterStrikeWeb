namespace CounterStrikeWeb.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Match;
    using CounterStrikeWeb.Models.Matches;
    using CounterStrikeWeb.Services.Matches;

    public class MatchController : Controller
    {
        private readonly IMatchService matches;
        private readonly CounterStrikeDbContext data;

        public MatchController(IMatchService matches, CounterStrikeDbContext data)
        {
            this.matches = matches;
            this.data = data;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddMatchFormModel match)
        {
            if (!ModelState.IsValid)
            {
                return View(match);
            }

            var firstTeam = this.data.Teams.FirstOrDefault(x => x.Name == match.FirstTeam);
            var secondTeam = this.data.Teams.FirstOrDefault(x => x.Name == match.SecondTeam);

            if (firstTeam == null || secondTeam == null)
            {
                return View(match);
            }

            var matchData = new Match
            {
                FirstTeam = match.FirstTeam,
                SecondTeam = match.SecondTeam,
                StartTime = DateTime.ParseExact(match.StartTime, "MMMM dd yyyy", CultureInfo.InvariantCulture),
            };

            matchData.Teams.Add(firstTeam);
            matchData.Teams.Add(secondTeam);

            this.data.Matches.Add(matchData);

            this.data.SaveChanges();

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
            var teams = new List<Team>();
            var firstTeamData = this.data.Teams.FirstOrDefault(m => m.Name == firstTeam);
            var secondTeamData = this.data.Teams.FirstOrDefault(m => m.Name == secondTeam);

            if (firstTeamData == null || secondTeamData == null)
            {
                return NotFound();
            }

            teams.Add(firstTeamData);
            teams.Add(secondTeamData);

            var matchData = new MatchDetailsViewModel
            {
                Id = Id,
                StartTime = startTime,
                Teams = teams,
            };

            return View(matchData);
        }
    }
}
