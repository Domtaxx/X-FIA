using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.Modules.Service;

namespace REST_API_XFIA.Modules
{
    public class DataStrucToSQLStruc
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static SQL_Model.Models.Tournament fillSQLTournament(Data_structures.Tournament tournament)
        {
            List<SQL_Model.Models.Tournament> tournaments = Db.Tournaments.ToList();
            SQL_Model.Models.Tournament toAdd = new SQL_Model.Models.Tournament();
            toAdd.Key = CodeGenerator.generate_key(tournaments);
            toAdd.Name = tournament.nombreCm;
            toAdd.Rules = tournament.descripcionDeReglas;
            toAdd.InitialHour = parseTime(tournament.horaDeInicio);
            toAdd.FinalHour = parseTime(tournament.horaDeFin);
            toAdd.InitialDate = parseDate(tournament.fechaDeInicio);
            toAdd.FinalDate = parseDate(tournament.fechaDeFin);
            toAdd.Budget = tournament.presupuesto;
            return toAdd;
        }
        public static SQL_Model.Models.Race fillSQLRace(Data_structures.Race race)
        {
            SQL_Model.Models.Race toAdd = new SQL_Model.Models.Race();
            toAdd.Name = race.Nombre;
            toAdd.TournamentKey = race.CampeonatoKey;
            toAdd.InitialHour = DateTime.Parse(race.horaDeInicio).TimeOfDay;
            toAdd.FinalHour = DateTime.Parse(race.horaDeFin).TimeOfDay;
            toAdd.InitialDate = DateTime.Parse(DateTime.Parse(race.fechaDeInicio).ToString("yyyy-MM-dd"));
            toAdd.FinalDate = DateTime.Parse(DateTime.Parse(race.fechaDeFin).ToString("yyyy-MM-dd"));
            toAdd.TrackName = race.NombreDePista;
            toAdd.State = 0;
            toAdd.Country = race.Pais;
            return toAdd;
        }

        public static SQL_Model.Models.User fillSQLUser(Data_structures.AllUserInfo user, IStorageService _storageService) {
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

        public static SQL_Model.Models.Tournament GetActiveTournament()
        {
            return Db.Tournaments.FirstOrDefault(T => T.InitialDate >= DateTime.Now);
        }

        public static DateTime parseDate(string toParse)
        {
            return DateTime.Parse(DateTime.Parse(toParse).ToString("yyyy-MM-dd"));
        }

        public static TimeSpan parseTime(string toParse)
        {
            return DateTime.Parse(toParse).TimeOfDay;
        }
    }
}
