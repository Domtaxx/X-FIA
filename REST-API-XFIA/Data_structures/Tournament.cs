namespace REST_API_XFIA.Data_structures
{
    public class Tournament
    {

        public String? nombreCm { get; set; } = null!;
        public String? fechaDeInicio { get; set; } = null!;
        public String? horaDeInicio { get; set; } = null!;
        public String? fechaDeFin { get; set; } = null!;
        public String? horaDeFin { get; set; } = null!;
        public String? descripcionDeReglas { get; set; } = null!;
        public double presupuesto { get; set; } 
    }
}
