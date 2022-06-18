using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.Data_structures;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.Modules.Service;
using REST_API_XFIA.SQL_Model.DB_Context;
using static REST_API_XFIA.Modules.Service.DocumentReader;

namespace REST_API_XFIA.Controllers
{
    [Route("Resultados")]
    public class APIResults : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();


        [HttpPost]
        public ActionResult postFile([FromForm] DataUploded dataUploded)
        {
            try
            {
                List<PilotDocument> pilotsInDoc = new();
                List<TeamDocument> teamsInDoc = new();
                try
                {
                    pilotsInDoc = getPilotsInfo(dataUploded.file.OpenReadStream());
                    teamsInDoc = getTeamsInfo(dataUploded.file.OpenReadStream());
                }catch(InternalDocumentFormatException a)
                {
                    return BadRequest(1);
                }
                List<SQL_Model.Models.Pilot> pilotsInDb = Db.Pilots.ToList();



                return Ok();
            }
            catch (Exception e)
            {
                return Problem("Error del servidor");
            }
        }
    }
}
