using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.SQL_Model.DB_Context;

namespace REST_API_XFIA.Controllers
{
    public class APICountry : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        [Route("Pais")]
        [HttpGet]
        public ActionResult listAllCountrys()
        {
            try
            {
                return Ok(Db.Countries.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }
    }
}
