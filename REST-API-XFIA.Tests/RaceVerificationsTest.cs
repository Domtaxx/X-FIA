using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REST_API_XFIA.Modules.BuisnessRules;
using REST_API_XFIA.Modules;

namespace REST_API_XFIA.Tests
{
    public class RaceVerificationsTest
    {

       
        [Fact]
        public void IfRacesAtSameTimeTestInvalidDates()
        {
            Assert.True(RaceVerifications.IfRacesAtSameTime(DateAndTimeParser.parseDate("2022-03-26"), DateAndTimeParser.parseTime("17:00:00"), DateAndTimeParser.parseDate("2022-03-27"), DateAndTimeParser.parseTime("04:00:00")));
            Assert.True(RaceVerifications.IfRacesAtSameTime(DateAndTimeParser.parseDate("2022-03-27"), DateAndTimeParser.parseTime("03:00:00"), DateAndTimeParser.parseDate("2022-03-29"), DateAndTimeParser.parseTime("01:00:00")));
            Assert.True(RaceVerifications.IfRacesAtSameTime(DateAndTimeParser.parseDate("2022-03-25"), DateAndTimeParser.parseTime("16:00:00"), DateAndTimeParser.parseDate("2022-03-26"), DateAndTimeParser.parseTime("18:00:00")));
        }

        [Fact]
        public void IfRacesWithNameExistTestInvalidNames()
        {
            Assert.True(RaceVerifications.raceWithNameExists("Street Circuit", "QWE123"));
        }
        [Fact]
        public void IfRacesWithNameExistTestValidNames()
        {
            Assert.False(RaceVerifications.raceWithNameExists("Street Circuit", "QWE125"));
        }
        [Fact]
        public void raceIsNotInChampDatesTestInvalidDates()
        {
            SQL_Model.Models.Race race = new();
            race.TournamentKey = "QWE123";
            race.InitialDate = DateAndTimeParser.parseDate("24/03/2022");
            race.FinalDate = DateAndTimeParser.parseDate("25/03/2022");
            race.FinalHour = DateAndTimeParser.parseTime("17:00:00");
            race.InitialHour = DateAndTimeParser.parseTime("18:00:00");
            Assert.True(RaceVerifications.raceIsNotInChampDates(race));
        }
        [Fact]
        public void raceIsNotInChampDatesTestValidDates()
        {
            SQL_Model.Models.Race race = new();
            race.TournamentKey = "QWE123";
            race.InitialDate = DateAndTimeParser.parseDate("25/03/2022");
            race.FinalDate = DateAndTimeParser.parseDate("26/03/2022");
            race.FinalHour = DateAndTimeParser.parseTime("17:00:00");
            race.InitialHour = DateAndTimeParser.parseTime("18:00:00");
            Assert.False(RaceVerifications.raceIsNotInChampDates(race));
        }

    }
}
