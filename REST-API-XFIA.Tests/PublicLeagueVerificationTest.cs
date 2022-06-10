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
            u1 = new User();
            u1.Email = "Alvaov@estudiantec.cr";
            u2 = new User();
            u2.Email = "briwag88@estudiantec.cr";
            u3 = new User();
            u3.Email = "lazh@estudiantec.cr";



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
        public void VerifyIfSubTeamsNamesAreRepeatedTestValidate()
        {
            User u1 = new User();

            Tournament tour = new Tournament();
            tour.Budget = 45;
            tour.FinalDate = new DateTime(2001, 6, 6);
            tour.InitialDate = new DateTime(2001, 5, 6);
            tour.InitialHour = new TimeSpan(12, 0, 0);
            tour.FinalHour = new TimeSpan(12, 0, 0);
            tour.Key = "123Q56";
            tour.Name = "TESTING 12345123";
            tour.Rules = "algo de reglas";

            Data_structures.AllUserInfo test= new();
            
        }
    }
}
