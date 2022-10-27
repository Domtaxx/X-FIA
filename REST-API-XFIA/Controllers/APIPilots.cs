using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Fetcher;
using REST_API_XFIA.Modules.Mappers;

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
                return Ok(
                            JsonConvert.SerializeObject(Db.Pilots.Include(P => P.CountryNameNavigation).Include(P => P.RealTeamsNameNavigation).ToList().OrderBy(p => p.Lastname), Formatting.Indented,
                            new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
                          );
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
                List<SQL_Model.Models.Pilot> pilotList = PilotFetcher.getPilotSubList(Db.Pilots.ToList(), page, amountByPage);
                return Ok(JsonConvert.SerializeObject(pilotList, Formatting.Indented,
                            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }

        
    }
}
