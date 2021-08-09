namespace CounterStrikeWeb.Controllers
{
    using AutoMapper;
    using CounterStrikeWeb.Models.Players;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Teams;
    using Microsoft.AspNetCore.Mvc;

    public class TeamsController : Controller
    {
        private readonly ITeamService teams;
        private readonly IMapper mapper;

        public TeamsController(
            ITeamService teams,
            IMapper mapper)
        {
            this.teams = teams;
            this.mapper = mapper;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddTeamFormModel team)
        {
            if (!ModelState.IsValid)
            {
                return View(team);
            }

            this.teams.Add(team);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllTeamsQueryModel query)
        {
            var queryResult = this.teams.All(
                query.SearchTerm,
                query.CurrentPage,
                AllTeamsQueryModel.TeamsPerPage);

            query.Teams = queryResult.Teams;
            query.TotalTeams = queryResult.TotalTeams;

            return View(query);
        }

        public IActionResult Details(int Id)
        {
            var team = this.teams.Find(Id);

            if (team == null)
            {
                return NotFound();
            }

            if (team.Rank == null)
            {
                team.Rank = 0;
            }

            var teamData = this.mapper.Map<TeamDetailsViewModel>(team);

            return View(teamData);
        }

        public IActionResult FindPlayerToAdd(int id, [FromQuery] AddPlayerToTeamViewModel query)
        {
            var queryResult = this.teams.FindPlayerToAdd(
                query.SearchTerm,
                query.CurrentPage,
                AddPlayerToTeamViewModel.PlayersPerPage);

            query.Players = queryResult.Players;
            query.TotalPlayers = queryResult.TotalPlayers;
            query.TeamId = id;

            return View(query);
        }

        public IActionResult AddPlayerToTeam(int playerId, int teamId)
        {
            this.teams.AddPlayerToTeam(playerId, teamId);

            return RedirectToAction(nameof(All));
        }
    }
}
