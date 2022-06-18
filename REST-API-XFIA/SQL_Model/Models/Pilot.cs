using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Pilot
    {
        public Pilot()
        {
            HasPilots = new HashSet<HasPilot>();
            PilotRaces = new HashSet<PilotRace>();
        }

        public string Id { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public double Price { get; set; }
        public int? HotstreakClassification { get; set; }
        public int? HotstreakRace { get; set; }
        public string Photo { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string? RealTeamsName { get; set; }

        public virtual Country CountryNameNavigation { get; set; } = null!;
        public virtual Realteam? RealTeamsNameNavigation { get; set; }
        public virtual ICollection<HasPilot> HasPilots { get; set; }
        public virtual ICollection<PilotRace> PilotRaces { get; set; }
    }
}
