using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class HasPilot
    {
        public HasPilot(int IdSubTeam, int IdPilot) {
            this.PilotId = IdPilot;
            this.SubTeamsId = IdSubTeam;
        }
        public int SubTeamsId { get; set; }
        public int PilotId { get; set; }
    }
}
