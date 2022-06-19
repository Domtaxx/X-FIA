using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.SQL_Model.Models;
using REST_API_XFIA.Data_structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using REST_API_XFIA.Modules.BuisnessRules;
using REST_API_XFIA.Modules.Mappers;

namespace REST_API_XFIA.Controllers
{
    public class APIRaces : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        [Route("Admin/Carreras")]
        [HttpGet]
        public ActionResult listAll()
        {
            try {
                return Ok(Db.Races.ToList());
            }
            catch (Exception e)
            {
                return Problem();
            }
        }
        [Route("Admin/Torneo/Carreras")]
        [HttpGet]
        public ActionResult listAllRacesInTournament(string tournamentKey)
        {
            try
            {
                var races = Db.Races.Where(R => R.TournamentKey == tournamentKey).ToList();
                return Ok(races);
            }
            catch (Exception e)
            {
                return Problem();
            }
        }

        [Route("Admin/Carreras")]
        [HttpPost]
        public ActionResult add([FromBody] Data_structures.Race r){
            try{
                SQL_Model.Models.Race toAdd = RaceMapper.fillSQLRace(r);
                int MsgCode = RaceVerifications.IsValid(toAdd);
                if (MsgCode != 0)
                {
                    return BadRequest(JsonConvert.SerializeObject(MsgCode));
                }
                Db.Races.Add(toAdd);
                Db.SaveChanges();
                return Ok(JsonConvert.SerializeObject(MsgCode));// Added succesfully
            }catch(Exception e)
            {
                return Problem();//Server failed
            }
        }
    }
}
