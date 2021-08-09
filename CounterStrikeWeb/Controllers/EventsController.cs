namespace CounterStrikeWeb.Controllers
{
    using AutoMapper;
    using CounterStrikeWeb.Models.Events;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Events;
    using Microsoft.AspNetCore.Mvc;

    public class EventsController : Controller
    {
        private readonly IEventService events;
        private readonly IMapper mapper;

        public EventsController(
            IEventService events,
            IMapper mapper)
        {
            this.events = events;
            this.mapper = mapper;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddEventFormModel @event)
        {
            if (!ModelState.IsValid)
            {
                return View(@event);
            }

            this.events.Add(@event);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            var events = this.events.All();

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
            this.events.AddTeamToEvent(teamId, eventId);

            return RedirectToAction(nameof(All));
        }
    }
}