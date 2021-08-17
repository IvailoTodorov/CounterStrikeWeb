namespace CounterStrikeWeb.Models.Players
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class PlayerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        [Required]
        [StringLength(PlayerNameMaxLength, MinimumLength = PlayerNameMinLength)]
        public string InGameName { get; init; }

        [Range(PlayerMinAge,PlayerMaxAge)]
        public int Age { get; init; }

        [Url]
        public string Picture { get; init; }

        [Required]
        public string Country { get; init; }

        [Display(Name = "Instagram URL")]
        [Url]
        public string InstagramUrl { get; init; }

        [Display(Name = "Twitter URL")]
        [Url]
        public string TwitterUrl { get; init; }
    }
}
