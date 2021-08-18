namespace CounterStrikeWeb.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using CounterStrikeWeb.Controllers;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Events;
    using CounterStrikeWeb.Models.Match;
    using CounterStrikeWeb.Models.Matches;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Events;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static Data.Matches;
    using static Data.Teams;

    public class MatchControllerTest
    {
        [Fact]
        public void GetAddShouldBeForAuthorizedUsersAndReturnView()
        => MyController<MatchController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("TestFirst", "TestSecond", "July 12 2024")]
        public void PostAddShouldBeForAuthorizedUsersAndReturnRedirect(
            string firstTeam,
            string secondTeam,
            string startTime)
            => MyController<MatchController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(TwoTeams()))
                .Calling(c => c.Add(new MatchFormModel
                {
                    FirstTeam = firstTeam,
                    SecondTeam = secondTeam,
                    StartTime = startTime
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Match>(match => match
                        .Any(m =>
                            m.FirstTeam == firstTeam &&
                            m.SecondTeam == secondTeam &&
                            m.StartTime == DateTime.ParseExact(startTime, "MMMM dd yyyy", CultureInfo.InvariantCulture))))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MatchController>(c => c.All(null)));

        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
            => MyController<MatchController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.All(new AllMatchesQueryModel
                {
                    SearchTerm = null,
                    Matches = null
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllMatchesQueryModel>());

        [Fact]
        public void DetailsShouldReturnViewWithCorrectModel()
        => MyController<MatchController>
                .Instance(controller => controller
                    .WithData(TenMatches))
                .Calling(c => c.Details(1, "firstTeam", "secondTeam", "July 12 2024"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<MatchDetailsViewModel>());

        [Fact]
        public void GetEditShouldReturnBadRequstIfUserIsNotAdmin()
            => MyController<MatchController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(x => x.Edit(1, "test", "test", "july 12 2024"))
            .ShouldReturn()
            .BadRequest();

        [Theory]
        [InlineData(1, "Admin", "Administrator", "test", "test", "july 12 2024")]
        public void GetEditShouldBeForAdminAndReturnViewWithCorrectModel(
            int id,
            string username,
            string role,
            string firstTeam,
            string secondTeam,
            string startTime)
           => MyController<MatchController>
               .Instance(controller => controller
                   .WithUser(username, new[] { role })
                   .WithData(TenMatches))
               .Calling(x => x.Edit(id, firstTeam, secondTeam, startTime))
           .ShouldReturn()
           .View(view => view
                    .WithModelOfType<MatchFormModel>());

        [Theory]
        [InlineData(1, "Admin", "Administrator")]
        public void DeleteShouldBeForAdministratorAndReturnRedirect(int testId, string username, string role)
          => MyController<MatchController>
                .Instance(instance => instance
                    .WithUser(username, new[] { role })
                    .WithData(TenMatches))
                .Calling(c => c.Delete(testId))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MatchController>(c => c.All(null)));
    }
}
