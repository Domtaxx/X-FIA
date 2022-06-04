using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Pilot
    {
        public Pilot()
        {
            SubTeams = new HashSet<Subteam>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public double Price { get; set; }
        public string Photo { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string? RealTeamsName { get; set; }
        public int? Points { get; set; }

        public virtual Country CountryNameNavigation { get; set; } = null!;
        public virtual Realteam? RealTeamsNameNavigation { get; set; }

        [JsonIgnore]
        public virtual ICollection<Subteam> SubTeams { get; set; }
    }
}
