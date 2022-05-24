using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.Modules;
using REST_API_XFIA.Modules.Service;

namespace REST_API_XFIA.Controllers
{
    [Route("Usuario")]
    public class APIUsers : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        private readonly IStorageService _storageService;

        public APIUsers(IStorageService storageService) {
            _storageService = storageService;
        }

        [HttpGet]
        public ActionResult listAllUsers()
        {
            try
            {
                return Ok(Db.Users.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }

        [Route("Agregar")]
        [HttpPost]
        public ActionResult AddUser([FromForm]Data_structures.AllUserInfo allInfo) {
            try
            {
                if (Verifications.VerifyIfUserHasAccount(allInfo))
                {
                    return BadRequest(1);
                }
                if (Verifications.VerifyIfTeamNameIsRepeated(allInfo))
                {
                    return BadRequest(2);
                }
                if (Verifications.VerifyIfSubTeamsNamesAreRepeated(allInfo)){
                    return BadRequest(3);
                }
                SQL_Model.Models.User toAdd = DataStrucToSQLStruc.fillSQLUser(allInfo, _storageService);
                Db.Users.Add(toAdd);

                List<SQL_Model.Models.Subteam> subteams = DataStrucToSQLStruc.fillSubteams(allInfo);
                Db.Subteams.AddRange(subteams);
                Db.SaveChanges();
                
                DataStrucToSQLStruc.fillHasPilots(allInfo, subteams[0], subteams[1]);
                return Ok(0);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("SubEquipos")]
        [HttpGet]
        public IActionResult Get(string email)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(Db.Subteams.Where(st => st.UserEmail == email).Include(st => st.Pilots)));
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
            
        }
    }
}
