namespace CounterStrikeWeb.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FluentAssertions;
    using CounterStrikeWeb.Controllers;
    using CounterStrikeWeb.Services.Players.Models;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Data.Models;
    using System.Linq;
    using CounterStrikeWeb.Models.Players;

    using static Data.Teams;
    using static Data.Players;

    public class TeamsControllerTest
    {
        [Fact]
        public void GetAddShouldBeForAuthorizedUsersAndReturnView()
        => MyController<TeamsController>
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
            "TestName",
            "https://img-cdn.hltv.org/teamlogo/pNV-lVdpvYZIkDwHdEXXg-.svg?ixlib=java-2.1.0&s=8b557b5b4d283208976340ef1bc44c76",
            "TestCoach", 
            "Bulgaria")]
        public void PostAddShouldBeForAuthorizedUsersAndReturnRedirect(
            string name,
            string logo,
            string coachName,
            string country)
            => MyController<TeamsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new TeamFormModel
                {
                    Name = name,
                    Logo = logo,
                    CoachName= coachName,
                    Country = country
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Team>(team => team
                        .Any(t =>
                            t.Name == name &&
                            t.Logo == logo &&
                            t.CoachName == coachName &&
                            t.Country == country)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<TeamsController>(c => c.Details(1, name)));


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
        [InlineData(null, 1, 10, 1)]
        public void FindPlayerToAddShouldBeForAuthorizedUsersAndReturnViewWithCorrectModel(
            string searchTerm,
            int currentPage,
            int TotalPlayers,
            int teamId)
        => MyController<TeamsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.FindPlayerToAdd(teamId, new AddPlayerToTeamViewModel
                {
                    SearchTerm = searchTerm,
                    CurrentPage = currentPage,
                    TotalPlayers = TotalPlayers,
                    TeamId = teamId,
                    Players = TenPlayers
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddPlayerToTeamViewModel>());

        [Fact]
        public void GetEditShouldReturnBadRequstIfUserIsNotAdmin()
            => MyController<TeamsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(x => x.Edit(1))
            .ShouldReturn()
            .BadRequest();

        [Theory]
        [InlineData(1, "Admin", "Administrator")]
        public void GetEditShouldBeForAdminAndReturnViewWithCorrectModel(int id, string username, string role)
           => MyController<TeamsController>
               .Instance(controller => controller
                   .WithUser(username, new[] { role })
                   .WithData(TenTeams))
               .Calling(x => x.Edit(id))
           .ShouldReturn()
           .View(view => view
                    .WithModelOfType<TeamFormModel>());


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
