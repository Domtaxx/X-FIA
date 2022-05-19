using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.SQL_Model.Models;
using REST_API_XFIA.Data_structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using REST_API_XFIA.Modules;

namespace REST_API_XFIA.Controllers
{
    public class APIRaces : Controller
    {

        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        [Route("Admin/Carreras")]
        [HttpGet]
        public ActionResult listAll()
        {
            return Ok(Db.Races.ToList());
        }

        [Route("Admin/Carreras")]
        [HttpPost]
        public ActionResult add([FromBody] Data_structures.Race r){
            try{
                SQL_Model.Models.Race toAdd = Modules.DataStrucToSQLStruc.fillSQLRace(r);
                if (Verifications.IfRacesAtSameTime(r.fechaDeInicio,r.horaDeInicio,r.fechaDeFin,r.horaDeFin))
                {
                    return BadRequest(JsonConvert.SerializeObject(3));// There is another race at the same time
                }
                
                if (Verifications.raceWithNameExists(toAdd.Name, toAdd.TournamentKey))
                {
                    return BadRequest(JsonConvert.SerializeObject(1));// Object already in data base
                }

                if (Verifications.raceIsNotInChampDates(toAdd))
                {
                    return BadRequest(JsonConvert.SerializeObject(2));// Race is outside tournamnet dates
                }

                Db.Races.Add(toAdd);
                Db.SaveChanges();
                return Ok(JsonConvert.SerializeObject(0));// Added succesfully
            }catch(Exception e)
            {
                return BadRequest(JsonConvert.SerializeObject(4));//Server failed
            }
        }
    }
}
