namespace CounterStrikeWeb.Areas.Admin.Controllers
{
    using CounterStrikeWeb.Services.Teams;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstrants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class TeamsController : AdminController
    {
        private readonly ITeamService teams;

        public TeamsController(ITeamService teams) 
            => this.teams = teams;

        public IActionResult All() => View(this.teams.All(publicOnly: false).Teams);

        public IActionResult ChangeVisibility(int id)
        {
            this.teams.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
