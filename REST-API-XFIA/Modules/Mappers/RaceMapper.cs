using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.Modules.Service;

namespace REST_API_XFIA.Modules.Mappers
{
    public class RaceMapper
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
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
    }
}
