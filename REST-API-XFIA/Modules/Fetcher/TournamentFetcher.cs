using REST_API_XFIA.SQL_Model.DB_Context;
namespace REST_API_XFIA.Modules.Fetcher
{
    public class TournamentFetcher
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static SQL_Model.Models.Tournament GetActiveTournament()
        {
            return Db.Tournaments.FirstOrDefault(tour => tour.InitialDate > DateTime.Today || tour.InitialDate == DateTime.Today && tour.InitialHour >= DateTime.Now.TimeOfDay);
        }
    }
}
