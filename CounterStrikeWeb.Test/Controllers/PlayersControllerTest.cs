namespace CounterStrikeWeb.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FluentAssertions;
    using CounterStrikeWeb.Controllers;
    using CounterStrikeWeb.Services.Players.Models;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Players;
    using System.Linq;

    using static Data.Players;

    public class PlayersControllerTest
    {
        [Fact]
        public void GetAddShouldBeForAuthorizedUsersAndReturnView()
        => MyController<PlayersController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData(
            "nameTest",
            "inGameTest",
            22,
            "https://img-cdn.hltv.org/playerbodyshot/MlU-FvS0jxq7tGkQZmuy9F.png?ixlib=java-2.1.0&w=400&s=5a14b8b67ed91d2c3a553d807e724ba4",
            "Bulgaria")]
        public void PostAddShouldBeForAuthorizedUsersAndReturnRedirect(
            string name,
            string inGameName,
            int age,
            string picture,
            string country
            )
            => MyController<PlayersController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new PlayerFormModel
                {
                    Name = name,
                    InGameName = inGameName,
                    Age = age,
                    Picture = picture,
                    Country = country
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Player>(player => player
                        .Any(p =>
                            p.Name == name &&
                            p.InGameName == inGameName &&
                            p.Age == age &&
                            p.Picture == picture &&
                            p.Country == country)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<PlayersController>(c => c.Details(1, inGameName)));

        [Fact]
        public void DetailsShouldReturnViewWithCorrectModel()
        => MyController<PlayersController>
                .Instance(controller => controller
                    .WithData(TenPublicPlayers))
                .Calling(c => c.Details(1, "test"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<PlayerDetailsServiceModel>());

        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
            => MyController<PlayersController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.All(new AllPlayersQueryModel
                {
                    SearchTerm = null,
                    CurrentPage = 1,
                    TotalPlayers = 10,
                    Players = null
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllPlayersQueryModel>());

        [Fact]
        public void GetEditShouldReturnBadRequstIfUserIsNotAdmin()
            => MyController<PlayersController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(x => x.Edit(1))
            .ShouldReturn()
            .BadRequest();

        [Theory]
        [InlineData(1, "Admin", "Administrator")]
        public void GetEditShouldBeForAdminAndReturnViewWithCorrectModel(int id, string username, string role)
           => MyController<PlayersController>
               .Instance(controller => controller
                   .WithUser(username, new[] { role })
                   .WithData(TenPublicPlayers))
               .Calling(x => x.Edit(id))
           .ShouldReturn()
           .View(view => view
                    .WithModelOfType<PlayerFormModel>());

        [Theory]
        [InlineData(1, "Admin", "Administrator")]
        public void DeleteShouldBeForAdministratorAndReturnRedirect(int testId, string username, string role)
          => MyController<PlayersController>
                .Instance(instance => instance
                    .WithUser(username, new[] { role })
                    .WithData(TenPublicPlayers))
                .Calling(c => c.Delete(testId))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<PlayersController>(c => c.All(null)));
    }
}
