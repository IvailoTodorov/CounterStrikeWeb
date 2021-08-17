namespace CounterStrikeWeb.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FluentAssertions;
    using CounterStrikeWeb.Controllers;
    using CounterStrikeWeb.Services.Players.Models;

    using static Data.Teams;
    using CounterStrikeWeb.Models.Teams;

    public class TeamsControllerTest
    {
        [Fact]
        public void GetAddShouldBeForAuthorizedUsersAndReturnView()
        => MyController<EventsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
            => MyController<TeamsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.All(new AllTeamsQueryModel
                {
                    SearchTerm = null,
                    CurrentPage = 1,
                    TotalTeams = 10,
                    Teams = null
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllTeamsQueryModel>());


        [Theory]
        [InlineData(1, "Admin", "Administrator")]
        public void DeleteShouldBeForAdministratorAndReturnRedirect(int testId, string username, string role)
          => MyController<TeamsController>
                .Instance(instance => instance
                    .WithUser(username, new[] { role })
                    .WithData(TenTeams))
                .Calling(c => c.Delete(testId))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<TeamsController>(c => c.All(null)));
    }
}
