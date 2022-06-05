using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Fetcher;
using REST_API_XFIA.Modules.Mappers;

namespace REST_API_XFIA.Controllers
{
    
    public class APIPublicLeague : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        [Route("PublicLeague")]
        [HttpGet]
        public ActionResult listByPage()
        {
            try
            {
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }

    }
}
