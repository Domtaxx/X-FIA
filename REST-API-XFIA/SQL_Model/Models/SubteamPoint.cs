using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class SubteamPoint
    {
        public string TournamentKey { get; set; } = null!;
        public int SubTeamId { get; set; }
        public int? Points { get; set; }

        public virtual Subteam SubTeam { get; set; } = null!;
        public virtual Tournament TournamentKeyNavigation { get; set; } = null!;
    }
}
