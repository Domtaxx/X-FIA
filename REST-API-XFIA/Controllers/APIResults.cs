using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.SQL_Model.DB_Context;

namespace REST_API_XFIA.Controllers
{
    [Route("Resultados")]
    public class APIResults : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();


        [HttpPost]
        public ActionResult postFile([FromForm] Data_structures.DataUploded dataUploded)
        {
            try
            {
                using (var fileStream = dataUploded.file.OpenReadStream())
                using (var reader = new StreamReader(fileStream))
                {
                    string data;
                    while ((data = reader.ReadLine()) != null)
                    {
                        var row = data.Split(',');

                    }
                }

                return Ok(Db.Countries.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }
    }
}
