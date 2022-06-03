using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Service;

namespace REST_API_XFIA.Modules.Mappers
{
    public class TournamentMapper
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static SQL_Model.Models.Tournament fillSQLTournament(Data_structures.Tournament tournament)
        {
            List<SQL_Model.Models.Tournament> tournaments = Db.Tournaments.ToList();
            SQL_Model.Models.Tournament toAdd = new SQL_Model.Models.Tournament();
            toAdd.Key = CodeGenerator.generate_key(tournaments);
            toAdd.Name = tournament.nombreCm;
            toAdd.Rules = tournament.descripcionDeReglas;
            toAdd.InitialHour = DateAndTimeParser.parseTime(tournament.horaDeInicio);
            toAdd.FinalHour = DateAndTimeParser.parseTime(tournament.horaDeFin);
            toAdd.InitialDate = DateAndTimeParser.parseDate(tournament.fechaDeInicio);
            toAdd.FinalDate = DateAndTimeParser.parseDate(tournament.fechaDeFin);
            toAdd.Budget = tournament.presupuesto;
            return toAdd;
        }
    }
}
