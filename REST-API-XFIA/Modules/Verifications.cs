using REST_API_XFIA.DB_Context;
namespace REST_API_XFIA.Modules
{
    public class Verifications
    {        
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        public static bool IfKeyIsNotRepeatedInDB(string key, List<SQL_Model.Models.Tournament> tournaments)
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
        public static bool IfTournamentAtSameTime(string iniDay, string iniHour, string finDay, string finHour){

            DateTime initialDay = DataStrucToSQLStruc.parseDate(iniDay);
            TimeSpan initialHour = DataStrucToSQLStruc.parseTime(iniHour);
            DateTime finalDay = DataStrucToSQLStruc.parseDate(finDay);
            TimeSpan finalHour = DataStrucToSQLStruc.parseTime(finHour);
            List<SQL_Model.Models.Tournament> conflictingTournaments = Db.Tournaments.Where(t =>
                                                        (finalDay < t.FinalDate && finalDay > t.InitialDate) ||
                                                        (initialDay < t.FinalDate && initialDay > t.InitialDate) ||
                                                        (t.InitialDate == initialDay && initialHour <= t.FinalHour) ||
                                                        (t.FinalDate == finalDay && finalHour >= t.InitialHour)).ToList();
            if (conflictingTournaments.Count() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool IfRacesAtSameTime(string iniDay, string iniHour, string finDay, string finHour)
        {

            DateTime initialDate = DataStrucToSQLStruc.parseDate(iniDay);
            TimeSpan initialHour = DataStrucToSQLStruc.parseTime(iniHour);
            DateTime finalDate = DataStrucToSQLStruc.parseDate(finDay);
            TimeSpan finalHour = DataStrucToSQLStruc.parseTime(finHour);
            List<SQL_Model.Models.Race> conflictingRaces = Db.Races.Where(c =>
                                                    (finalDate < c.FinalDate && finalDate > c.InitialDate) ||
                                                    (initialDate < c.FinalDate && initialDate > c.InitialDate) ||
                                                    (c.InitialDate == initialDate && initialHour <= c.FinalHour) ||
                                                    (c.FinalDate == finalDate && finalHour >= c.InitialHour)
                                                ).ToList();
            if (conflictingRaces.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public static bool raceWithNameExists(String rName, String champKey)
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

        public static List<SQL_Model.Models.Pilot> getPilotSubList(List<SQL_Model.Models.Pilot> pilots, int page, int amountByPage)
        {
            List<SQL_Model.Models.Pilot> pilotsInPage = new List<SQL_Model.Models.Pilot>();
            int actualPage = 0;
            for (int i=0; i<pilots.Count-1 ;i++)
            {
                if(i%amountByPage == 0)
                {
                    actualPage++;
                }
                
                if(actualPage == page)
                {
                    pilotsInPage.Add(pilots[i]);
                }else if(actualPage > page)
                {
                    break;
                }
            }
            return pilotsInPage;
        }

        public static List<SQL_Model.Models.Realteam> getRealTeamsSubList(List<SQL_Model.Models.Realteam> RealTeams, int page, int amountByPage)
        {
            List<SQL_Model.Models.Realteam> RealTeamsInPage = new List<SQL_Model.Models.Realteam>();
            int actualPage = 0;
            for (int i = 0; i < RealTeams.Count - 1; i++)
            {
                if (i % amountByPage == 0)
                {
                    actualPage++;
                }

                if (actualPage == page)
                {
                    RealTeamsInPage.Add(RealTeams[i]);
                }
                else if (actualPage > page)
                {
                    break;
                }
            }
            return RealTeamsInPage;
        }
        public static bool VerifyIfUserHasAccount(Data_structures.AllUserInfo userInfo)
        {
            var usersWithSameEmail = Db.Users.Where(U => U.Email == userInfo.Email).ToList();
            if (usersWithSameEmail.Count() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool VerifyIfTeamNameIsRepeated(Data_structures.AllUserInfo userInfo)
        {
            var invalidTeams = Db.Users.Where(U => U.TeamsName == userInfo.TeamsName ).ToList();
            if(invalidTeams.Count > 0)
            {
                return true;
            }
            return false;
        }
        public static bool VerifyIfSubTeamsNamesAreRepeated(Data_structures.AllUserInfo userInfo)
        {
            if(userInfo.NameSubteam1 == userInfo.NameSubteam2)
            {
                return true;
            }
            var invalidteams = Db.Subteams.Where(St => St.UserEmail == userInfo.Email && (St.Name == userInfo.NameSubteam1 || St.Name == userInfo.NameSubteam2)).ToList();
            if (invalidteams.Count() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool VerifyIfTournamentsActiveOrFuture()
        {
            List<SQL_Model.Models.Tournament> tournaments = (List<SQL_Model.Models.Tournament>)Db.Tournaments.Where(T => T.InitialDate >= DateTime.Now).ToList();
            if (tournaments.Count()>0) {
                return false;
            }
            return true;
        }

    }
}
