namespace CounterStrikeWeb.Models.Teams
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllTeamsQueryModel
    {
        public const int TeamsPerPage = 2;

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalCars { get; set; }

        public IEnumerable<TeamListingViewModel> Teams { get; set; }
    }
}
