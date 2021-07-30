namespace CounterStrikeWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Events;
    using CounterStrikeWeb.Models.Teams;
    using Microsoft.AspNetCore.Mvc;

    public class EventsController : Controller
    {
        private readonly CounterStrikeDbContext data;

        public EventsController(CounterStrikeDbContext data)
            => this.data = data;

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
            var teamsQuery = this.data.Teams.AsQueryable();
            var totalTeams = teamsQuery.Count();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                teamsQuery = teamsQuery.Where(t =>
                t.Name.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            var teams = teamsQuery
                .Where(t => t.Event == null)
                .Skip((query.CurrentPage - 1) * AddTeamToEventViewModel.TeamsPerPage)
                .Take(AddTeamToEventViewModel.TeamsPerPage)
                .OrderByDescending(x => x.Id)
                .Select(t => new TeamListingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Logo = t.Logo,
                })
                .ToList();

            query.Teams = teams;
            query.TotalTeams = totalTeams;
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