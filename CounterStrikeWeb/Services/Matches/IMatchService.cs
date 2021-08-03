namespace CounterStrikeWeb.Services.Matches
{
    public interface IMatchService
    {
        MatchQueryServiceModel All(string searchTerm);
    }
}
