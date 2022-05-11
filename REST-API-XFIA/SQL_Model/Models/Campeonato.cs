using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Campeonato
    {
        public Campeonato()
        {
            Carreras = new HashSet<Carrera>();
        }

        public string Llave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public DateTime FechaDeInicio { get; set; }
        public TimeSpan HoraDeInicio { get; set; }
        public DateTime FechaDeFin { get; set; }
        public TimeSpan HoraDeFin { get; set; }
        public double Presupuesto { get; set; }
        public string? DescripcionDeReglas { get; set; }

        public virtual ICollection<Carrera> Carreras { get; set; }
    }
}
