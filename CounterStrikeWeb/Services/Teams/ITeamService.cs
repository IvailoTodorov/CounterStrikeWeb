namespace CounterStrikeWeb.Services.Teams
{
    public interface ITeamService
    {
        TeamQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int teamsPerPage);
    }
}
