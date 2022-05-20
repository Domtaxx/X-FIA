using static System.Net.Mime.MediaTypeNames;

namespace REST_API_XFIA.Data_structures
{
    public class User
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TeamsName { get; set; } = null!;
        public string TeamsLogo { get; set; } = null!;
        public string CountryName { get; set; } = null!;
    }
}
