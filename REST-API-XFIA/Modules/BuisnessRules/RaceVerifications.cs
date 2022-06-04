using REST_API_XFIA.Data_structures;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.Modules.BuisnessRules
{
    public class RaceVerifications : IAddingRules
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static bool IfRacesAtSameTime(DateTime iniDay, TimeSpan iniHour, DateTime finDay, TimeSpan finHour)
        {
            List<SQL_Model.Models.Race> conflictingRaces = Db.Races.Where(c =>
                                                    finDay < c.FinalDate && finDay > c.InitialDate ||
                                                    iniDay < c.FinalDate && iniDay > c.InitialDate ||
                                                    c.InitialDate == finDay && finHour >= c.InitialHour ||
                                                    c.FinalDate == iniDay && iniHour <= c.FinalHour ||
                                                    c.FinalDate == finDay && finHour == c.FinalHour ||
                                                    c.InitialDate == iniDay && iniHour == c.InitialHour).ToList();
            if (conflictingRaces.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public static bool raceWithNameExists(string rName, string champKey)
        {
            List<SQL_Model.Models.Race> existingRace = Db.Races.Where(c => c.TournamentKey == champKey && c.Name == rName).ToList();
            if (existingRace.Count() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool raceIsNotInChampDates(SQL_Model.Models.Race race)
        {
            SQL_Model.Models.Tournament tour = Db.Tournaments.Find(race.TournamentKey);
            if (
                (
                    tour.InitialDate < race.InitialDate &&
                    race.InitialDate < tour.FinalDate ||
                    tour.InitialDate == race.InitialDate &&
                    race.InitialHour >= tour.InitialHour
                ) && (
                    tour.InitialDate < race.FinalDate &&
                    race.FinalDate < tour.FinalDate ||
                    tour.FinalDate == race.FinalDate &&
                    race.InitialHour >= tour.InitialHour
                )
               ) { return false; }
            return true;
        }

        public int IsValid(SQL_Model.Models.Race race)
        {
            if (IfRacesAtSameTime(race.InitialDate, race.InitialHour, race.FinalDate, race.FinalHour))
            {
                return 3;// There is another race at the same time
            }

            if (raceWithNameExists(race.Name, race.TournamentKey))
            {
                return 1;// Object already in data base
            }

            if (raceIsNotInChampDates(race))
            {
                return 2;// Race is outside tournamnet dates
            }return 0;
            
        }

        int IAddingRules.IsValid(SQL_Model.Models.Tournament tour)
        {
            throw new NotImplementedException();
        }

        int IAddingRules.IsValid(AllUserInfo user)
        {
            throw new NotImplementedException();
        }
    }
     
}
