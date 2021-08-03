namespace CounterStrikeWeb.Services.Events
{
    public interface IEventService
    {
        EventQueryServiceModel FindTeamToAdd(
             string searchTerm,
             int currentPage,
             int teamsPerPage);
    }
}
