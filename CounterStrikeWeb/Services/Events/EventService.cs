namespace CounterStrikeWeb.Services.Events
{
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Services.Teams;
    using System.Linq;

    public class EventService : IEventService
    {
        private readonly CounterStrikeDbContext data;

        public EventService(CounterStrikeDbContext data)
            => this.data = data;

        public EventQueryServiceModel FindTeamToAdd(
            string searchTerm,
            int currentPage,
            int teamsPerPage)
        {
            var teamsQuery = this.data.Teams.AsQueryable();
            var totalTeams = teamsQuery.Count();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                teamsQuery = teamsQuery.Where(t =>
                t.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var teams = teamsQuery
                .Where(t => t.Event == null)
                .Skip((currentPage - 1) * teamsPerPage)
                .Take(teamsPerPage)
                .OrderByDescending(x => x.Id)
                .Select(t => new TeamServiceModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Logo = t.Logo,
                })
                .ToList();

            return new EventQueryServiceModel
            {
                CurrentPage = currentPage,
                TeamsPerPage = teamsPerPage,
                TotalTeams = totalTeams,
                Teams = teams,
            };
        }
    }
}
