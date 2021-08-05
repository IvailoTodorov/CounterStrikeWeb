namespace CounterStrikeWeb.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static Data.DataConstants;

    public class User : IdentityUser
    {
        [MaxLength(UserFullNameMaxLength)]
        public string FullName { get; set; }
    }
}
