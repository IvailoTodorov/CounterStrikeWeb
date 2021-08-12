namespace CounterStrikeWeb.Controllers
{
    using System.Linq;
    using AutoMapper;
    using CounterStrikeWeb.Infrastrucure;
    using CounterStrikeWeb.Models.Players;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Teams;
    using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Add() => View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(TeamFormModel team)
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

            var teamData = this.mapper.Map<TeamDetailsViewModel>(team);

            return View(teamData);
        }

        [Authorize]
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

        [Authorize]
        public IActionResult AddPlayerToTeam(int playerId, int teamId)
        {
            this.teams.AddPlayerToTeam(playerId, teamId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            var team = this.teams.Details(id);

            if (team == null)
            {
                return NotFound();
            }

            var teamData = this.mapper.Map<TeamFormModel>(team);

            return View(teamData);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, TeamFormModel teamData)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            var teamIsEdited = this.teams.Edit(
            id,
            teamData.Name,
            teamData.Logo,
            teamData.CoachName,
            teamData.Country);

            if (!teamIsEdited)
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

            this.teams.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
