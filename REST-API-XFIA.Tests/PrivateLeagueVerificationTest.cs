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
    public class PrivateLeagueVerificationTest
    {
        List<User> users;
        List<Privateleague> privateLeagues;
        User u1,u2,u3;
        Privateleague pl1,pl2,pl3;
        Tournament tour;


        private void init()
        {
            users = new List<User>();
            u1 = new User();
            u1.Email = "Alvaov@estudiantec.cr";
            u1.PrivateLeagueName = "Carros rapidos";
            u2 = new User();
            u2.Email = "briwag88@estudiantec.cr";
            u2.PrivateLeagueName = "Rayo veloz";
            u3 = new User();
            u3.Email = "lazh@estudiantec.cr";

            users.Add(u1);
            users.Add(u2);
            users.Add(u3);

            privateLeagues = new List<Privateleague>();

            pl1 = new Privateleague();
            pl1.PrivateLeagueKey = "123456";
            pl2 = new Privateleague();
            pl2.PrivateLeagueKey = "ABCDEF";
            pl3 = new Privateleague();
            pl3.PrivateLeagueKey = "123ABC";

            privateLeagues.Add(pl1);
            privateLeagues.Add(pl2);
            privateLeagues.Add(pl3);

            tour = new Tournament();
            tour.Budget = 45;
            tour.FinalDate = new DateTime(2001, 6, 6);
            tour.InitialDate = new DateTime(2001, 5, 6);
            tour.InitialHour = new TimeSpan(12, 0, 0);
            tour.FinalHour = new TimeSpan(12, 0, 0);
            tour.Key = "123Q56";
            tour.Name = "TESTING 12345123";
            tour.Rules = "reglas generales";

        }
        [Fact]
        public void userBelongsToPrivateLeague()
        {
            init();
            Assert.True(PrivateLeagueVerification.userAlreadyHasPrivateLeague(u1));
            Assert.True(PrivateLeagueVerification.userAlreadyHasPrivateLeague(u2));
            Assert.False(PrivateLeagueVerification.userAlreadyHasPrivateLeague(u3));
        }
        [Fact]
        public void privateLeagueExists()
        {
            Assert.True(PrivateLeagueVerification.IfKeyIsRepeatedInDB("123456",privateLeagues));
            Assert.True(PrivateLeagueVerification.IfKeyIsRepeatedInDB("123ABC", privateLeagues));
            Assert.False(PrivateLeagueVerification.IfKeyIsRepeatedInDB("124ABD", privateLeagues));
            Assert.False(PrivateLeagueVerification.IfKeyIsRepeatedInDB("XYZ789", privateLeagues));
        }
    }
}

