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
            List<PublicLeagueResponse> AllPrivateLeagueRes = new List<PublicLeagueResponse>();
            List<SQL_Model.Models.User> users;
            users = Db.Users.Where(U => U.PrivateLeagueName == privateLeagueName).ToList();
            foreach (SQL_Model.Models.User user in users)
            {
                PublicLeagueResponse data;
                List<SQL_Model.Models.Subteam> subTeams = Db.Subteams.Where(st => st.UserEmail == user.Email).ToList();
                foreach (SQL_Model.Models.Subteam subTeam in subTeams)
                {
                    data = new PublicLeagueResponse();
                    var pilotsInSub = Db.HasPilots.Where(HP => HP.SubTeamsId == subTeam.Id).ToList();
                    data.Points = sumPoints(pilotsInSub, subTeam.RealTeamsNameNavigation, tournament.Races.ToList());
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
        private static uint sumPoints(List<SQL_Model.Models.HasPilot> pilots, SQL_Model.Models.Realteam car, List<SQL_Model.Models.Race> races)
        {
            uint points = 0;
            foreach (SQL_Model.Models.Race race in races)
            {
                foreach (SQL_Model.Models.HasPilot pilot in pilots)
                {
                    points += (uint)Db.PilotRaces.Where(PR => PR.PilotId == pilot.PilotId && PR.TournamentKey == race.TournamentKey && PR.Name == race.Name).Single().Points;
                }
                points += (uint)Db.RealTeamRaces.Where(RTR => RTR.RealTeamName == car.Name && RTR.TournamentKey == race.TournamentKey && RTR.Name == race.Name).Single().Points;
            }
            return points;
        }
    }
}

