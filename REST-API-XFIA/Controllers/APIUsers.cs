using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.Modules;

namespace REST_API_XFIA.Controllers
{
    [Route("Usuario")]
    public class APIUsers : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        [HttpGet]
        public ActionResult listAllUsers()
        {
            return Ok(Db.Users.ToList());
        }

        [Route("Agregar")]
        [HttpPost]
        public ActionResult AddUser([FromBody]Data_structures.User user) { 
            SQL_Model.Models.User toAdd = DataStrucToSQLStruc.fillSQLUser(user);
            Db.Add(toAdd);
            Db.SaveChanges();
            return Ok(JsonConvert.SerializeObject("Exito"));
        }

    }
}
