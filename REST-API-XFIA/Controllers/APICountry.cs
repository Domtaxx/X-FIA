using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.DB_Context;

namespace REST_API_XFIA.Controllers
{
    public class APICountry : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        [Route("Pais")]
        [HttpGet]
        public ActionResult listAllCountrys()
        {
            return Ok(Db.Countries.ToList());
        }
    }
}
