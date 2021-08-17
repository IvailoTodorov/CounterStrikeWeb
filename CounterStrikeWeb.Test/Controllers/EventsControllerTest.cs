namespace CounterStrikeWeb.Test.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using CounterStrikeWeb.Controllers;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Events;
    using CounterStrikeWeb.Models.Teams;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static Data.Teams;
    using static Data.Events;

    public class EventsControllerTest
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

        [Theory]
        [InlineData("TestEvent", "July 12 2024", "10,000 cash")]
        public void PostAddShouldBeForAuthorizedUsersAndReturnRedirect(
            string name,
            string startOn,
            string price)
            => MyController<EventsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new EventFormModel
                {
                    Name = name,
                    StartOn = startOn,
                    Price = price
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Event>(ev => ev
                        .Any(e =>
                            e.Name == name &&
                            e.StartOn == DateTime.ParseExact(startOn, "MMMM dd yyyy", CultureInfo.InvariantCulture) &&
                            e.Price == price)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<EventsController>(c => c.All()));

        [Fact]
        public void AllShouldReturnViewWithCorrectModel()
         => MyController<EventsController>
                .Instance()
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<EventListingViewModel>>());

        [Theory]
        [InlineData(null, 1, 10, 1)]
        public void FindTeamToAddShouldBeForAuthorizedUsersAndReturnViewWithCorrectModel(
            string searchTerm,
            int currentPage,
            int TotalTeams,
            int eventId)
        => MyController<EventsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.FindTeamToAdd(eventId, new AddTeamToEventViewModel
                {
                    SearchTerm = searchTerm,
                    CurrentPage = currentPage,
                    TotalTeams = TotalTeams,
                    EventId = eventId,
                    Teams = TenPublicTeams
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddTeamToEventViewModel>());

        [Fact]
        public void GetEditShouldReturnBadRequstIfUserIsNotAdmin()
            => MyController<EventsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(x => x.Edit(1))
            .ShouldReturn()
            .BadRequest();

        [Theory]
        [InlineData(1, "Admin", "Administrator")]
        public void DeleteShouldBeForAdministratorAndReturnRedirect(int testId, string username, string role)
          => MyController<EventsController>
                .Instance(instance => instance
                    .WithUser(username, new[] { role})
                    .WithData(TenEvents))
                .Calling(c => c.Delete(testId))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<EventsController>(c => c.All()));

    }
}
