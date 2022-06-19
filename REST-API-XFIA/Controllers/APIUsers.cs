using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.BuisnessRules;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.Modules.Service;
using REST_API_XFIA.Modules.Fetcher;

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
        [Route("Unico")]
        [HttpGet]
        public ActionResult GetUsers(string userEmail)
        {
            try
            {
                var User = Db.Users.Include(U => U.CountryNameNavigation).Where(U => U.Email == userEmail).Single();
                var res = UserMapper.fillUserResponse(User, SubTeamFetcher.getSubTeamsLatest(userEmail));

                return Ok(JsonConvert.SerializeObject(res, Formatting.Indented,
                            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                         );
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
                Db.ChangeTracker.Clear();
                int MsgCode = UserVerifications.IsValid(allInfo);
                if (MsgCode != 0)
                {
                    return BadRequest(JsonConvert.SerializeObject(MsgCode));
                }
                SQL_Model.Models.User toAdd = UserMapper.fillSQLUser(allInfo, _storageService);
                Db.Users.Add(toAdd);

                List<SQL_Model.Models.Subteam> subteams = UserMapper.fillSubteams(allInfo);
                Db.Subteams.AddRange(subteams);
                Db.SaveChanges();
                
                var pilotConex = UserMapper.fillHasPilots(allInfo, subteams[0].Id, subteams[1].Id);
                Db.HasPilots.AddRange(pilotConex);
                Db.SaveChanges();
                return Ok(MsgCode);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("Modificar/Usuario")]
        [HttpPost]
        public ActionResult AddSubTeams([FromForm] Data_structures.AllUserInfo allInfo)
        {
            try
            {
                Db.ChangeTracker.Clear();
                int MsgCode = UserVerifications.IsValidForModification(allInfo);
                if (MsgCode != 0)
                {
                    return BadRequest(JsonConvert.SerializeObject(MsgCode));
                }
                var user = Db.Users.Find(allInfo.Email);
                user.CountryName = allInfo.CountryName;
                user.TeamsName = allInfo.TeamsName;
                user.Username = allInfo.Username;
                user.Password = allInfo.Password;
                if(allInfo.TeamsLogo != null){ 
                    user.TeamsLogo = _storageService.Upload(allInfo.TeamsLogo, allInfo.Email);
                }
                Db.Users.Update(user);
                Db.SaveChanges();
                List<SQL_Model.Models.Subteam> subteams = UserMapper.fillSubteamsForMod(allInfo);
                var pilotConex = UserMapper.fillHasPilots(allInfo, subteams[0].Id, subteams[1].Id);

                Db.HasPilots.AddRange(pilotConex);
                Db.SaveChanges();
                return Ok(MsgCode);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Login")]
        [HttpGet]
        public ActionResult GetUsers(string userEmail, string password)
        {
            try
            {
                var User = Db.Users.Include(U => U.CountryNameNavigation).Where(U => U.Email == userEmail && U.Password == password).Single();
                var res = UserMapper.fillUserResponse(User, SubTeamFetcher.getSubTeamsLatest(userEmail));

                return Ok(JsonConvert.SerializeObject(res, Formatting.Indented,
                            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                         );
            }
            catch (Exception e)
            {
                return BadRequest(4);
            }
        }

        [Route("SubEquipos")]
        [HttpGet]
        public IActionResult Get(string email)
        {
            try
            {
                return Ok(
                            JsonConvert.SerializeObject(Db.Subteams.Where(st => st.UserEmail == email).Include(st => st.HasPilots).ThenInclude(HP=>HP.Pilot), Formatting.Indented,
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
