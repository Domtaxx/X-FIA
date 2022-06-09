using REST_API_XFIA.Data_structures;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.Modules.BuisnessRules
{
    public class PublicLeagueVerification
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static bool VerifyIfTournamentNotValid(SQL_Model.Models.Tournament tour)
        {
            if(tour == null)
            {
                return true;
            }
            return false;
        }

        public static bool VerifyIfTournamentNotValidAndNumValid(SQL_Model.Models.Tournament tour, int num)
        {
            if (num < 1 || VerifyIfTournamentNotValid(tour))
            {
                return true;
            }
            return false;
        }

        internal static int verifyPublicLeagueValid(SQL_Model.Models.Tournament tour, int amountByPage, int page)
        {
            if (amountByPage < 1 || VerifyIfTournamentNotValid(tour) || page < 1)
            {
                return 1;
            }
            return 0;
        }

        public static int verifyUserPublicLeagueValid(SQL_Model.Models.Tournament tour, string UserEmail, List<SQL_Model.Models.User> users)
        {
            if (VerifyIfTournamentNotValid(tour))
            {
                return 1;
            }
            var user = users.Find(U => U.Email == UserEmail);
            if (user == null)
            {
                return 2;
            }

            return 0;
        }
    }
}
