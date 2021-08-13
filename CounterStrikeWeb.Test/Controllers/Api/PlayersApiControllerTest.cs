namespace CounterStrikeWeb.Test.Controllers.Api
{
    using CounterStrikeWeb.Controllers.Api;
    using CounterStrikeWeb.Models.Api.Players;
    using CounterStrikeWeb.Test.Mocks;
    using Xunit;

    public class PlayersApiControllerTest
    {
        [Fact]
        public void AllShouldReturnAllPlayers()
        {
            // Arrange
            var playerController = new PlayersApiController(PlayerServiceMock.Instance);

            // Act
            var result = playerController.All(new AllPlayersApiRequestModel());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(4, result.PlayersPerPage);
            Assert.Equal(6, result.TotalPlayers);
        }
    }
}
