namespace CounterStrikeWeb.Test.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Teams;
    using CounterStrikeWeb.Data.Models;

    public static class Teams
    {
        public static IEnumerable<TeamServiceModel> TenPublicTeams
            => Enumerable.Range(0, 10).Select(i => new TeamServiceModel
            {
                IsPublic = true
            });

        public static IEnumerable<Team> TenTeams
            => Enumerable.Range(0, 10).Select(i => new Team
            {
            });

        public static IEnumerable<Team> TwoTeams()
        {
            var teams = new List<Team>();

            var first = new Team { Name = "TestFirst" };
            var second = new Team { Name = "TestSecond" };

            teams.Add(first);
            teams.Add(second);

            return teams;
        }
    }
}
