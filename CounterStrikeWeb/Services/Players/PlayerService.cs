namespace CounterStrikeWeb.Services.Players
{
    using CounterStrikeWeb.Data;
    using System.Linq;

    public class PlayerService : IPlayerService
    {
        private readonly CounterStrikeDbContext data;

        public PlayerService(CounterStrikeDbContext data)
            => this.data = data;

        public PlayerQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int playersPerPage)
        {
            var playersQuery = this.data.Players.AsQueryable();
            var totalPlayers = playersQuery.Count();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                playersQuery = playersQuery.Where(t =>
                t.Name.ToLower().Contains(searchTerm.ToLower()) ||
                t.InGameName.ToLower().Contains(searchTerm.ToLower()) ||
                t.Country.ToLower().Contains(searchTerm.ToLower()));
            }

            var players = playersQuery
                .Skip((currentPage - 1) * playersPerPage)
                .Take(playersPerPage)
                .OrderByDescending(x => x.Id)
                .Select(p => new PlayerServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    InGameName = p.InGameName,
                    Age = p.Age,
                    Picture = p.Picture,
                })
                .ToList();

            return new PlayerQueryServiceModel
            {
                CurrentPage = currentPage,
                PlayersPerPage = playersPerPage,
                TotalPlayers = totalPlayers,
                Players = players
            };
        }
    }
}
