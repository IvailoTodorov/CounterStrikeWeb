namespace CounterStrikeWeb.Test.Services
{
    using AutoMapper;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Services.Teams;
    using CounterStrikeWeb.Test.Mocks;
    using Moq;
    using Xunit;

    public class TeamServiceTest
    {
        [Fact]
        public void FindShouldReturnTeam()
        {
            // Arrange
            using var data = DatabaseMock.Instance;

            data.Teams.Add(new Team { Id = 1, Name = "test"});
            data.SaveChanges();

            var teamService = new TeamService(data, Mock.Of<IMapper>());

            // Act
            var result = teamService.Find(1);

            // Assert
            Assert.Equal("test", result.Name);
        }
    }
}
