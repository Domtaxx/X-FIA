using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class PilotRace
    {
        public int PilotId { get; set; }
        public string Name { get; set; } = null!;
        public string TournamentKey { get; set; } = null!;
        public int? Points { get; set; }

        public virtual Pilot Pilot { get; set; } = null!;
        public virtual Race Race { get; set; } = null!;
    }
}
