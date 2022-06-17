using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.SQL_Model.DB_Context;
using System.IO;

namespace REST_API_XFIA.Controllers
{
    public class APIImages : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        [Route("Imagenes")]
        [HttpGet]
        public ActionResult GetImage(string path)
        {
            try
            {
                var contentType = "image/" + path.Substring(path.Length - 3);
                var stream = System.IO.File.OpenRead(path);
                return new FileStreamResult(stream, contentType);
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }
    }
}
