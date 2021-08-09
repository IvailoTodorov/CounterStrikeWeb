namespace CounterStrikeWeb.Services.Events
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CounterStrikeWeb.Data;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Events;
    using CounterStrikeWeb.Services.Teams;

    public class EventService : IEventService
    {
        private readonly CounterStrikeDbContext data;
        private readonly IMapper mapper;

        public EventService(CounterStrikeDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(AddEventFormModel @event)
        {
            var startOn = DateTime.ParseExact(@event.StartOn, "MMMM dd yyyy", CultureInfo.InvariantCulture);

            if (startOn < DateTime.UtcNow)
            {
                throw new ArgumentException();
            }

            var eventData = new Event
            {
                Name = @event.Name,
                StartOn = startOn,
                Price = @event.Price,
            };

            this.data.Events.Add(eventData);

            this.data.SaveChanges();
        }

        public void AddTeamToEvent(int teamId, int eventId)
        {
            var team = this.data.Teams.Find(teamId);
            var @event = this.data.Events.Find(eventId);

            team.Event = @event;

            this.data.SaveChanges();
        }

        public IEnumerable<EventListingViewModel> All()
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

            return events;
        }

        public Event Find(int id) 
            => this.data
            .Events
            .Find(id);

        public EventQueryServiceModel FindTeamToAdd(
            string searchTerm,
            int currentPage,
            int teamsPerPage)
        {
            var teamsQuery = this.data.Teams.AsQueryable();
            var totalTeams = teamsQuery.Count();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                teamsQuery = teamsQuery.Where(t =>
                t.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var teams = teamsQuery
                .Where(t => t.Event == null)
                .Skip((currentPage - 1) * teamsPerPage)
                .Take(teamsPerPage)
                .OrderByDescending(x => x.Id)
                .ProjectTo<TeamServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return new EventQueryServiceModel
            {
                CurrentPage = currentPage,
                TeamsPerPage = teamsPerPage,
                TotalTeams = totalTeams,
                Teams = teams,
            };
        }
    }
}
