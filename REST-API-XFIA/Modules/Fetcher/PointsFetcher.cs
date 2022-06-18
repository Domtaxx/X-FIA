using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.Data_structures;
using REST_API_XFIA.SQL_Model.DB_Context;


namespace REST_API_XFIA.Modules.Fetcher
{
    public class PointsFetcher
    {

        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        public static void addPointsForTeam(SQL_Model.Models.Tournament tournament)
        {
            List<SQL_Model.Models.User> users = tournament.UserEmails.ToList();
            foreach (SQL_Model.Models.User user in users)
            {
                List<SQL_Model.Models.Subteam> subTeams = SubTeamFetcher.getLatestSubTeam(user.Email, tournament);

                foreach (SQL_Model.Models.Subteam subTeam in subTeams)
                {
                    SQL_Model.Models.SubteamPoint subteamPoint = new();
                    subteamPoint.TournamentKey = tournament.Key;
                    subteamPoint.SubTeamId = subTeam.Id;

                    int totalSubTeamPoints = 0;

                    foreach (SQL_Model.Models.HasPilot hasPilot in subTeam.HasPilots)
                    {
                        List<SQL_Model.Models.PilotRace> pilotRaces = Db.PilotRaces.
                                                         Include(PR => PR.Race).
                                                         Where(
                                                                PR => (PR.Race.InitialDate > subTeam.CreationDate || (PR.Race.InitialDate == subTeam.CreationDate && PR.Race.InitialHour > subTeam.CreationHour))
                                                                && PR.PilotId.Equals(hasPilot.PilotId)
                                                                && PR.TournamentKey.Equals(tournament.Key)
                                                              ).ToList();
                        foreach (SQL_Model.Models.PilotRace pilotRace in pilotRaces)
                        {
                            totalSubTeamPoints += (int)pilotRace.Points;
                        }
                    }

                    List<SQL_Model.Models.RealTeamRace> realTeamRaces = Db.RealTeamRaces.
                                                        Include(RTR => RTR.Race).
                                                        Where(
                                                            RTR => (RTR.Race.InitialDate > subTeam.CreationDate || (RTR.Race.InitialDate == subTeam.CreationDate && RTR.Race.InitialHour > subTeam.CreationHour))
                                                            && RTR.RealTeamName.Equals(subTeam.RealTeamsName)
                                                            && RTR.TournamentKey.Equals(tournament.Key)
                                                            ).ToList();

                    foreach (SQL_Model.Models.RealTeamRace realTeamRace in realTeamRaces)
                    {
                        totalSubTeamPoints += (int)realTeamRace.Points;
                    }

                    subteamPoint.Points = totalSubTeamPoints;
                    Db.SubteamPoints.Add(subteamPoint);
                    Db.SaveChanges();
                }
            }
        }
        public static List<SQL_Model.Models.PilotRace> getPilotRaces(List<PilotDocument> pilotInfo, string raceName, string tourKey)
        {
            var res = new List<SQL_Model.Models.PilotRace>();
            foreach (PilotDocument PD in pilotInfo)
            {
                var temp = new SQL_Model.Models.PilotRace();
                temp.Name = raceName;
                temp.TournamentKey = tourKey;
                temp.PilotId = PD.id;
            }

            return res;
        }
        public static int getPoints(PilotDocument pilot)
        {
            int totalPoints = 0;
            if (pilot.Q1)
            {
                totalPoints += 1;
            }
            if (pilot.Q2)
            {
                totalPoints += 2;
            }
            if (pilot.Q3)
            {
                totalPoints += 3;
            }
            if (pilot.didNotDoClassification)
            {
                totalPoints -= 5;
            }
            if (pilot.classificationPos <= 10)
            {
                totalPoints += 11-pilot.classificationPos;
            }
            if (pilot.wonParter)
            {
                totalPoints += 7;
            }
            if (pilot.disqClassification)
            {
                totalPoints -= 10;
            }
            if(pilot.racePos == 1)
            {
                totalPoints += 50;
            }
            else if (pilot.racePos == 2)
            {
                totalPoints += 36;
            }
            else if (pilot.racePos == 3)
            {
                totalPoints += 30;
            }
            else if (pilot.racePos == 4)
            {
                totalPoints += 24;
            }
            else if (pilot.racePos == 5)
            {
                totalPoints += 20;
            }
            else if (pilot.racePos == 6)
            {
                totalPoints += 16;
            }
            else if (pilot.racePos == 7)
            {
                totalPoints += 12;
            }
            else if (pilot.racePos == 8)
            {
                totalPoints += 8;
            }
            else if (pilot.racePos == 9)
            {
                totalPoints += 3;
            }
            else if (pilot.racePos == 10)
            {
                totalPoints += 1;
            }
            if (pilot.disqRace)
            {
                totalPoints -= 40;
            }
            if (pilot.didNotDoRace)
            {
                totalPoints -= 5;
            }
            if (pilot.didFastestLap)
            {
                totalPoints += 10;
            }
            totalPoints += 2 * (pilot.racePos - pilot.classificationPos);
            return totalPoints;
        }
    }
}
    }
