using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Fetcher;
using REST_API_XFIA.Modules.Mappers;

namespace REST_API_XFIA.Controllers
{
    
    public class APIPublicLeague : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        [Route("PublicLeague")]
        [HttpGet]
        public ActionResult listByPage(string tournamentKey, int amountByPage, int page)
        {
            try
            {
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }
        [Route("User/PublicLeague")]
        [HttpGet]
        public ActionResult UserPos(string tournamentKey, string userEmail)
        {
            try
            {

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }

        [Route("PublicLeague/MaxPages")]
        [HttpGet]
        public ActionResult MaxPages(string tournamentKey, int amountByPage)
        {
            try
            {
                SQL_Model.Models.Tournament Temp;
                int amount = 0;
                int residue = 0;
                if (tournamentKey == null)
                {
                    Temp = TournamentFetcher.GetActiveTournament();
                    Temp = Db.Tournaments.Include(t => t.UserEmails).Where(t => t.Key == Temp.Key).Single();
                }
                else
                {
                    Temp = Db.Tournaments.Include(t => t.UserEmails).Where(t => t.Key == tournamentKey).Single();
                }
                amount = Temp.UserEmails.Count() / amountByPage;
                residue = Temp.UserEmails.Count() % amountByPage;
                if (residue > 0)
                {
                    amount++;
                }
                return Ok(JsonConvert.SerializeObject(amount));
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }
        [Route("PublicLeague/UserAmount")]
        [HttpGet]
        public ActionResult UserAmount(string tournamentKey)
        {
            try
            {
                SQL_Model.Models.Tournament Temp;
                int amount = 0;
                if (tournamentKey == null)
                {
                    Temp = TournamentFetcher.GetActiveTournament();
                    Temp = Db.Tournaments.Include(t => t.UserEmails).Where(t => t.Key == Temp.Key).Single();
                }
                else
                {
                    Temp = Db.Tournaments.Include(t=>t.UserEmails).Where(t=>t.Key == tournamentKey).Single();
                }
                amount = Temp.UserEmails.Count();
                return Ok(JsonConvert.SerializeObject(amount));
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }

    }
}
