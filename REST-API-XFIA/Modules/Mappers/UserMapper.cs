using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Service;

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
            toAdd.TeamsLogo = _storageService.Upload(user.TeamsLogo);
            return toAdd;
        }
        public static List<SQL_Model.Models.Subteam> fillSubteams(Data_structures.AllUserInfo userInfo)
        {
            List<SQL_Model.Models.Subteam> subteams = new List<SQL_Model.Models.Subteam>();
            SQL_Model.Models.Subteam team1 = new SQL_Model.Models.Subteam();
            SQL_Model.Models.Subteam team2 = new SQL_Model.Models.Subteam();
            List<SQL_Model.Models.Subteam> lastId = Db.Subteams.OrderByDescending(ST => ST.Id).ToList();

            team1.Id = lastId[0].Id + 1;
            team1.RealTeamsName = userInfo.Car1;
            team1.Name = userInfo.NameSubteam1;
            team1.UserEmail = userInfo.Email;



            team2.Id = lastId[0].Id + 2;
            team2.RealTeamsName = userInfo.Car2;
            team2.Name = userInfo.NameSubteam2;
            team2.UserEmail = userInfo.Email;


            subteams.Add(team1);
            subteams.Add(team2);

            return subteams;
        }

        public static void fillHasPilots(Data_structures.AllUserInfo userInfo, SQL_Model.Models.Subteam subTeam1, SQL_Model.Models.Subteam subTeam2)
        {
            subTeam1.Pilots = new List<SQL_Model.Models.Pilot>();
            subTeam2.Pilots = new List<SQL_Model.Models.Pilot>();
            IQueryable<SQL_Model.Models.Subteam> subteams1 = Db.Subteams.FromSqlInterpolated($"exec dbo.uspInsertIntoHasPilot @subTeamId = {subTeam1.Id}, @pilotId = {userInfo.pilot1Subteam1}").AsNoTracking();
            IQueryable<SQL_Model.Models.Subteam> subteams2 = Db.Subteams.FromSqlInterpolated($"exec dbo.uspInsertIntoHasPilot @subTeamId = {subTeam1.Id}, @pilotId = {userInfo.pilot2Subteam1}").AsNoTracking();
            IQueryable<SQL_Model.Models.Subteam> subteams3 = Db.Subteams.FromSqlInterpolated($"exec dbo.uspInsertIntoHasPilot @subTeamId = {subTeam1.Id}, @pilotId = {userInfo.pilot3Subteam1}").AsNoTracking();
            IQueryable<SQL_Model.Models.Subteam> subteams4 = Db.Subteams.FromSqlInterpolated($"exec dbo.uspInsertIntoHasPilot @subTeamId = {subTeam1.Id}, @pilotId = {userInfo.pilot4Subteam1}").AsNoTracking();
            IQueryable<SQL_Model.Models.Subteam> subteams5 = Db.Subteams.FromSqlInterpolated($"exec dbo.uspInsertIntoHasPilot @subTeamId = {subTeam1.Id}, @pilotId = {userInfo.pilot5Subteam1}").AsNoTracking();

            IQueryable<SQL_Model.Models.Subteam> subteams6 = Db.Subteams.FromSqlInterpolated($"exec dbo.uspInsertIntoHasPilot @subTeamId = {subTeam2.Id}, @pilotId = {userInfo.pilot1Subteam2}").AsNoTracking();
            IQueryable<SQL_Model.Models.Subteam> subteams7 = Db.Subteams.FromSqlInterpolated($"exec dbo.uspInsertIntoHasPilot @subTeamId = {subTeam2.Id}, @pilotId = {userInfo.pilot2Subteam2}").AsNoTracking();
            IQueryable<SQL_Model.Models.Subteam> subteams8 = Db.Subteams.FromSqlInterpolated($"exec dbo.uspInsertIntoHasPilot @subTeamId = {subTeam2.Id}, @pilotId = {userInfo.pilot3Subteam2}").AsNoTracking();
            IQueryable<SQL_Model.Models.Subteam> subteams9 = Db.Subteams.FromSqlInterpolated($"exec dbo.uspInsertIntoHasPilot @subTeamId = {subTeam2.Id}, @pilotId = {userInfo.pilot4Subteam2}").AsNoTracking();
            IQueryable<SQL_Model.Models.Subteam> subteams10 = Db.Subteams.FromSqlInterpolated($"exec dbo.uspInsertIntoHasPilot @subTeamId = {subTeam2.Id}, @pilotId = {userInfo.pilot5Subteam2}").AsNoTracking();

            Db.SaveChanges();
            return;
        }
    }
}
