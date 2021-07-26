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

    public class MatchController : Controller
    {
        private readonly CounterStrikeDbContext data;

        public MatchController(CounterStrikeDbContext data)
            => this.data = data;

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

            this.data.Matches.Add(matchData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllMatchesQueryModel query)
        {
            var matchesQuery = this.data.Matches.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                matchesQuery = matchesQuery.Where(t =>
                t.FirstTeam.ToLower().Contains(query.SearchTerm.ToLower()) ||
                t.SecondTeam.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            var matches = matchesQuery
               .OrderByDescending(x => x.Id)
               .Select(m => new MatchListingViewModel
               {
                   Id = m.Id,
                   FirstTeam = m.FirstTeam,
                   SecondTeam = m.SecondTeam,
                   StartTime = m.StartTime.ToString("MMMM dd yyyy")
               })
               .ToList();

            query.Matches = matches;

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
