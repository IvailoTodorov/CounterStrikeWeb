namespace CounterStrikeWeb.Services.Matches
{
    using CounterStrikeWeb.Data;
    using System.Linq;

    public class MatchService : IMatchService
    {
        private readonly CounterStrikeDbContext data;

        public MatchService(CounterStrikeDbContext data)
            => this.data = data;

        public MatchQueryServiceModel All(string searchTerm)
        {
            var matchesQuery = this.data.Matches.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                matchesQuery = matchesQuery.Where(t =>
                t.FirstTeam.ToLower().Contains(searchTerm.ToLower()) ||
                t.SecondTeam.ToLower().Contains(searchTerm.ToLower()));
            }

            var matches = matchesQuery
               .OrderByDescending(x => x.Id)
               .Select(m => new MatchServiceModel
               {
                   Id = m.Id,
                   FirstTeam = m.FirstTeam,
                   SecondTeam = m.SecondTeam,
                   StartTime = m.StartTime.ToString("MMMM dd yyyy")
               })
               .ToList();

            return new MatchQueryServiceModel
            {
                Matches = matches
            };
        }
    }
}
