using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Paise
    {
        public Paise()
        {
            Carreras = new HashSet<Carrera>();
        }

        public string Nombre { get; set; } = null!;

        public virtual ICollection<Carrera> Carreras { get; set; }
    }
}
