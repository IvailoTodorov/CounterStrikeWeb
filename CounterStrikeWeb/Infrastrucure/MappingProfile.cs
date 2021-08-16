namespace CounterStrikeWeb.Infrastrucure
{
    using AutoMapper;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Events;
    using CounterStrikeWeb.Models.Match;
    using CounterStrikeWeb.Models.Matches;
    using CounterStrikeWeb.Models.Players;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Teams;
    using CounterStrikeWeb.Services.Players.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Player
            this.CreateMap<Player, PlayerServiceModel>();
            this.CreateMap<Player, PlayerDetailsServiceModel>();
            this.CreateMap<PlayerDetailsServiceModel, PlayerFormModel>();

            // Team
            this.CreateMap<Team, TeamServiceModel>();
            this.CreateMap<Team, TeamDetailsViewModel>();
            this.CreateMap<Team, TeamFormModel>();
            this.CreateMap<TeamDetailsViewModel, TeamFormModel>();

            // Event
            this.CreateMap<Event, EventFormModel>();

            // Match
            this.CreateMap<MatchDetailsViewModel, MatchFormModel>();
        }
    }
}
