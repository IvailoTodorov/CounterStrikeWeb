namespace CounterStrikeWeb.Services.Players.Models
{
    using System.Collections.Generic;

    public interface IPlayerService
    {
        PlayerQueryServiceModel All(
            string searchTerm = null,
            int currentPage = 1,
            int playersPerPage = int.MaxValue,
            bool publicOnly = true);

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
                string twitterUrl,
                bool isPublic);

        void ChangeVisibility(int id);

        PlayerDetailsServiceModel Details(int id);

        void Delete(int id);

        IEnumerable<PlayerServiceModel> Latest();

    }
}
