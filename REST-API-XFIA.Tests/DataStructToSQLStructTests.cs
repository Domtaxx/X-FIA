using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REST_API_XFIA.Modules;


namespace REST_API_XFIA.Tests
{
    public class DataStructToSQLStructTests
    {
        [Fact]
        public void GetActiveOrFutureTournamentTest() {
            var tour = DataStrucToSQLStruc.GetActiveTournament();
            Assert.True(tour.InitialDate > DateTime.Today || (tour.InitialDate == DateTime.Today && tour.InitialHour >= DateTime.Now.TimeOfDay));
        }
        [Fact]
        public void ParseDateFormatYearMonthDayTest()
        {
            var test1 = DataStrucToSQLStruc.parseDate("2001/5/6");
            Assert.True(DateTime.Compare(new DateTime(2001,5,6), test1) == 0);
        }
        [Fact(Skip = "unsuportted format")]
        public void ParseDAteFormatMonthDayYearTest()
        {
            var test1 = DataStrucToSQLStruc.parseDate("5/28/2001");
            Assert.True(DateTime.Compare(new DateTime(2001, 5, 6), test1) == 0);
        }
        [Fact]
        public void ParseDateFormatDayMonthYearTest()
        {
            var test1 = DataStrucToSQLStruc.parseDate("6/5/2001");
            Assert.True(DateTime.Compare(new DateTime(2001, 5, 6), test1) == 0);
        }
        [Fact]
        public void ParseTimeFormatTwoDotDelimterTest()
        {
            var test1 = DataStrucToSQLStruc.parseTime("12:00:00");
            Assert.True(TimeSpan.Compare(new TimeSpan(12,0,0), test1) == 0);
        }
    }

}
