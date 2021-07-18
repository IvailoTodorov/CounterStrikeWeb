namespace CounterStrikeWeb.Models.Events
{
    public class EventListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public string StartOn { get; set; }

        public string Price { get; set; }

        public int ParticipantsCount { get; set; }
    }
}
