namespace CounterStrikeWeb.Models.Events
{
    public class EventListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string StartOn { get; init; }

        public string Price { get; init; }

        public int ParticipantsCount { get; init; }
    }
}
