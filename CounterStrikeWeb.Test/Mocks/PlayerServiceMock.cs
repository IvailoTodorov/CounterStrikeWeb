namespace CounterStrikeWeb.Test.Mocks
{
    using CounterStrikeWeb.Services.Players.Models;
    using Moq;

    public static class PlayerServiceMock
    {

        public static IPlayerService Instance
        {
            get
            {
                var playerServiceMock = new Mock<IPlayerService>();

                playerServiceMock
                    .Setup(s => s.All(null, 1, 4))
                    .Returns(new PlayerQueryServiceModel
                    {
                        CurrentPage = 1,
                        PlayersPerPage = 4,
                        TotalPlayers = 6,
                        Players = null
                    });

                return playerServiceMock.Object;
            }
        }

    }
}
