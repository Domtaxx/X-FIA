using System;
using System.Collections.Generic;

namespace REST_API_XFIA.SQL_Model.Models
{
    public partial class Carrera
    {
        public string Nombre { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public int Estado { get; set; }
        public string NombreDePista { get; set; } = null!;
        public DateTime FechaDeInicio { get; set; }
        public TimeSpan HoraDeInicio { get; set; }
        public DateTime FechaDeFin { get; set; }
        public TimeSpan HoraDeFin { get; set; }
        public string CampeonatoKey { get; set; } = null!;

        public virtual Campeonato CampeonatoKeyNavigation { get; set; } = null!;
        public virtual Paise PaisNavigation { get; set; } = null!;
    }
}
