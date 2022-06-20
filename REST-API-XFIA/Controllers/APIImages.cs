using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.SQL_Model.DB_Context;
using System.IO;
using System.Reflection;

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
                string DirPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)+ path;

                var contentType = "image/" + path.Substring(path.Length - 3);
                var stream = System.IO.File.OpenRead(DirPath);
                return new FileStreamResult(stream, contentType);
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }
    }
}
