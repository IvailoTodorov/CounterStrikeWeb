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

        void Add(EventFormModel @event);

        void AddTeamToEvent(int teamId, int eventId);

        Event Find(int id);

        bool Edit(
            int id,
            string name,
            string startOn,
            string price);

        void Delete(int id);
    }
}
