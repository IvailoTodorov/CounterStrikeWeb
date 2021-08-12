namespace CounterStrikeWeb.Services.Matches
{
    using CounterStrikeWeb.Models.Match;
    using CounterStrikeWeb.Models.Matches;

    public interface IMatchService
    {
        MatchQueryServiceModel All(string searchTerm);

        void Add(MatchFormModel match);

        MatchDetailsViewModel Details(
            int id,
            string firstTeam,
            string secondTeam,
            string startTime);

        bool Edit(
            int id, 
            string firstTeam,
            string secondTeam,
            string startTime);

        void Delete(int id);
    }
}
