using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using REST_API_XFIA.DB_Context;
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
    }
}
