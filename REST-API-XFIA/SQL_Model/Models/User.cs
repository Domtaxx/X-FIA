using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class User
    {
        public User()
        {
            TournamentKeys = new HashSet<Tournament>();
        }

        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Country { get; set; } = null!;

        public virtual Country CountryNavigation { get; set; } = null!;

        public virtual ICollection<Tournament> TournamentKeys { get; set; }
    }
}
