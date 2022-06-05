using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class HasPilot
    {
        public int SubTeamsId { get; set; }
        public int PilotId { get; set; }
        public int? DummyData { get; set; }

        public virtual Pilot Pilot { get; set; } = null!;
        public virtual Subteam SubTeams { get; set; } = null!;
    }
}
