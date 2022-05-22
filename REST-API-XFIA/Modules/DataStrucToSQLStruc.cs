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
            toAdd.Firstname = user.Firstname;
            toAdd.Lastname = user.Lastname;
            toAdd.Email = user.Email;
            toAdd.TeamsLogo = _storageService.Upload(user.TeamsLogo);
            return toAdd;
        }
        public static List<SQL_Model.Models.Subteam> fillSubteams(Data_structures.AllUserInfo userInfo)
        {
            List<SQL_Model.Models.Subteam> subteams = new List<SQL_Model.Models.Subteam>();
            SQL_Model.Models.Subteam team1 = new SQL_Model.Models.Subteam();
            SQL_Model.Models.Subteam team2 = new SQL_Model.Models.Subteam();

            team1.RealTeamsName = userInfo.Car1;
            team1.Name = userInfo.NameSubteam1;
            team1.UserEmail = userInfo.Email;
            team1.Pilots.Add(Db.Pilots.Where(p => p.Id == userInfo.pilot1Subteam1).Single());
            team1.Pilots.Add(Db.Pilots.Where(p => p.Id == userInfo.pilot2Subteam1).Single());
            team1.Pilots.Add(Db.Pilots.Where(p => p.Id == userInfo.pilot3Subteam1).Single());
            team1.Pilots.Add(Db.Pilots.Where(p => p.Id == userInfo.pilot4Subteam1).Single());
            team1.Pilots.Add(Db.Pilots.Where(p => p.Id == userInfo.pilot5Subteam1).Single());

            team2.RealTeamsName = userInfo.Car2;
            team2.Name = userInfo.NameSubteam2;
            team2.UserEmail = userInfo.Email;
            team2.Pilots.Add(Db.Pilots.Where(p => p.Id == userInfo.pilot1Subteam2).Single());
            team2.Pilots.Add(Db.Pilots.Where(p => p.Id == userInfo.pilot2Subteam2).Single());
            team2.Pilots.Add(Db.Pilots.Where(p => p.Id == userInfo.pilot3Subteam2).Single());
            team2.Pilots.Add(Db.Pilots.Where(p => p.Id == userInfo.pilot4Subteam2).Single());
            team2.Pilots.Add(Db.Pilots.Where(p => p.Id == userInfo.pilot5Subteam2).Single());

            subteams.Add(team1);
            subteams.Add(team2);

            return subteams;
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
