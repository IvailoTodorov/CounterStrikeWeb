﻿namespace CounterStrikeWeb.Infrastrucure
{
    using AutoMapper;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Events;
    using CounterStrikeWeb.Models.Players;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Players.Models;
    using CounterStrikeWeb.Services.Teams;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Player, PlayerServiceModel>();
            this.CreateMap<Player, PlayerDetailsServiceModel>();
            this.CreateMap<PlayerDetailsServiceModel, PlayerFormModel>();
            this.CreateMap<Team, TeamServiceModel>();
            this.CreateMap<Team, TeamDetailsViewModel>();
        }
    }
}
