using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REST_API_XFIA.Modules.BuisnessRules;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.Modules;

namespace REST_API_XFIA.Tests
{
    public class TournamentVerificationsTest
    {

        [Fact]
        public void IfKeyIsRepeatedInDBKeyIsRepeatedTest()
        {
            var tournaments = new List<SQL_Model.Models.Tournament>();
            var temp = new SQL_Model.Models.Tournament();
            temp.Key = "USZ435";
            tournaments.Add(temp);
            Assert.True(TournamentVerifications.IfKeyIsRepeatedInDB("USZ435", tournaments));
        }
        [Fact]
        public void IfKeyIsNotRepeatedInDBIsNotRepeatedTest()
        {
            var tournaments = new List<SQL_Model.Models.Tournament>();
            var temp = new SQL_Model.Models.Tournament();
            temp.Key = "USZ435";
            tournaments.Add(temp);
            Assert.False(TournamentVerifications.IfKeyIsRepeatedInDB("USZ437", tournaments));
        }

        [Fact]
        public void IfTournamnetInDBFalseCaseTest()
        {
            Assert.False(TournamentVerifications.tournamentIsInDatabase("USZ437"));
        }
        [Fact]
        public void IfTournamnetInDBTrueCaseTest()
        {
            Assert.True(TournamentVerifications.tournamentIsInDatabase("QWE123"));
        }

        [Fact]
        public void IfTournamnetAtSameTimeTestInvalidDates()
        {
            Assert.True(TournamentVerifications.IfTournamentAtSameTime(DateAndTimeParser.parseDate("2022-03-25"), DateAndTimeParser.parseTime("0:00:00"), DateAndTimeParser.parseDate("2022-03-27"), DateAndTimeParser.parseTime("09:00:00")));
            Assert.True(TournamentVerifications.IfTournamentAtSameTime(DateAndTimeParser.parseDate("2022-03-27"), DateAndTimeParser.parseTime("8:00:00"), DateAndTimeParser.parseDate("2022-03-29"), DateAndTimeParser.parseTime("10:00:00")));
            Assert.True(TournamentVerifications.IfTournamentAtSameTime(DateAndTimeParser.parseDate("2022-03-23"), DateAndTimeParser.parseTime("8:00:00"), DateAndTimeParser.parseDate("2022-03-25"), DateAndTimeParser.parseTime("00:00:00")));
            Assert.False(TournamentVerifications.IfTournamentAtSameTime(DateAndTimeParser.parseDate("27-01-2038"), DateAndTimeParser.parseTime("12:00:00"), DateAndTimeParser.parseDate("29-01-2038"), DateAndTimeParser.parseTime("12:00:00")));
        }
    }
}
