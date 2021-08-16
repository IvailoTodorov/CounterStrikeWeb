namespace CounterStrikeWeb.Services.Teams
{
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Players.Models;

    public interface ITeamService
    {
        TeamQueryServiceModel All(
            string searchTerm = null,
            int currentPage = 1,
            int teamsPerPage = int.MaxValue,
             bool publicOnly = true);

        Team Find(int Id);

        int FindId(string teamName);

        void Add(TeamFormModel team);

        TeamDetailsViewModel Details(int id);

        bool Edit(
            int id,
            string name,
            string logo,
            string coachName,
            string country,
            bool isPublic);

        void Delete(int id);

        void AddPlayerToTeam(
            int playerId,
            int teamId);

        PlayerQueryServiceModel FindPlayerToAdd(
            string searchTerm,
            int currentPage,
            int playersPerPage);

        void ChangeVisibility(int id);
    }
}
