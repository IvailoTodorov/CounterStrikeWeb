namespace CounterStrikeWeb.Models.Teams
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class TeamFormModel
    {
        [Required]
        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string Name { get; init; }

        [Required]
        [Url]
        public string Logo { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        [Display(Name = "Coach Name")]
        public string CoachName { get; init; }

        [Required]
        public string Country { get; init; }

    }
}
