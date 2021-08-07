namespace CounterStrikeWeb.Services.Events
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Services.Teams;
    using System.Linq;

    public class EventService : IEventService
    {
        private readonly CounterStrikeDbContext data;
        private readonly IMapper mapper;

        public EventService(CounterStrikeDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

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
                .ProjectTo<TeamServiceModel>(this.mapper.ConfigurationProvider)
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
