﻿namespace CounterStrikeWeb.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class Match
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string FirstTeam { get; set; }

        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string SecondTeam { get; set; }

        public DateTime StartTime { get; set; }

        public int? EventId { get; set; }

        public Event Event { get; init; }
    }
}
