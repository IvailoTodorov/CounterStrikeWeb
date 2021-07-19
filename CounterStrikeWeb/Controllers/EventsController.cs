namespace CounterStrikeWeb.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Events;
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

            var eventData = new Event
            {
                Name = @event.Name,
                StartOn = DateTime.ParseExact(@event.StartOn, "MMMM dd yyyy", CultureInfo.InvariantCulture),
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
    }
}
