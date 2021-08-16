namespace CounterStrikeWeb.Models.Match
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CounterStrikeWeb.Services.Matches;

    public class AllMatchesQueryModel
    {
        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public IEnumerable<MatchServiceModel> Matches { get; set; }
    }
}
