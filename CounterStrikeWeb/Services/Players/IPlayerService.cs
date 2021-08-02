namespace CounterStrikeWeb.Services.Players
{
    public interface IPlayerService
    {
        PlayerQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int playersPerPage);

    }
}
