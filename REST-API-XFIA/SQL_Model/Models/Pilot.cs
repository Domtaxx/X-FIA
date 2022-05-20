using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Pilot
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public double Price { get; set; }
        public string Logo { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public int SubTeamsId { get; set; }
        public string RealTeamsName { get; set; } = null!;

        public virtual Country CountryNameNavigation { get; set; } = null!;
        public virtual Realteam RealTeamsNameNavigation { get; set; } = null!;
        public virtual Subteam SubTeams { get; set; } = null!;
    }
}
