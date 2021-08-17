namespace CounterStrikeWeb.Controllers
{
    using AutoMapper;
    using CounterStrikeWeb.Infrastrucure;
    using CounterStrikeWeb.Models.Events;
    using CounterStrikeWeb.Models.Teams;
    using CounterStrikeWeb.Services.Events;
    using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Add() => View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(EventFormModel @event)
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

        [Authorize]
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

        [Authorize]
        public IActionResult AddTeamToEvent(int teamId, int eventId)
        {
            this.events.AddTeamToEvent(teamId, eventId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            var @event = this.events.Find(id);

            var eventForm = this.mapper.Map<EventFormModel>(@event);

            return View(eventForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, EventFormModel eventData)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            var eventIsEdited = this.events.Edit(
            id,
            eventData.Name,
            eventData.StartOn,
            eventData.Price);

            if (!eventIsEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.events.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}