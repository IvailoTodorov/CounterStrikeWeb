namespace CounterStrikeWeb.Models.Players
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class AddPlayerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PlayerNameMaxLength, MinimumLength = PlayerNameMinLength)]
        public string InGameName { get; set; }

        [Range(PlayerMinAge,PlayerMaxAge)]
        public int Age { get; set; }

        [Url]
        public string Picture { get; set; }

        [Required]
        public string Country { get; set; }

        [Display(Name = "Instagram URL")]
        [Url]
        public string InstagramUrl { get; set; }

        [Display(Name = "Twitter URL")]
        [Url]
        public string TwitterUrl { get; set; }

        //public string Crosshair { get; set; }

        //public string ViewModel { get; set; }

        //public string CL_BOB { get; set; }

        //public string LaunchOptions { get; set; }

        //public int TeamId { get; set; }
    }
}
