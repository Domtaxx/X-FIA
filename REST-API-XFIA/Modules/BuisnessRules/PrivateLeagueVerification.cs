using REST_API_XFIA.Data_structures;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.Modules.BuisnessRules
{
    public class PrivateLeagueVerification
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        public static bool IfKeyIsRepeatedInDB(string key, List<SQL_Model.Models.Privateleague> privateLeagues)
        {
            foreach (SQL_Model.Models.Privateleague privateLeague in privateLeagues)
            {
                if (privateLeague.PrivateLeagueKey == key)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool privateLeagueIsInDatabase(string name)
        {
            SQL_Model.Models.Privateleague privateLeague = Db.Privateleagues.Find(name);
            if (privateLeague == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool privateLeagueIsActive(List<SQL_Model.Models.User> usersInPrivateLeague)
        {
            if (usersInPrivateLeague.Count < 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}
