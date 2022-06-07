using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Realteam
    {
        public Realteam()
        {
            Pilots = new HashSet<Pilot>();
            RealTeamRaces = new HashSet<RealTeamRace>();
            Subteams = new HashSet<Subteam>();
        }

        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public string Photo { get; set; } = null!;
        public string Logo { get; set; } = null!;

        public virtual ICollection<Pilot> Pilots { get; set; }
        public virtual ICollection<RealTeamRace> RealTeamRaces { get; set; }
        public virtual ICollection<Subteam> Subteams { get; set; }
    }
}
