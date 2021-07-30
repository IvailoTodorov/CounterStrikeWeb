namespace CounterStrikeWeb.Controllers
{
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Players;
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

        public IActionResult All([FromQuery]AllTeamsQueryModel query)
        {
            var teamsQuery = this.data.Teams.AsQueryable();
            var totalTeams = teamsQuery.Count();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                teamsQuery = teamsQuery.Where(t =>
                t.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                t.CoachName.ToLower().Contains(query.SearchTerm.ToLower()) ||
                t.Country.ToLower().Contains(query.SearchTerm.ToLower()));    
            }

            var teams = teamsQuery
                .Skip((query.CurrentPage - 1) * AllTeamsQueryModel.TeamsPerPage)
                .Take(AllTeamsQueryModel.TeamsPerPage)
                .OrderByDescending(x => x.Id)
               .Select(t => new TeamListingViewModel
               {
                   Id = t.Id,
                   Name = t.Name,
                   Logo = t.Logo,
               })
               .ToList();

            query.Teams = teams;
            query.TotalTeams = totalTeams;

            return View(query);
        }

        public IActionResult Details(int Id)
        {
            var team = this.data
                .Teams
                .Find(Id);

            if (team == null)
            {
                return NotFound();
            }

            if (team.Rank == null)
            {
                team.Rank = 0;
            }

            var teamData = new TeamDetailsViewModel
            {
                Id = Id,
                Name = team.Name,
                Country = team.Country,
                CoachName = team.CoachName,
                Logo = team.Logo,
                Rank = team.Rank,
                Players = team.Players,
                AveragePlayersAge = team.Players.Count(),
            };

            return View(teamData);
        }

        public IActionResult FindPlayerToAdd(int id, [FromQuery] AddPlayerToTeamViewModel query) 
        {
            var playersQuery = this.data.Players.AsQueryable();
            var totalPlayers = playersQuery.Count();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                playersQuery = playersQuery.Where(t =>
                t.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                t.InGameName.ToLower().Contains(query.SearchTerm.ToLower()) ||
                t.Country.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            var players = playersQuery
                .Where(p => p.Team == null)
                .Skip((query.CurrentPage - 1) * AddPlayerToTeamViewModel.PlayersPerPage)
                .Take(AddPlayerToTeamViewModel.PlayersPerPage)
                .OrderByDescending(x => x.Id)
                .Select(p => new PlayerListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    InGameName = p.InGameName,
                    Age = p.Age,
                    Picture = p.Picture,
                })
                .ToList();

            query.Players = players;
            query.TotalPlayers = totalPlayers;
            query.TeamId = id;

            return View(query);
        }

        public IActionResult AddPlayerToTeam(int playerId, int teamId)
        {
            var player = this.data.Players.Find(playerId);
            var team = this.data.Teams.Find(teamId);

            player.Team = team;

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
