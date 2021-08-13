namespace CounterStrikeWeb.Test.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using CounterStrikeWeb.Controllers;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Services.Players.Models;
    using CounterStrikeWeb.Test.Mocks;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Players.AddRange(Enumerable.Range(0, 10).Select(i => new Player()));
            data.SaveChanges();

            var playerService = new PlayerService(data, mapper);

            var homeController = new HomeController(playerService);
            // Act
            var result = homeController.Index();

            // Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var playerServiceModel = Assert.IsType<List<PlayerServiceModel>>(model);

            Assert.Equal(5, playerServiceModel.Count);
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(null);

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
