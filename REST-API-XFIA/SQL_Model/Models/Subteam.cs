using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Subteam
    {
        public Subteam()
        {
            Pilots = new HashSet<Pilot>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string RealTeamsName { get; set; } = null!;

        public virtual Realteam RealTeamsNameNavigation { get; set; } = null!;
        public virtual User UserEmailNavigation { get; set; } = null!;

        public virtual ICollection<Pilot> Pilots { get; set; }
    }
}
