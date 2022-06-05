using REST_API_XFIA.SQL_Model.DB_Context;
namespace REST_API_XFIA.Modules.Fetcher
{
    public class TournamentFetcher
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static SQL_Model.Models.Tournament GetActiveTournament()
        {
            SQL_Model.Models.Tournament res = null;

            List<SQL_Model.Models.Tournament> Tour = Db.Tournaments.Where(t =>
                                                        DateTime.Today < t.FinalDate && DateTime.Today > t.InitialDate ||
                                                        DateTime.Today < t.FinalDate && DateTime.Today > t.InitialDate ||
                                                        DateTime.Today == t.FinalDate && t.FinalHour > DateTime.Now.TimeOfDay
                                                        ).ToList();
            if (Tour.Count > 0)
            {
                res = Tour.OrderBy(t=> t.FinalHour).ToList()[0];
                return res;
            }
            Tour = Db.Tournaments.Where(tour => tour.InitialDate == DateTime.Today && tour.InitialHour >= DateTime.Now.TimeOfDay).ToList();
            if (Tour.Count > 0)
            {
                res = Tour.OrderBy(Tour => Tour.InitialHour).ToList()[0];
                return res;
            }
            Tour = Db.Tournaments.Where(tour => tour.InitialDate > DateTime.Today).ToList();
            if(Tour.Count > 0)
            {
                Tour = Tour.OrderBy(Tour => Tour.InitialDate).ToList();
                Tour = TourSameDate(Tour[0].InitialDate, Tour);
                res = Tour.OrderBy(Tour => Tour.InitialHour).ToList()[0];
                return res;
            }
            return res;
        }


        private static List<SQL_Model.Models.Tournament> TourSameDate(DateTime date, List<SQL_Model.Models.Tournament> tournaments)
        {
            var sameDateTournaments = new List<SQL_Model.Models.Tournament>();
            foreach(SQL_Model.Models.Tournament T in tournaments)
            {
                if(T.InitialDate == date)
                {
                    sameDateTournaments.Add(T);
                }
            }
            return sameDateTournaments;
        }
    }
}
