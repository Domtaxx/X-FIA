using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Fetcher;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.Modules.BuisnessRules;

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
                SQL_Model.Models.Tournament tour = TournamentFetcher.GetTournament(tournamentKey);

                var errCode = PublicLeagueVerification.verifyPublicLeagueValid(tour, amountByPage, page);
                if (errCode != 0)
                {
                    return BadRequest(errCode);
                }
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
                var DBO = new SQL_Model.DB_Context.RESTAPIXFIA_dbContext();
                SQL_Model.Models.Tournament tour = TournamentFetcher.GetTournament(tournamentKey);
                var errCode = PublicLeagueVerification.verifyUserPublicLeagueValid(tour, userEmail, DBO.Users.ToList());
                if (errCode != 0)
                {
                    return BadRequest(errCode);
                }
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
                if (PublicLeagueVerification.VerifyIfTournamentNotValidAndNumValid(tour, amountByPage))
                {
                    return BadRequest(1);
                }
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
                if (PublicLeagueVerification.VerifyIfTournamentNotValid(tour))
                {
                    return BadRequest(1);
                }
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
