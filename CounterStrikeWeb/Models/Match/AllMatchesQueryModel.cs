namespace CounterStrikeWeb.Models.Match
{
    using CounterStrikeWeb.Services.Matches;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllMatchesQueryModel
    {
        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public IEnumerable<MatchServiceModel> Matches { get; set; }
    }
}
