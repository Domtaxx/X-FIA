namespace REST_API_XFIA.Data_structures
{
    public class Race
    {
        public string Nombre { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public int Estado { get; set; }
        public string NombreDePista { get; set; } = null!;
        public string fechaDeInicio { get; set; }
        public string horaDeInicio { get; set; }
        public string fechaDeFin { get; set; }
        public string horaDeFin { get; set; }
        public string CampeonatoKey { get; set; } = null!;
    }
}
