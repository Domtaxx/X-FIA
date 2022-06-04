using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Privateleagues = new HashSet<Privateleague>();
            Races = new HashSet<Race>();
            UserEmails = new HashSet<User>();
        }

        public string Key { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime InitialDate { get; set; }
        public TimeSpan InitialHour { get; set; }
        public DateTime FinalDate { get; set; }
        public TimeSpan FinalHour { get; set; }
        public double Budget { get; set; }
        public string? Rules { get; set; }

        public virtual ICollection<Privateleague> Privateleagues { get; set; }
        public virtual ICollection<Race> Races { get; set; }

        public virtual ICollection<User> UserEmails { get; set; }
    }
}
