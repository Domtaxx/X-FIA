using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REST_API_XFIA.Modules.Fetcher;
using REST_API_XFIA.Modules.Mappers;

namespace REST_API_XFIA.Tests
{
    public class FetcherTests
    {
        [Fact]
        public void GetActiveOrFutureTournamentTest() {
            var tour = TournamentFetcher.GetActiveTournament();
            Assert.True(tour.InitialDate > DateTime.Today || (tour.InitialDate == DateTime.Today && tour.InitialHour >= DateTime.Now.TimeOfDay));
        }
    }

}
