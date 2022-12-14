using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.Data_structures;
using REST_API_XFIA.SQL_Model.DB_Context;

namespace REST_API_XFIA.Modules.Fetcher
{
    public class PrivateLeagueFetcher
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        public static List<PublicLeagueResponse> getEveryoneInList(string privateLeagueName, SQL_Model.Models.Tournament tournament)
        {
            var DBO = new RESTAPIXFIA_dbContext();
            List<PublicLeagueResponse> AllPrivateLeagueRes = new List<PublicLeagueResponse>();
            List<SQL_Model.Models.User> users;
            users = DBO.Users.Where(U => U.PrivateLeagueName == privateLeagueName).ToList();
            foreach (SQL_Model.Models.User user in users)
            {
                PublicLeagueResponse data;
                List<SQL_Model.Models.Subteam> subTeams = SubTeamFetcher.getLatestSubTeam(user.Email, tournament);
                foreach (SQL_Model.Models.Subteam subTeam in subTeams)
                {
                    data = new PublicLeagueResponse();
                    var pilotsInSub = DBO.HasPilots.Where(HP => HP.SubTeamsId == subTeam.Id).ToList();
                    var SubTeamPoints = DBO.SubteamPoints.Where(STP => STP.SubTeamId == subTeam.Id && STP.TournamentKey == tournament.Key).ToList();
                    data.Points = 0;
                    if (SubTeamPoints.Count > 0)
                    {
                        data.Points = (int)SubTeamPoints[0].Points;
                    }
                    data.SubteamName = subTeam.Name;
                    data.TeamName = user.TeamsName;
                    data.UserName = user.Username;
                    data.UserEmail = user.Email;
                    AllPrivateLeagueRes.Add(data);
                }
            }
            AllPrivateLeagueRes = AllPrivateLeagueRes.OrderByDescending(PLR => PLR.Points).ToList();
            for (int i = 0; i < AllPrivateLeagueRes.Count(); i++)
            {
                AllPrivateLeagueRes[i].Position = (uint)i + 1;
            }
            return AllPrivateLeagueRes;
        }
        public static Data_structures.PrivateLeague GetPrivateleagueData(string userEmail)
        {
            var DBO = new RESTAPIXFIA_dbContext();
            SQL_Model.Models.Privateleague privateLeague = DBO.Users.Include(U => U.PrivateLeagueNameNavigation).ThenInclude(PRL => PRL.Users).Where(U => U.Email.Equals(userEmail)).Single().PrivateLeagueNameNavigation;
            var data = new Data_structures.PrivateLeague();

            data.name = privateLeague.Name;
            data.key = "" + privateLeague.TournamentKey + privateLeague.PrivateLeagueKey;
            data.ownerEmail = privateLeague.OwnerEmail;
            data.maxUser = privateLeague.MaxUser;
            data.state = false;
            if (privateLeague.Users.Count() > 5)
            {
                data.state = true;
            }
            return data;
        }
    }
}

