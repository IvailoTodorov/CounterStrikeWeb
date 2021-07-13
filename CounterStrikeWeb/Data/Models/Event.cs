namespace CounterStrikeWeb.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Event
    {
        public Event()
        {
            this.Teams = new HashSet<Team>();
            this.Matches = new HashSet<Match>();
        }

        public int Id { get; init; }

        [Required]
        [MaxLength(EventNameMaxLength)]
        public string Name { get; set; }

        public DateTime StartOn { get; set; }

        [Required]
        [MaxLength(EventPriceMaxLength)]
        public string Price { get; set; }

        public int ParticipantsCount { get; set; }

        public IEnumerable<Team> Teams { get; init; }

        public IEnumerable<Match> Matches { get; init; }
    }
}
