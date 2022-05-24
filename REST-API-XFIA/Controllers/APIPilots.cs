using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.Modules;

namespace REST_API_XFIA.Controllers
{
    public class APIPilots : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        [Route("Pilotos/Todos")]
        [HttpGet]
        public ActionResult ListAll()
        {
            try {
                return Ok(JsonConvert.SerializeObject(Db.Pilots.Include(P=>P.CountryNameNavigation).Include(P=>P.RealTeamsNameNavigation).ToList().OrderBy(p => p.Lastname)));
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }

        [Route("Pilotos/Pages")]
        [HttpGet]
        public ActionResult ListPilotByPage(int page, int amountByPage)
        {
            try
            {
                List<SQL_Model.Models.Pilot> pilotList = Verifications.getPilotSubList(Db.Pilots.ToList(), page, amountByPage);
                return Ok(JsonConvert.SerializeObject(pilotList));
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }

        
    }
}
