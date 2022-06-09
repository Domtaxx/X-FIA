using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.SQL_Model.DB_Context;
namespace REST_API_XFIA.Modules.Fetcher
{
    public class SubTeamFetcher
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static List<SQL_Model.Models.Subteam> getSubTeamsLatest(String userEmail)
        {
            List<SQL_Model.Models.Subteam> Tempsubteams = Db.Subteams.Where(St => St.UserEmail == userEmail)
                                                                     .OrderByDescending(St => St.CreationDate)
                                                                     .ToList();
            Tempsubteams = Db.Subteams.Include(St => St.RealTeamsNameNavigation)
                                      .Include(St => St.HasPilots)
                                      .ThenInclude(HP => HP.Pilot)
                                      .ThenInclude(P => P.CountryNameNavigation)
                                      .Include(St => St.HasPilots)
                                      .ThenInclude(HP => HP.Pilot)
                                      .ThenInclude(P => P.RealTeamsNameNavigation)
                                      .Where(St =>
                                                St.UserEmail == userEmail &&
                                                Tempsubteams[0].CreationDate == St.CreationDate
                                            )
                                      .OrderByDescending(St => St.CreationHour)
                                      .ToList();
            List<SQL_Model.Models.Subteam> subTeams = new List<SQL_Model.Models.Subteam>();
            subTeams.Add(Tempsubteams[0]);
            subTeams.Add(Tempsubteams[1]);
            return subTeams;
        }
    }
}
