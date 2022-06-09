using REST_API_XFIA.Data_structures;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.Modules.BuisnessRules
{
    public class TournamentVerifications 
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static bool IfKeyIsRepeatedInDB(string key, List<SQL_Model.Models.Tournament> tournaments)
        {
            foreach (SQL_Model.Models.Tournament tournament in tournaments)
            {
                if (tournament.Key == key)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool tournamentIsInDatabase(string key)
        {
            SQL_Model.Models.Tournament tournamentTested = Db.Tournaments.Find(key);
            if (tournamentTested == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IfTournamentAtSameTime(DateTime iniDay, TimeSpan iniHour, DateTime finDay, TimeSpan finHour)
        {
            List<SQL_Model.Models.Tournament> conflictingTournaments = Db.Tournaments.Where(t =>
                                                        finDay < t.FinalDate && finDay > t.InitialDate ||
                                                        iniDay < t.FinalDate && iniDay > t.InitialDate ||
                                                        t.InitialDate == finDay && finHour >= t.InitialHour ||
                                                        t.FinalDate == iniDay && iniHour <= t.FinalHour ||
                                                        t.FinalDate == finDay && finHour == t.FinalHour ||
                                                        t.InitialDate == iniDay && iniHour == t.InitialHour).ToList();
            if (conflictingTournaments.Count() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool VerifyIfTournamentsActiveOrFuture()
        {
            List<SQL_Model.Models.Tournament> tournaments = Db.Tournaments.Where(T => T.InitialDate > DateTime.Now || T.InitialDate == DateTime.Now && T.InitialHour > DateTime.Now.TimeOfDay).ToList();
            if (tournaments.Count() > 0)
            {
                return false;
            }
            return true;
        }

        int IsValid(SQL_Model.Models.Tournament tour)
        {
            if (IfTournamentAtSameTime(tour.InitialDate, tour.InitialHour, tour.FinalDate, tour.FinalHour))
            {
                return 1;
            }
            return 0;
        }
    }
}
