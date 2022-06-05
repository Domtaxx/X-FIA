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
        public ActionResult add([FromBody] Data_structures.PrivateLeague privateLeague)
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
                return Ok(JsonConvert.SerializeObject(toAdd));
            }
            catch(Exception e)
            {
                return BadRequest(JsonConvert.SerializeObject(2));
            }
        }

        [HttpDelete]
        public ActionResult deleteUser(string userEmail)
        {
            SQL_Model.Models.User user = Db.Users.Find(userEmail);
            if (user != null)
            {
                user.PrivateLeagueName = null;
                Db.Users.Update(user);
                Db.SaveChanges();
                return Ok(JsonConvert.SerializeObject(user));
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(2));
            }
            
        }
    }
}
