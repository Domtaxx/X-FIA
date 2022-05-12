using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Race
    {
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public int State { get; set; }
        public string TrackName { get; set; } = null!;
        public DateTime InitialDate { get; set; }
        public TimeSpan InitialHour { get; set; }
        public DateTime FinalDate { get; set; }
        public TimeSpan FinalHour { get; set; }
        public string TournamentKey { get; set; } = null!;

        public virtual Country CountryNavigation { get; set; } = null!;
        public virtual Tournament TournamentKeyNavigation { get; set; } = null!;
    }
}
