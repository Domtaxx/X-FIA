using REST_API_XFIA.Data_structures;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.Modules.BuisnessRules
{
    public class UserVerifications: IAddingRules
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

        int IAddingRules.IsValid(AllUserInfo user)
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

        int IAddingRules.IsValid(SQL_Model.Models.Tournament tour)
        {
            throw new NotImplementedException();
        }

        int IAddingRules.IsValid(SQL_Model.Models.Race race)
        {
            throw new NotImplementedException();
        }
    }
}
