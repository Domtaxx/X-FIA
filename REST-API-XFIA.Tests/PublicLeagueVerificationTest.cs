using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REST_API_XFIA.Modules.BuisnessRules;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.Modules.Fetcher;
using REST_API_XFIA.SQL_Model.Models;

namespace REST_API_XFIA.Tests
{
    public class PublicLeagueVerificationTest
    {
        List<User> users;
        User u1;
        User u2;
        User u3;
        Tournament tour;
        
        private void init()
        {
            users = new List<User>();
            u1 = new User();
            u1.Email = "Alvaov@estudiantec.cr";
            u2 = new User();
            u2.Email = "briwag88@estudiantec.cr";
            u3 = new User();
            u3.Email = "lazh@estudiantec.cr";

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);

            tour = new Tournament();
            tour.Budget = 45;
            tour.FinalDate = new DateTime(2001, 6, 6);
            tour.InitialDate = new DateTime(2001, 5, 6);
            tour.InitialHour = new TimeSpan(12, 0, 0);
            tour.FinalHour = new TimeSpan(12, 0, 0);
            tour.Key = "123Q56";
            tour.Name = "TESTING 12345123";
            tour.Rules = "algo de reglas";
        }

        [Fact]
        public void verifyUserPublicLeagueValidTest()
        {
            init();
            Assert.Equal(0,PublicLeagueVerification.verifyUserPublicLeagueValid(tour, "briwag88@estudiantec.cr", users));
            Assert.Equal(1, PublicLeagueVerification.verifyUserPublicLeagueValid(null, "briwag88@estudiantec.cr", users));
            Assert.Equal(2, PublicLeagueVerification.verifyUserPublicLeagueValid(tour, "AREALA@estudiantec.cr", users));
        }
        [Fact]
        public void VerifyIfTournamentNotValidAndNumValidTest()
        {
            init();
            Assert.False(PublicLeagueVerification.VerifyIfTournamentNotValidAndNumValid(tour, 1));
            Assert.False(PublicLeagueVerification.VerifyIfTournamentNotValidAndNumValid(tour, 5));
            Assert.True(PublicLeagueVerification.VerifyIfTournamentNotValidAndNumValid(tour, -1));
            Assert.True(PublicLeagueVerification.VerifyIfTournamentNotValidAndNumValid(null, -1));
            Assert.True(PublicLeagueVerification.VerifyIfTournamentNotValidAndNumValid(null, 61));
        }

    }
}
