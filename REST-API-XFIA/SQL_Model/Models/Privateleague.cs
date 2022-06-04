using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Privateleague
    {
        public Privateleague()
        {
            Users = new HashSet<User>();
        }

        public string? OwnerEmail { get; set; }
        public string? TournamentKey { get; set; }
        public string Name { get; set; } = null!;
        public int? MaxUser { get; set; }

        public virtual User? OwnerEmailNavigation { get; set; }
        public virtual Tournament? TournamentKeyNavigation { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
