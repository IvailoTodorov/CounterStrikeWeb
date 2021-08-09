namespace CounterStrikeWeb.Services.Matches
{
    using CounterStrikeWeb.Models.Match;
    using CounterStrikeWeb.Models.Matches;

    public interface IMatchService
    {
        MatchQueryServiceModel All(string searchTerm);

        void Add(AddMatchFormModel match);

        MatchDetailsViewModel Details(int Id, string firstTeam, string secondTeam, string startTime); 
    }
}
