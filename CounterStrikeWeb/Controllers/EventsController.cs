namespace CounterStrikeWeb.Controllers
{
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Events;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Events;
    using CounterStrikeWeb.Services.Teams;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Globalization;
    using System.Linq;

    public class EventsController : Controller
    {
        private readonly IEventService events;
        private readonly CounterStrikeDbContext data;

        public EventsController(IEventService events, CounterStrikeDbContext data)
        {
            this.events = events;
            this.data = data;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddEventFormModel @event)
        {
            if (!ModelState.IsValid)
            {
                return View(@event);
            }

            var startOn = DateTime.ParseExact(@event.StartOn, "MMMM dd yyyy", CultureInfo.InvariantCulture);

            if (startOn < DateTime.UtcNow)
            {
                return BadRequest();
            }

            var eventData = new Event
            {
                Name = @event.Name,
                StartOn = startOn,
                Price = @event.Price,
            };

            this.data.Events.Add(eventData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            var events = this.data
               .Events
               .OrderByDescending(x => x.Id)
               .Select(e => new EventListingViewModel
               {
                   Id = e.Id,
                   Name = e.Name,
                   StartOn = e.StartOn.ToString("MMMM dd yyyy"),
                   Price = e.Price,
                   ParticipantsCount = e.Teams.Count()
               })
               .ToList();

            return View(events);
        }

        public IActionResult FindTeamToAdd(int id, [FromQuery] AddTeamToEventViewModel query)
        {
            var queryResult = this.events.FindTeamToAdd(
                query.SearchTerm,
                query.CurrentPage,
                AddTeamToEventViewModel.TeamsPerPage);

            query.Teams = queryResult.Teams;
            query.TotalTeams = queryResult.TotalTeams;
            query.EventId = id;

            return View(query);
        }

        public IActionResult AddTeamToEvent(int teamId, int eventId)
        {
            var team = this.data.Teams.Find(teamId);
            var @event = this.data.Events.Find(eventId);

            team.Event = @event;

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}