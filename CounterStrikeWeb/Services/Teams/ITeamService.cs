﻿namespace CounterStrikeWeb.Services.Teams
{
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Players.Models;

    public interface ITeamService
    {
        TeamQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int teamsPerPage);

        Team Find(int Id);

        void Add(TeamFormModel team);

        TeamDetailsViewModel Details(int id);

        bool Edit(
            int id,
            string name,
            string logo,
            string coachName,
            string country);

        void Delete(int id);

        void AddPlayerToTeam(
            int playerId,
            int teamId);

        PlayerQueryServiceModel FindPlayerToAdd(
            string searchTerm,
            int currentPage,
            int playersPerPage);
    }
}
