namespace CounterStrikeWeb.Services.Teams
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Players.Models;

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

        public void Add(TeamFormModel team)
        {
            var teamData = new Team
            {
                Name = team.Name,
                Logo = team.Logo,
                CoachName = team.CoachName,
                Country = team.Country,
                IsPublic = false
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
            string searchTerm = null,
            int currentPage = 1,
            int teamsPerPage = int.MaxValue,
             bool publicOnly = true)
        {
            var teamsQuery = this.data.Teams.
                Where(t => !publicOnly || t.IsPublic);
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
                .ProjectTo<TeamServiceModel>(this.mapper.ConfigurationProvider)
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

        public bool Edit(
            int id,
            string name,
            string logo,
            string coachName,
            string country,
            bool isPublic)
        {
            var team = this.data.Teams.Find(id);

            if (team == null)
            {
                return false;
            }

            team.Name = name;
            team.Logo = logo;
            team.CoachName = coachName;
            team.Country = country;
            team.IsPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }

        public void Delete(int id)
        {
            var team = this.data.Teams.Find(id);

            this.data.Teams.Remove(team);
            this.data.SaveChanges();
        }

        public TeamDetailsViewModel Details(int id)
            => this.data
            .Teams
            .Where(x => x.Id == id)
            .ProjectTo<TeamDetailsViewModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefault();

        public int FindId(string teamName)
        {
            var team = this.data.Teams.Where(t => t.Name == teamName).First();

            return team.Id;
        }

        public void ChangeVisibility(int id)
        {
            var team = this.data.Teams.Find(id);

            team.IsPublic = !team.IsPublic;

            this.data.SaveChanges();
        }
    }
}
