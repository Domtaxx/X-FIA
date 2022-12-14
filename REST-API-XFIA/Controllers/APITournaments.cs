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
    [Route("Admin/Campeonato")]
    public class APITournaments : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        [HttpGet]
        public ActionResult listAll()
        {
            try
            {
                return Ok(Db.Tournaments.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }

        [HttpPost]
        public ActionResult add([FromBody] Data_structures.Tournament t)
        {
            try
            {
                SQL_Model.Models.Tournament toAdd = TournamentMapper.fillSQLTournament(t);
                int MsgCode = TournamentVerifications.IsValid(toAdd);
                if (MsgCode != 0)
                {
                    return BadRequest(JsonConvert.SerializeObject(MsgCode));
                }

                Db.Tournaments.Add(toAdd);
                Db.SaveChanges();
                return Ok(JsonConvert.SerializeObject(toAdd.Key));
            }
            catch (Exception e)
            {
                return BadRequest(JsonConvert.SerializeObject(2));
            }
        }

        [HttpGet]
        [Route("Admin/Campeonato/Budget")]
        public ActionResult GetActiveTournamentBudget()
        {
            try{
                if (TournamentVerifications.VerifyIfTournamentsActiveOrFuture())
                {
                    return BadRequest(JsonConvert.SerializeObject(1));
                }
                SQL_Model.Models.Tournament ActiveTournament = TournamentFetcher.GetActiveTournament();
                return Ok(JsonConvert.SerializeObject(ActiveTournament.Budget));
            }catch(Exception e)
            {
                return BadRequest(JsonConvert.SerializeObject(4));
            }
        }
    }
}
