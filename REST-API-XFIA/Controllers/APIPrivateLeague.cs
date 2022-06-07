using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.SQL_Model.Models;
using REST_API_XFIA.Data_structures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using REST_API_XFIA.Modules.BuisnessRules;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.Modules.Fetcher;
namespace REST_API_XFIA.Controllers
{
    [ApiController]
    [Route("User/PrivateLeague")]

    
    public class APIPrivateLeague : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        
        [HttpPost]
        public ActionResult addPrivateLeague([FromBody] Data_structures.PrivateLeague privateLeague)
        {
            try
            {
                SQL_Model.Models.Privateleague toAdd = PrivateLeagueMapper.fillSQLPrivateLeague(privateLeague);
                if (PrivateLeagueVerification.privateLeagueIsInDatabase(toAdd.Name))
                {
                    return BadRequest();
                }
                Db.Privateleagues.Add(toAdd);
                Db.SaveChanges();
                return Ok(false);
            }
            catch(Exception e)
            {
                return BadRequest(JsonConvert.SerializeObject(2));
            }
        }

        [HttpDelete]
        public ActionResult deleteUserFromPrivateLeague(string userEmail)
        {
            SQL_Model.Models.User user = Db.Users.Find(userEmail);
            SQL_Model.Models.Privateleague privateLeague;
            if (user != null)
            {
                string privateLeagueName = user.PrivateLeagueName;
                privateLeague = Db.Privateleagues.Find(privateLeagueName);

                user.PrivateLeagueName = null;
                Db.Users.Update(user);
                Db.SaveChanges();
                List<SQL_Model.Models.User> users;
                users = Db.Users.Where(U => U.PrivateLeagueName==privateLeagueName).ToList();
                return Ok(PrivateLeagueVerification.privateLeagueIsActive(users));
                
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(2));
            }
            
        }

        [HttpPost]
        [Route("User/PrivateLeague/NewMember")]
        public ActionResult addUserToPrivateLeague([FromBody] Data_structures.UserToPrivateLeague userToPrivateLeague)
        {
            return Ok(0);
            SQL_Model.Models.User user = Db.Users.Find(userToPrivateLeague.userEmail);
            SQL_Model.Models.Privateleague privateLeague = Db.Privateleagues.Where(P => P.PrivateLeagueKey == userToPrivateLeague.privateLeagueKey).Single();
            if (privateLeague != null)
            {
                user.PrivateLeagueName = privateLeague.Name;
                Db.Users.Update(user);
                Db.SaveChanges();
                List<SQL_Model.Models.User> users;
                users = Db.Users.Where(U => U.PrivateLeagueName == privateLeague.Name).ToList();
                return Ok(PrivateLeagueVerification.privateLeagueIsActive(users));
            }
        }
        [HttpGet]
        public ActionResult getAllPrivateLeagueMembers(string userEmail)
        {
            try
            {
                SQL_Model.Models.Users user = Db.Users.Find(userEmail);
                SQL_Model.Models.Privateleague privateLeague = Db.Privateleagues.Find(user.PrivateLeagueName);
                SQL_Model.Models.Tournament tournament = TournamentFetcher.GetTournament(privateLeague.TournamentKey);
                List<Data_structures.PublicLeagueResponse> res = PrivateLeagueFetcher.getEveryoneInList(privateLeague.privateLeagueName, tournament);
                return Ok(JsonConvert.SerializeObject(res));
            }
            catch
            {
                return BadRequest(2);
            }
            
        }
    }
}
