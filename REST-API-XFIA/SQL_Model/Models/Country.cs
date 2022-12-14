using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Country
    {
        public Country()
        {
            Pilots = new HashSet<Pilot>();
            Races = new HashSet<Race>();
            Users = new HashSet<User>();
        }

        public string Name { get; set; } = null!;
        public string Photo { get; set; } = null!;

        public virtual ICollection<Pilot> Pilots { get; set; }
        public virtual ICollection<Race> Races { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
