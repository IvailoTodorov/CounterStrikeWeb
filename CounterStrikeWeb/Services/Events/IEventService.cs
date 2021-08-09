namespace CounterStrikeWeb.Services.Events
{
    using System.Collections.Generic;
    using CounterStrikeWeb.Data.Models;
    using CounterStrikeWeb.Models.Events;

    public interface IEventService
    {
        EventQueryServiceModel FindTeamToAdd(
             string searchTerm,
             int currentPage,
             int teamsPerPage);

        IEnumerable<EventListingViewModel> All();

        void Add(AddEventFormModel @event);

        void AddTeamToEvent(int teamId, int eventId);

        Event Find(int id);
    }
}
