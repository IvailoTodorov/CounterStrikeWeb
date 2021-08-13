namespace CounterStrikeWeb.Models.Matches
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class MatchFormModel
    {
        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string FirstTeam { get; init; }

        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string SecondTeam { get; init; }

        [Required]
        public string StartTime { get; init; }
    }
}
