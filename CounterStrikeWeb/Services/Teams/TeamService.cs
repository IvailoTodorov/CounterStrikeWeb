namespace CounterStrikeWeb.Services.Teams
{
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using System.Linq;

    public class TeamService : ITeamService
    {
        private readonly CounterStrikeDbContext data;

        public TeamService(CounterStrikeDbContext data)
            => this.data = data;

        public TeamQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int teamsPerPage)
        {
            var teamsQuery = this.data.Teams.AsQueryable();
            var totalTeams = teamsQuery.Count();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                teamsQuery = teamsQuery.Where(t =>
                t.Name.ToLower().Contains(searchTerm.ToLower()) ||
                t.CoachName.ToLower().Contains(searchTerm.ToLower()) ||
                t.Country.ToLower().Contains(searchTerm.ToLower()));
            }

            var teams = teamsQuery
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

            return new TeamQueryServiceModel
            {
                CurrentPage = currentPage,
                TeamsPerPage = teamsPerPage,
                TotalTeams = totalTeams,
                Teams = teams
            };
        }

        public Team Find(int Id)
            => this.data
                .Teams
                .Find(Id);
    }
}
