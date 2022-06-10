using REST_API_XFIA.Data_structures;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.SQL_Model.Models;
using Microsoft.EntityFrameworkCore;

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

        public static bool privateLeagueIsInDatabase(string key)
        {
            String privatekey = key.Substring(6, 6);
            SQL_Model.Models.Privateleague privateLeague = Db.Privateleagues.Where(PL=>PL.PrivateLeagueKey.Equals(privatekey)).Single();
            if (privateLeague == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool privateLeagueIsInDatabaseByName(string Name, string tourKey)
        {
            var privateLeague = Db.Privateleagues.Where(PL => PL.Name.Equals(Name) && PL.TournamentKey.Equals(tourKey)).ToList();
            if (privateLeague.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public static bool privateLeagueIsActive(string privateLeagueName)
        {
            List<SQL_Model.Models.User> users;
            users = Db.Users.Where(U => U.PrivateLeagueName == privateLeagueName).ToList();
            if (users.Count < 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool userAlreadyHasPrivateLeague(SQL_Model.Models.User user)
        {
  
            if (user.PrivateLeagueName == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool userExistsInDB(string userEmail)
        {
            SQL_Model.Models.User user = Db.Users.Find(userEmail);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static bool isUserOwner(SQL_Model.Models.User user)
        {
            SQL_Model.Models.Privateleague privateLeague = Db.Privateleagues.Find(user.PrivateLeagueName);
            if (privateLeague.OwnerEmail == user.Email)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int isValidDelete(SQL_Model.Models.User user) //for deleting user from private league
        {
            if (!PrivateLeagueVerification.userExistsInDB(user.Email))
            {
                return 5; //User is not logged in
            }
            if (!userAlreadyHasPrivateLeague(user))
            {
                return 12; //User doesn't belong to any private league
            }
            if (isUserOwner(user))
            {
                return 10; //User is the owner
            }
            return 0;
        }

        public static int isValid(SQL_Model.Models.Privateleague privateLeague)//for creating privateLeague
        {
            if (!PrivateLeagueVerification.userExistsInDB(privateLeague.OwnerEmail))
            {
                return 5; //User is not logged in
            }
            if (PrivateLeagueVerification.userAlreadyHasPrivateLeague(Db.Users.Find(privateLeague.OwnerEmail)))
            {
                return 3; //User has already a private league
            }
            if (PrivateLeagueVerification.privateLeagueIsInDatabaseByName(privateLeague.Name, privateLeague.TournamentKey))
            {
                return 1; //Private league name is already in use
            }

            return 0;
        }


        public static int isValid(Data_structures.UserToPrivateLeague userToPrivateLeague)//for adding new member
        {
            SQL_Model.Models.Privateleague privateLeague = Db.Privateleagues.Include(PRL => PRL.Users).Where(PRL => PRL.PrivateLeagueKey == userToPrivateLeague.privateLeagueKey.Substring(6,6)).Single();
            if (!PrivateLeagueVerification.userExistsInDB(userToPrivateLeague.userEmail))
            {
                return 5; //User is not logged in
            }
            if (PrivateLeagueVerification.userAlreadyHasPrivateLeague(Db.Users.Find(userToPrivateLeague.userEmail)))
            {
                return 3; //User has already a private league
            }
            if (!privateLeagueIsInDatabase(userToPrivateLeague.privateLeagueKey))
            {
                return 7; //PrivateLeague doesn't exist
            }

            return 0;
        }

        public static int isValid(SQL_Model.Models.User user) //for getting private league and its members
        {
            if (!PrivateLeagueVerification.userExistsInDB(user.Email))
            {
                return 5; //User is not logged in
            }
            if (!userAlreadyHasPrivateLeague(user))
            {
                return 12; //User doesn't belong to any private league
            }
            return 0;
            
        }
    }
}



