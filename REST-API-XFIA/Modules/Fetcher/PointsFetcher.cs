using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.Data_structures;
using REST_API_XFIA.SQL_Model.DB_Context;


namespace REST_API_XFIA.Modules.Fetcher
{
    public class PointsFetcher
    {

        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        public static void addPointsForTeam(string userEmail, SQL_Model.Models.Tournament tournament)
        {
            //SQL_Model.Models.User user = Db.Users.Where(U => U.Email.Equals(userEmail)).Single();
            List<SQL_Model.Models.Subteam> subTeams = SubTeamFetcher.getLatestSubTeam(userEmail,tournament);

            foreach (SQL_Model.Models.Subteam subTeam in subTeams)
            {
                SQL_Model.Models.SubteamPoint subteamPoint = new();
                subteamPoint.TournamentKey = tournament.Key;
                subteamPoint.SubTeamId = subTeam.Id;

                int totalSubTeamPoints = 0;

                foreach (SQL_Model.Models.HasPilot hasPilot in subTeam.HasPilots)
                {
                    List<SQL_Model.Models.PilotRace> pilotRaces = Db.PilotRaces.
                                                     Where(P => P.PilotId.Equals(hasPilot.PilotId) 
                                                     && P.TournamentKey.Equals(tournament.Key)).ToList();
                    foreach(SQL_Model.Models.PilotRace pilotRace in pilotRaces)
                    {
                        totalSubTeamPoints += (int) pilotRace.Points;
                    }
                }

                List<SQL_Model.Models.RealTeamRace> realTeamRaces = Db.RealTeamRaces.
                                                    Where(RTR => RTR.RealTeamName.Equals(subTeam.RealTeamsName) 
                                                    && RTR.TournamentKey.Equals(tournament.Key)).ToList();

                foreach (SQL_Model.Models.RealTeamRace realTeamRace in realTeamRaces)
                {
                    totalSubTeamPoints += (int) realTeamRace.Points;
                }

                subteamPoint.Points = totalSubTeamPoints;
                Db.SubteamPoints.Add(subteamPoint);
                Db.SaveChanges();
            }


        }
    }
}
