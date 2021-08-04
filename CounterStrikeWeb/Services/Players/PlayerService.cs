namespace CounterStrikeWeb.Services.Players
{
    using System.Linq;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;

    public class PlayerService : IPlayerService
    {
        private readonly CounterStrikeDbContext data;

        public PlayerService(CounterStrikeDbContext data)
            => this.data = data;

        public int Create(
            string name,
            string inGameName,
            int age,
            string country,
            string picture,
            string instagramUrl,
            string twitterUrl)
        {
            var playerData = new Player
            {
                Name = name,
                InGameName = inGameName,
                Age = age,
                Country = country,
                Picture = picture,
                InstagramUrl = instagramUrl,
                TwitterUrl = twitterUrl,
            };

            this.data.Players.Add(playerData);
            this.data.SaveChanges();

            return playerData.Id;
        }

        public bool Edit(
            int id,
            string name,
            string inGameName,
            int age,
            string country,
            string picture,
            string instagramUrl,
            string twitterUrl)
        {
            var player = this.data.Players.Find(id);

            if (player == null) // fix with administrator
            {
                return false;
            }

            player.Name = name;
            player.InGameName = inGameName;
            player.Age = age;
            player.Country = country;
            player.Picture = picture;
            player.InstagramUrl = instagramUrl;
            player.TwitterUrl = twitterUrl;
            

            this.data.SaveChanges();

            return true;
        }

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

        public PlayerDetailsServiceModel Details(int id)
        => this.data
            .Players
            .Where(x => x.Id == id)
            .Select(p => new PlayerDetailsServiceModel
            {
                Id = p.Id,
                Name = p.Name,
                InGameName = p.InGameName,
                Country = p.Country,
                Age = p.Age,
                Picture = p.Picture,
                InstagramUrl = p.InstagramUrl,
                TwitterUrl = p.TwitterUrl,
            })
            .FirstOrDefault();
            
    }
}
