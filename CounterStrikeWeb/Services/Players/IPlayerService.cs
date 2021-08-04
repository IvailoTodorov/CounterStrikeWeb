namespace CounterStrikeWeb.Services.Players
{
    public interface IPlayerService
    {
        PlayerQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int playersPerPage);

        int Create(
                string name,
                string inGameName,
                int age,
                string country,
                string picture,
                string instagramUrl,
                string twitterUrl);

        bool Edit(
                int id,
                string name,
                string inGameName,
                int age,
                string country,
                string picture,
                string instagramUrl,
                string twitterUrl);
        PlayerDetailsServiceModel Details(int id);

    }
}
