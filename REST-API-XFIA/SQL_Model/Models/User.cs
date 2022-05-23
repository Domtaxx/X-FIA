using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class User
    {
        public User()
        {
            Subteams = new HashSet<Subteam>();
            TournamentKeys = new HashSet<Tournament>();
        }

        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TeamsName { get; set; } = null!;
        public string TeamsLogo { get; set; } = null!;
        public string CountryName { get; set; } = null!;

        public virtual Country CountryNameNavigation { get; set; } = null!;
        public virtual ICollection<Subteam> Subteams { get; set; }

        public virtual ICollection<Tournament> TournamentKeys { get; set; }
    }
}
