using Newtonsoft.Json;
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

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TeamsName { get; set; } = null!;
        public string TeamsLogo { get; set; } = null!;
        public string CountryName { get; set; } = null!;

        public virtual Country CountryNameNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Subteam> Subteams { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tournament> TournamentKeys { get; set; }
    }
}
