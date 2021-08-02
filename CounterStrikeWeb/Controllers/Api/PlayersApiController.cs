namespace CounterStrikeWeb.Controllers.Api
{
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Models.Api.Players;
    using CounterStrikeWeb.Services.Players;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class PlayersApiController : ControllerBase
    {
        private readonly IPlayerService players;

        public PlayersApiController(IPlayerService players) 
            => this.players = players;

        [HttpGet]
        public PlayerQueryServiceModel All([FromQuery] AllPlayersApiRequestModel query) 
            => this.players.All(
                query.SearchTerm,
                query.CurrentPage,
                query.PlayersPerPage);
    }
}
