using REST_API_XFIA.SQL_Model.DB_Context;

namespace REST_API_XFIA.Modules.Fetcher
{
    public class PilotFetcher
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static List<SQL_Model.Models.Pilot> getPilotSubList(List<SQL_Model.Models.Pilot> pilots, int page, int amountByPage)
        {
            List<SQL_Model.Models.Pilot> pilotsInPage = new List<SQL_Model.Models.Pilot>();
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
    }
}
