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
    public class UserVerificationsTest
    {
        [Fact]
        public void VerifyIfSubTeamsNamesAreRepeatedTestValidate()
        {
            Data_structures.AllUserInfo test= new();
            test.NameSubteam1 = "Team 1";
            test.NameSubteam2 = "Team 1";
            Assert.True(UserVerifications.VerifyIfSubTeamsNamesAreRepeated(test));

            Data_structures.AllUserInfo test1 = new();
            test1.NameSubteam1 = "Team 1";
            test1.NameSubteam2 = "Team 2";
            Assert.False(UserVerifications.VerifyIfSubTeamsNamesAreRepeated(test1));
        }

        [Fact]
        public void VerifyIfTeamsNamesAreRepeatedTestValidate()
        {
            Data_structures.AllUserInfo test = new();
            test.TeamsName = "Los tornados locos";
            Assert.True(UserVerifications.VerifyIfTeamNameIsRepeated(test));

            Data_structures.AllUserInfo test1 = new();
            test1.NameSubteam1 = "AAAAAAAAAAAAAAAAA";
            Assert.False(UserVerifications.VerifyIfTeamNameIsRepeated(test1));
        }
        [Fact]
        public void VerifyIfUserHasAccountTestValidate()
        {
            Data_structures.AllUserInfo test = new();
            test.Email = "briwag88@hotmail.com";
            Assert.True(UserVerifications.VerifyIfUserHasAccount(test));

            Data_structures.AllUserInfo test1 = new();
            test1.Email = "Prueba@gmail.com";
            Assert.False(UserVerifications.VerifyIfUserHasAccount(test1));
        }
    }
}
