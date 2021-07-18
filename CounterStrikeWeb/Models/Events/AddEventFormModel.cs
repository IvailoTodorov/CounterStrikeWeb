namespace CounterStrikeWeb.Models.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddEventFormModel
    {
        [Required]
        [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength)]
        public string Name { get; init; }

        [Required]
        public string StartOn { get; init; }

        [Required]
        [StringLength(EventPriceMaxLength, MinimumLength = EventPriceMinLength)]
        public string Price { get; init; }
    }
}
