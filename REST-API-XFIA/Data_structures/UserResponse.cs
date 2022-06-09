namespace REST_API_XFIA.Data_structures
{
    public class UserResponse
    {
        public string Username { get; set; } = null!;
        public string TeamsName { get; set; } = null!;
        public string TeamsLogo { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string? PrivateLeagueName { get; set; }
        public virtual SQL_Model.Models.Country CountryNameNavigation { get; set; } = null!;
        public virtual List<Data_structures.SubTeam> Subteams { get; set; }
    }
}