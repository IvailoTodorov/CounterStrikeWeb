namespace CounterStrikeWeb.Services.Teams
{
    using CounterStrikeWeb.Data.Models;

    public interface ITeamService
    {
        TeamQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int teamsPerPage);

        Team Find(int Id);
    }
}
