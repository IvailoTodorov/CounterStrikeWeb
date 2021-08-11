namespace CounterStrikeWeb.Services.Players.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;

    public class PlayerService : IPlayerService
    {
        private readonly CounterStrikeDbContext data;
        private readonly IMapper mapper;

        public PlayerService(CounterStrikeDbContext data, 
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

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
                .ProjectTo<PlayerServiceModel>(this.mapper.ConfigurationProvider)
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
            .ProjectTo<PlayerDetailsServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefault();

        public IEnumerable<PlayerServiceModel> Latest()
            => this.data
              .Players
              .OrderByDescending(x => x.Id)
              .ProjectTo<PlayerServiceModel>(this.mapper.ConfigurationProvider)
              .Take(5)
              .ToList();

        public void Delete(int id)
        {
            var player = this.data.Players.Find(id);

            this.data.Players.Remove(player);
            this.data.SaveChanges();
        }
    }
}
