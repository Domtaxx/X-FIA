using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Carrera
    {
        public string NombreCr { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public string NombreDePista { get; set; } = null!;
        public DateTime FechaDeInicio { get; set; }
        public TimeSpan HoraDeInicio { get; set; }
        public DateTime FechaDeFin { get; set; }
        public TimeSpan HoraDeFin { get; set; }
        public string Campeonato { get; set; } = null!;

        public virtual Campeonato CampeonatoNavigation { get; set; } = null!;
    }
}
