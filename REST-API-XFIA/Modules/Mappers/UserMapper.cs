using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Service;
using REST_API_XFIA.Modules.Fetcher;

namespace REST_API_XFIA.Modules.Mappers
{
    public class UserMapper
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        public static SQL_Model.Models.User fillSQLUser(Data_structures.AllUserInfo user, IStorageService _storageService)
        {
            SQL_Model.Models.User toAdd = new SQL_Model.Models.User();
            toAdd.CountryName = user.CountryName;
            toAdd.TeamsName = user.TeamsName;
            toAdd.Password = user.Password;
            toAdd.Username = user.Username;
            toAdd.Email = user.Email;
            toAdd.TeamsLogo = _storageService.Upload(user.TeamsLogo, user.Email);
            return toAdd;
        }
        public static List<SQL_Model.Models.Subteam> fillSubteams(Data_structures.AllUserInfo userInfo)
        {
            var ActiveTournament = TournamentFetcher.GetActiveTournament();
            List<SQL_Model.Models.Subteam> subteams = new List<SQL_Model.Models.Subteam>();
            SQL_Model.Models.Subteam team1 = new SQL_Model.Models.Subteam();
            SQL_Model.Models.Subteam team2 = new SQL_Model.Models.Subteam();
            List<SQL_Model.Models.Subteam> lastId = Db.Subteams.OrderByDescending(ST => ST.Id).ToList();

            team1.Id = lastId[0].Id + 1;
            team1.RealTeamsName = userInfo.Car1;
            team1.Name = userInfo.NameSubteam1;
            team1.UserEmail = userInfo.Email;
            team1.CreationHour = DateTime.Now.TimeOfDay;
            team1.CreationDate = DateTime.Now.Date;
            



            team2.Id = lastId[0].Id + 2;
            team2.RealTeamsName = userInfo.Car2;
            team2.Name = userInfo.NameSubteam2;
            team2.UserEmail = userInfo.Email;
            team2.CreationHour = DateTime.Now.TimeOfDay;
            team2.CreationDate = DateTime.Now.Date;

            subteams.Add(team1);
            subteams.Add(team2);

            return subteams;
        }

        public static List<SQL_Model.Models.HasPilot> fillHasPilots(Data_structures.AllUserInfo userInfo, int subTeam1, int subTeam2)
        {
            List<SQL_Model.Models.HasPilot> PilotConex = new List<SQL_Model.Models.HasPilot>();
            var pilot1 = new SQL_Model.Models.HasPilot();
            var pilot2 = new SQL_Model.Models.HasPilot();
            var pilot3 = new SQL_Model.Models.HasPilot();
            var pilot4 = new SQL_Model.Models.HasPilot();
            var pilot5 = new SQL_Model.Models.HasPilot();
            
            var pilot6 = new SQL_Model.Models.HasPilot();
            var pilot7 = new SQL_Model.Models.HasPilot();
            var pilot8 = new SQL_Model.Models.HasPilot();
            var pilot9 = new SQL_Model.Models.HasPilot();
            var pilot10 = new SQL_Model.Models.HasPilot();

            pilot1.PilotId = userInfo.pilot1Subteam1;
            pilot2.PilotId = userInfo.pilot2Subteam1;
            pilot3.PilotId = userInfo.pilot3Subteam1;
            pilot4.PilotId = userInfo.pilot4Subteam1;
            pilot5.PilotId = userInfo.pilot5Subteam1;

            pilot6.PilotId = userInfo.pilot1Subteam2;
            pilot7.PilotId = userInfo.pilot2Subteam2;
            pilot8.PilotId = userInfo.pilot3Subteam2;
            pilot9.PilotId = userInfo.pilot4Subteam2;
            pilot10.PilotId = userInfo.pilot5Subteam2;

            pilot1.SubTeamsId = subTeam1;
            pilot2.SubTeamsId = subTeam1;
            pilot3.SubTeamsId = subTeam1;
            pilot4.SubTeamsId = subTeam1;
            pilot5.SubTeamsId = subTeam1;

            pilot6.SubTeamsId = subTeam2;
            pilot7.SubTeamsId = subTeam2;
            pilot8.SubTeamsId = subTeam2;
            pilot9.SubTeamsId = subTeam2;
            pilot10.SubTeamsId = subTeam2;


            PilotConex.Add(pilot1);
            PilotConex.Add(pilot2);
            PilotConex.Add(pilot3);
            PilotConex.Add(pilot4);
            PilotConex.Add(pilot5);
            PilotConex.Add(pilot6);
            PilotConex.Add(pilot7);
            PilotConex.Add(pilot8);
            PilotConex.Add(pilot9);
            PilotConex.Add(pilot10);

            return PilotConex;
        }
        public static Data_structures.UserResponse fillUserResponse(SQL_Model.Models.User user, List<SQL_Model.Models.Subteam> subteams)
        {
            Data_structures.UserResponse response = new Data_structures.UserResponse();
            response.Username = user.Username;
            response.TeamsName = user.TeamsName;
            response.CountryName = user.CountryName;
            response.CountryNameNavigation = user.CountryNameNavigation;
            response.CountryNameNavigation.Users = null;
            response.CountryNameNavigation.Pilots = null;
            response.PrivateLeagueName = user.PrivateLeagueName;
            response.TeamsLogo = user.TeamsLogo;
            response.Subteams = new List<Data_structures.SubTeam>();

            foreach (SQL_Model.Models.Subteam subteam in subteams)
            {
                Data_structures.SubTeam temp = new Data_structures.SubTeam();
                temp.Pilots = new List<SQL_Model.Models.Pilot>();
                temp.Name = subteam.Name;
                temp.RealTeamsNameNavigation = subteam.RealTeamsNameNavigation;
                temp.RealTeamsNameNavigation.Pilots = null;
                temp.RealTeamsNameNavigation.Subteams = null;
                temp.CreationHour = subteam.CreationHour;
                temp.CreationDate = subteam.CreationDate;

                var pilot1 = subteam.HasPilots.ToList()[0].Pilot;
                pilot1.HasPilots = null;
                pilot1.RealTeamsNameNavigation.Pilots = null;
                pilot1.RealTeamsNameNavigation.Subteams = null;
                pilot1.CountryNameNavigation.Pilots = null;

                var pilot2 = subteam.HasPilots.ToList()[1].Pilot;
                pilot2.HasPilots = null;
                pilot2.RealTeamsNameNavigation.Pilots = null;
                pilot2.RealTeamsNameNavigation.Subteams = null;
                pilot2.CountryNameNavigation.Pilots = null;

                var pilot3 = subteam.HasPilots.ToList()[2].Pilot;
                pilot3.HasPilots = null;
                pilot3.RealTeamsNameNavigation.Pilots = null;
                pilot3.RealTeamsNameNavigation.Subteams = null;
                pilot3.CountryNameNavigation.Pilots = null;

                var pilot4 = subteam.HasPilots.ToList()[3].Pilot;
                pilot4.HasPilots = null;
                pilot4.RealTeamsNameNavigation.Pilots = null;
                pilot4.RealTeamsNameNavigation.Subteams = null;
                pilot4.CountryNameNavigation.Pilots = null;

                var pilot5 = subteam.HasPilots.ToList()[4].Pilot;
                pilot5.HasPilots = null;
                pilot5.RealTeamsNameNavigation.Pilots = null;
                pilot5.RealTeamsNameNavigation.Subteams = null;
                pilot5.CountryNameNavigation.Pilots = null;

                temp.Pilots.Add(pilot1);
                temp.Pilots.Add(pilot2);
                temp.Pilots.Add(pilot3);
                temp.Pilots.Add(pilot4);
                temp.Pilots.Add(pilot5);
                response.Subteams.Add(temp);
            }
            return response;
        }
        
        public static void modSubTeams(SQL_Model.Models.Subteam subTeam1, SQL_Model.Models.Subteam subTeam2, Data_structures.AllUserInfo userInfo)
        {
            subTeam1.CreationDate = DateTime.Now.Date;
            subTeam1.CreationHour = DateTime.Now.TimeOfDay;

            subTeam2.CreationDate = DateTime.Now.Date;
            subTeam2.CreationHour = DateTime.Now.TimeOfDay;

            return;
        }
    }
}
