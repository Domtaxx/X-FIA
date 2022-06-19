using REST_API_XFIA.Data_structures;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.Modules.BuisnessRules
{
    public class UserVerifications
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static bool VerifyIfUserHasAccount(AllUserInfo userInfo)
        {
            var usersWithSameEmail = Db.Users.Where(U => U.Email == userInfo.Email).ToList();
            if (usersWithSameEmail.Count() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool VerifyIfTeamNameIsRepeated(AllUserInfo userInfo)
        {
            var invalidTeams = Db.Users.Where(U => U.TeamsName == userInfo.TeamsName).ToList();
            if (invalidTeams.Count > 0)
            {
                return true;
            }
            return false;
        }
        public static bool VerifyIfSubTeamsNamesAreRepeated(AllUserInfo userInfo)
        {
            if (userInfo.NameSubteam1.CompareTo(userInfo.NameSubteam2) == 0)
            {
                return true;
            }
            return false;
        }

        public static int IsValid(AllUserInfo user)
        {
            if (VerifyIfUserHasAccount(user))
            {
                return 1;
            }
            if (VerifyIfTeamNameIsRepeated(user))
            {
                return 2;
            }
            if (VerifyIfSubTeamsNamesAreRepeated(user))
            {
                return 3;
            }return 0;
        }
        public static int IsValidForModification(AllUserInfo user)
        {
            if (!VerifyIfUserHasAccount(user))
            {
                return 1;
            }
            if (VerifyIfTeamNameIsRepeated(user))
            {
                return 2;
            }
            if (VerifyIfSubTeamsNamesAreRepeated(user))
            {
                return 3;
            }
            if (RacesActive())
            {
                return 4;
            }
            return 0;
        }

        private static bool RacesActive()
        {
            var races = Db.Races.Where(R => R.State == 1).ToList();
            if (races.Count > 0)
            {
                return true;
            }return false;
        }
    }
}
