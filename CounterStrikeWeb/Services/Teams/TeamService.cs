namespace CounterStrikeWeb.Services.Teams
{
    using CounterStrikeWeb.Data;
    using System.Linq;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Models.Players;
    using AutoMapper.QueryableExtensions;
    using CounterStrikeWeb.Services.Players.Models;
    using AutoMapper;

    public class TeamService : ITeamService
    {
        private readonly CounterStrikeDbContext data;
        private readonly IMapper mapper;

        public TeamService(CounterStrikeDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(AddTeamFormModel team)
        {
            var teamData = new Team
            {
                Name = team.Name,
                Logo = team.Logo,
                CoachName = team.CoachName,
                Country = team.Country,
            };

            this.data.Teams.Add(teamData);

            this.data.SaveChanges();
        }

        public void AddPlayerToTeam(int playerId, int teamId)
        {
            var player = this.data.Players.Find(playerId);
            var team = this.data.Teams.Find(teamId);

            player.Team = team;

            this.data.SaveChanges();
        }

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
        public PlayerQueryServiceModel FindPlayerToAdd(
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
                .Where(p => p.Team == null)
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
                Players = players,
            };
        }

        public Team Find(int Id)
            => this.data
                .Teams
                .Find(Id);
    }
}
