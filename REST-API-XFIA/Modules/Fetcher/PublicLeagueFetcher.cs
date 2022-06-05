using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.Data_structures;
using REST_API_XFIA.SQL_Model.DB_Context;

namespace REST_API_XFIA.Modules.Fetcher
{
    public class PublicLeagueFetcher
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
       /*
        public static List<SQL_Model.Models.Pilot> getPublicLeagueSubList(SQL_Model.Models.Tournament tournament, int page, int amountByPage)
        {
            List<PublicLeagueResponse> DataInPage = new List<PublicLeagueResponse>();
            List<SQL_Model.Models.User> users = Db.Users.Include(u => u.Subteams).ThenInclude(St => St.Pilots).ThenInclude(St => St.RealTeamsNameNavigation).ToList();
            foreach (SQL_Model.Models.User user in users)
            {
                PublicLeagueResponse data = new PublicLeagueResponse();
                sumPoints(user);
            }
            int actualPage = 0;
            for (int i = 0; i < pilots.Count - 1; i++)
            {
                if (i % amountByPage == 0)
                {
                    actualPage++;
                }

                if (actualPage == page)
                {
                    pilotsInPage.Add(pilots[i]);
                }
                else if (actualPage > page)
                {
                    break;
                }
            }
            return pilotsInPage;
        }
       */
        private static int sumPoints(object a)
        {
            throw new NotImplementedException();
        }
    }
}
