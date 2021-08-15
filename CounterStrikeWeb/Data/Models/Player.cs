namespace CounterStrikeWeb.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class Player
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PlayerNameMaxLength)]
        public string InGameName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        public string Country { get; set; }

        public string InstagramUrl { get; set; }

        public string TwitterUrl { get; set; }

        public bool IsPublic { get; set; }

        public int? TeamId { get; set; }

        public Team Team { get; set; }

    }
}
