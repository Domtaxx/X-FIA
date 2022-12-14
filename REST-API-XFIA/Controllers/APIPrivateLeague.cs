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
                SQL_Model.Models.User user = Db.Users.Find(privateLeague.ownerEmail);
                SQL_Model.Models.Privateleague toAdd = PrivateLeagueMapper.fillSQLPrivateLeague(privateLeague);
                int msgCode = PrivateLeagueVerification.isValid(toAdd, user);
                if (msgCode != 0)
                {
                    return BadRequest(JsonConvert.SerializeObject(msgCode));
                }
                
                user.PrivateLeagueName = privateLeague.name;
                Db.Update(user);
                Db.Privateleagues.Add(toAdd);
                Db.SaveChanges();
                string fullPrivateLeagueKey = toAdd.TournamentKey + toAdd.PrivateLeagueKey;
                return Ok(JsonConvert.SerializeObject(fullPrivateLeagueKey));
            }
            catch(Exception e)
            {
                return BadRequest(JsonConvert.SerializeObject(2));
            }
        }

        [HttpDelete]
        public ActionResult deleteUserFromPrivateLeague(string userEmail)
        {
            SQL_Model.Models.User user = Db.Users.Where(U => U.Email == userEmail || U.TeamsName == userEmail).FirstOrDefault();
            SQL_Model.Models.Privateleague privateLeague;
            int msgCode = PrivateLeagueVerification.isValidDelete(user);
            if (msgCode != 0)
            {
                return BadRequest(JsonConvert.SerializeObject(msgCode));
            }
            string privateLeagueName = user.PrivateLeagueName;
            privateLeague = Db.Privateleagues.Find(privateLeagueName);

            user.PrivateLeagueName = null;
            Db.Users.Update(user);
            Db.SaveChanges();
            List<SQL_Model.Models.User> users;
            users = Db.Users.Where(U => U.PrivateLeagueName==privateLeagueName).ToList();
            return Ok(PrivateLeagueVerification.privateLeagueIsActive(privateLeagueName));
            
        }

        [HttpPost]
        [Route("User/PrivateLeague/NewMember")]
        public ActionResult addUserToPrivateLeague([FromBody] Data_structures.UserToPrivateLeague userToPrivateLeague)
        {
            SQL_Model.Models.User user = Db.Users.Find(userToPrivateLeague.userEmail);
            int msgCode = PrivateLeagueVerification.isValid(userToPrivateLeague, user);
            if (msgCode != 0)
            {
                return BadRequest(JsonConvert.SerializeObject(msgCode));
            }
            
            String publickey = userToPrivateLeague.privateLeagueKey.Substring(0, 6);
            String privatekey = userToPrivateLeague.privateLeagueKey.Substring(6, 6);
            SQL_Model.Models.Privateleague privateLeague = Db.Privateleagues.Where(P => P.PrivateLeagueKey == privatekey && P.TournamentKey==publickey).Single();
            user.PrivateLeagueName = privateLeague.Name;
            Db.Users.Update(user);
            Db.SaveChanges();
            return Ok(PrivateLeagueVerification.privateLeagueIsActive(privateLeague.Name));
            
        }
        [HttpGet]
        public ActionResult getAllPrivateLeagueMembers(string userEmail)
        {
            try
            {
                SQL_Model.Models.User user = Db.Users.Find(userEmail);
                int msgCode = PrivateLeagueVerification.isValid(user);
                if (msgCode != 0)
                {
                    return BadRequest(JsonConvert.SerializeObject(msgCode));
                }
                SQL_Model.Models.Privateleague privateLeague = Db.Privateleagues.Find(user.PrivateLeagueName);
                SQL_Model.Models.Tournament tournament = TournamentFetcher.GetTournament(privateLeague.TournamentKey);
                List<Data_structures.PublicLeagueResponse> res = PrivateLeagueFetcher.getEveryoneInList(privateLeague.Name, tournament);
                return Ok(JsonConvert.SerializeObject(res));
            }
            catch
            {
                return BadRequest(2);
            }
            
        }
        [Route("League")]
        [HttpGet]
        public ActionResult getPrivateLeague(string userEmail)
        {
            try
            {
                SQL_Model.Models.User user = Db.Users.Find(userEmail);
                int msgCode = PrivateLeagueVerification.isValid(user);
                if (msgCode != 0)
                {
                    return BadRequest(JsonConvert.SerializeObject(msgCode));
                }
                var res = PrivateLeagueFetcher.GetPrivateleagueData(userEmail);
                return Ok(JsonConvert.SerializeObject(res));
            }
            catch
            {
                return BadRequest(2);
            }
        }
        [Route("League/Owner")]
        [HttpGet]
        public ActionResult isPrivateLeagueOwner(string userEmail, string PrivateLeagueName)
        {
            try
            {
                var privateLeague = Db.Privateleagues.Find(PrivateLeagueName);
                if (privateLeague.OwnerEmail.Equals(userEmail))
                {
                    return Ok(JsonConvert.SerializeObject(1));
                }
                return Ok(JsonConvert.SerializeObject(0));
            }
            catch
            {
                return Problem(); 
            }
        }
    }
}
