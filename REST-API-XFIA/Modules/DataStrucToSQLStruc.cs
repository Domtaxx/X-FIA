using REST_API_XFIA.DB_Context;

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

        public static SQL_Model.Models.User fillSQLUser(Data_structures.User user) {
            SQL_Model.Models.User toAdd = new SQL_Model.Models.User();
            toAdd.CountryName = user.CountryName;
            toAdd.TeamsName = user.TeamsName;
            toAdd.Password = user.Password;
            toAdd.Firstname = user.Firstname;
            toAdd.Lastname = user.Lastname;
            toAdd.Email = user.Email;
            toAdd.TeamsLogo = user.TeamsLogo;
            return toAdd;
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
