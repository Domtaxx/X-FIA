namespace REST_API_XFIA.Data_structures
{
    public class Race
    {
        public string Nombre { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public int Estado { get; set; } 
        public string NombreDePista { get; set; } = null!;
        public string fechaDeInicio { get; set; } = null!;
        public string horaDeInicio { get; set; } = null!;
        public string fechaDeFin { get; set; } = null!;
        public string horaDeFin { get; set; } = null!;
        public string CampeonatoKey { get; set; } = null!;
    }
}
