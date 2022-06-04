using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using REST_API_XFIA.SQL_Model.DB_Context;
namespace REST_API_XFIA.Controllers
{
    public class APIRealTeams : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        [Route("Escuderias/Todos")]
        [HttpGet]
        public ActionResult ListAll()
        {
            try
            {
                return Ok(
                            JsonConvert.SerializeObject(Db.Realteams.ToList(), Formatting.Indented,
                            new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects })
                          );
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }
    }
}
