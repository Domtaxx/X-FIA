using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class RealTeamRace
    {
        public string RealTeamName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string TournamentKey { get; set; } = null!;
        public int? Points { get; set; }

        public virtual Race Race { get; set; } = null!;
        public virtual Realteam RealTeamNameNavigation { get; set; } = null!;
    }
}
