namespace CounterStrikeWeb.Services.Matches
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Globalization;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Match;
    using CounterStrikeWeb.Models.Matches;

    public class MatchService : IMatchService
    {
        private readonly CounterStrikeDbContext data;

        public MatchService(CounterStrikeDbContext data)
            => this.data = data;

        public void Add(AddMatchFormModel match)
        {
            var firstTeam = this.data.Teams.FirstOrDefault(x => x.Name == match.FirstTeam);
            var secondTeam = this.data.Teams.FirstOrDefault(x => x.Name == match.SecondTeam);

            var matchData = new Match
            {
                FirstTeam = match.FirstTeam,
                SecondTeam = match.SecondTeam,
                StartTime = DateTime.ParseExact(match.StartTime, "MMMM dd yyyy", CultureInfo.InvariantCulture),
            };

            matchData.Teams.Add(firstTeam);
            matchData.Teams.Add(secondTeam);

            this.data.Matches.Add(matchData);

            this.data.SaveChanges();
        }

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

        public MatchDetailsViewModel Details(int Id, string firstTeam, string secondTeam, string startTime)
        {
            var teams = new List<Team>();
            var firstTeamData = this.data.Teams.FirstOrDefault(m => m.Name == firstTeam);
            var secondTeamData = this.data.Teams.FirstOrDefault(m => m.Name == secondTeam);

            teams.Add(firstTeamData);
            teams.Add(secondTeamData);

            var matchData = new MatchDetailsViewModel
            {
                Id = Id,
                StartTime = startTime,
                Teams = teams,
            };

            return matchData;
        }
    }
}
