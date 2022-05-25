using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REST_API_XFIA.Modules;

namespace REST_API_XFIA.Tests
{
    public class VerificationsTest
    {

        [Fact]
        public void IfKeyIsRepeatedInDBKeyIsRepeatedTest()
        {
            var tournaments = new List<SQL_Model.Models.Tournament>();
            var temp = new SQL_Model.Models.Tournament();
            temp.Key = "USZ435";
            tournaments.Add(temp);
            Assert.True(Verifications.IfKeyIsRepeatedInDB("USZ435", tournaments));
        }
        [Fact]
        public void IfKeyIsNotRepeatedInDBIsNotRepeatedTest()
        {
            var tournaments = new List<SQL_Model.Models.Tournament>();
            var temp = new SQL_Model.Models.Tournament();
            temp.Key = "USZ435";
            tournaments.Add(temp);
            Assert.False(Verifications.IfKeyIsRepeatedInDB("USZ437", tournaments));
        }

        [Fact]
        public void IfTournamnetInDBFalseCaseTest()
        {
            Assert.False(Verifications.tournamentIsInDatabase("USZ437"));
        }
        [Fact]
        public void IfTournamnetInDBTrueCaseTest()
        {
            Assert.True(Verifications.tournamentIsInDatabase("QWE123"));
        }

        [Fact]
        public void IfTournamnetAtSameTimeTestInvalidDates()
        {
            Assert.True(Verifications.IfTournamentAtSameTime("2022-03-25", "0:00:00", "2022-03-27", "09:00:00"));
            Assert.True(Verifications.IfTournamentAtSameTime("2022-03-27", "8:00:00", "2022-03-29", "10:00:00"));
            Assert.True(Verifications.IfTournamentAtSameTime("2022-03-23", "8:00:00", "2022-03-25", "00:00:00"));
        }
        [Fact]
        public void IfRacesAtSameTimeTestInvalidDates()
        {
            Assert.True(Verifications.IfTournamentAtSameTime("2022-03-26", "17:00:00", "2022-03-27", "04:00:00"));
            Assert.True(Verifications.IfTournamentAtSameTime("2022-03-27", "03:00:00", "2022-03-29", "01:00:00"));
            Assert.True(Verifications.IfTournamentAtSameTime("2022-03-25", "16:00:00", "2022-03-26", "18:00:00"));
        }

        [Fact]
        public void IfRacesWithNameExistTestInvalidNames()
        {
            Assert.True(Verifications.raceWithNameExists("Street Circuit", "QWE123"));
        }
        [Fact]
        public void IfRacesWithNameExistTestValidNames()
        {
            Assert.False(Verifications.raceWithNameExists("Street Circuit", "QWE125"));
        }
        [Fact]
        public void raceIsNotInChampDatesTestInvalidDates()
        {
            SQL_Model.Models.Race race = new();
            race.TournamentKey = "QWE123";
            race.InitialDate = DataStrucToSQLStruc.parseDate("24/03/2022");
            race.FinalDate = DataStrucToSQLStruc.parseDate("25/03/2022");
            race.FinalHour = DataStrucToSQLStruc.parseTime("17:00:00");
            race.InitialHour = DataStrucToSQLStruc.parseTime("18:00:00");
            Assert.True(Verifications.raceIsNotInChampDates(race));
        }
        [Fact]
        public void raceIsNotInChampDatesTestValidDates()
        {
            SQL_Model.Models.Race race = new();
            race.TournamentKey = "QWE123";
            race.InitialDate = DataStrucToSQLStruc.parseDate("25/03/2022");
            race.FinalDate = DataStrucToSQLStruc.parseDate("26/03/2022");
            race.FinalHour = DataStrucToSQLStruc.parseTime("17:00:00");
            race.InitialHour = DataStrucToSQLStruc.parseTime("18:00:00");
            Assert.False(Verifications.raceIsNotInChampDates(race));
        }

        [Fact]
        public void VerifyIfSubTeamsNamesAreRepeatedTestValidate()
        {
            Data_structures.AllUserInfo test= new();
            test.NameSubteam1 = "Team 1";
            test.NameSubteam2 = "Team 1";
            Assert.True(Verifications.VerifyIfSubTeamsNamesAreRepeated(test));

            Data_structures.AllUserInfo test1 = new();
            test1.NameSubteam1 = "Team 1";
            test1.NameSubteam2 = "Team 2";
            Assert.False(Verifications.VerifyIfSubTeamsNamesAreRepeated(test1));
        }

        [Fact]
        public void VerifyIfTeamsNamesAreRepeatedTestValidate()
        {
            Data_structures.AllUserInfo test = new();
            test.TeamsName = "Los tornados locos";
            Assert.True(Verifications.VerifyIfTeamNameIsRepeated(test));

            Data_structures.AllUserInfo test1 = new();
            test1.NameSubteam1 = "AAAAAAAAAAAAAAAAA";
            Assert.False(Verifications.VerifyIfTeamNameIsRepeated(test1));
        }
        [Fact]
        public void VerifyIfUserHasAccountTestValidate()
        {
            Data_structures.AllUserInfo test = new();
            test.Email = "briwag88@hotmail.com";
            Assert.True(Verifications.VerifyIfUserHasAccount(test));

            Data_structures.AllUserInfo test1 = new();
            test1.Email = "Prueba@gmail.com";
            Assert.False(Verifications.VerifyIfUserHasAccount(test1));
        }
    }
}
