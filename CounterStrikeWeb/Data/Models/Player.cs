namespace CounterStrikeWeb.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Player
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PlayerNameMaxLength)]
        public string InGameName { get; set; }

        public int Age { get; set; }

        public string Picture { get; set; }

        public string Country { get; set; }

        public string InstagramUrl { get; set; }

        public string TwitterUrl { get; set; }

        //public string Crosshair { get; set; }

        //public string ViewModel { get; set; }

        //public string CL_BOB { get; set; }

        //public string LaunchOptions { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; init; }

    }
}
