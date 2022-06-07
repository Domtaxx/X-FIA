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
        [Route("PublicLeague")]
        [HttpGet]
        public ActionResult listByPage(string tournamentKey, int amountByPage, int page)
        {
            try
            {
                SQL_Model.Models.Tournament tour = TournamentFetcher.GetTournament(tournamentKey);
                List<Data_structures.PublicLeagueResponse> res = PublicLeagueFetcher.getPublicLeagueList(tour, page, amountByPage);
                
                return Ok(JsonConvert.SerializeObject(res));
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
                SQL_Model.Models.Tournament tour = TournamentFetcher.GetTournament(tournamentKey);
                List<Data_structures.PublicLeagueResponse> res = PublicLeagueFetcher.getUserPublicLeague(tour, userEmail);
                return Ok(JsonConvert.SerializeObject(res));
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
                SQL_Model.Models.Tournament tour = TournamentFetcher.GetTournament(tournamentKey);
                int amount = 0;
                int residue = 0;
                amount = (tour.UserEmails.Count()*2) / amountByPage;
                residue = (tour.UserEmails.Count() * 2) % amountByPage;
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
                SQL_Model.Models.Tournament tour = TournamentFetcher.GetTournament(tournamentKey);
                int amount = 0;
                amount = tour.UserEmails.Count()*2;
                return Ok(JsonConvert.SerializeObject(amount));
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }
        

    }
}
