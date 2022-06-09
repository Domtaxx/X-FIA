using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.Data_structures
{
    public class SubTeam
    {
        public string Name { get; set; } = null!;
        public virtual Realteam RealTeamsNameNavigation { get; set; }
        public DateTime? CreationDate { get; set; }
        public TimeSpan? CreationHour { get; set; }
        public virtual List<SQL_Model.Models.Pilot> Pilots { get; set; }
    }
}
