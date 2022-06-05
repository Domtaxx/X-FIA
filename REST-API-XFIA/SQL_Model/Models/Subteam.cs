using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Subteam
    {
        public Subteam()
        {
            HasPilots = new HashSet<HasPilot>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string RealTeamsName { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public TimeSpan? CreationHour { get; set; }

        public virtual Realteam RealTeamsNameNavigation { get; set; } = null!;
        public virtual User UserEmailNavigation { get; set; } = null!;
        public virtual ICollection<HasPilot> HasPilots { get; set; }
    }
}
