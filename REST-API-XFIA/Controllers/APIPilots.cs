using Microsoft.AspNetCore.Mvc;
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
            return Ok(JsonConvert.SerializeObject(Db.Pilots.ToList()));
        }

        [Route("Pilotos/Pages")]
        [HttpGet]
        public ActionResult ListPilotByPage(int page, int amountByPage)
        {
            List<SQL_Model.Models.Pilot> pilotList = Verifications.getPilotSubList(Db.Pilots.ToList(),page,amountByPage);
            return Ok(JsonConvert.SerializeObject(pilotList));
        }

        
    }
}
