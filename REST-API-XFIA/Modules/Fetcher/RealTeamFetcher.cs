using REST_API_XFIA.SQL_Model.DB_Context;
namespace REST_API_XFIA.Modules.Fetcher
{
    public class RealTeamFetcher
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static List<SQL_Model.Models.Realteam> getRealTeamsSubList(List<SQL_Model.Models.Realteam> RealTeams, int page, int amountByPage)
        {
            List<SQL_Model.Models.Realteam> RealTeamsInPage = new List<SQL_Model.Models.Realteam>();
            int actualPage = 0;
            for (int i = 0; i < RealTeams.Count - 1; i++)
            {
                if (i % amountByPage == 0)
                {
                    actualPage++;
                }

                if (actualPage == page)
                {
                    RealTeamsInPage.Add(RealTeams[i]);
                }
                else if (actualPage > page)
                {
                    break;
                }
            }
            return RealTeamsInPage;
        }
    }
}
