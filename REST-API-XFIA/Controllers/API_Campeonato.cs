using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.SQL_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_XFIA.Controllers
{
    [ApiController]
    [Route("Admin/Campeonato")]
    public class API_Campeonato : Controller
    {
        DB_ProyectEspContext Db = new DB_ProyectEspContext();
        [HttpGet]
        public ActionResult listAll()
        {
            try
            {
                return Ok(Db.Campeonatos.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public ActionResult add()
        {
            try
            {

                return Ok(Db.Campeonatos.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        private int generateNewCode()
        {
            Random ran_gen = new Random();
            List<Campeonato> tournaments = Db.Campeonatos.ToList();


            return 0;
        }

        private bool verifyIfCodeIsRepeated(int num, List<Campeonato> tournaments) {
            foreach (Campeonato tournament in tournaments)
            {
                
            }
            return false;
        }
    }
}
