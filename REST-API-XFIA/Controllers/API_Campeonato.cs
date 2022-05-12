using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.SQL_Model.Models;
using REST_API_XFIA.Data_structures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace REST_API_XFIA.Controllers
{
    [ApiController]
    [Route("Admin/Campeonato")]
    public class API_Campeonato : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        private static Random random = new Random();
        [HttpGet]
        public ActionResult listAll()
        {
            try
            {
                return Ok(Db.Tournaments.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public ActionResult add([FromBody] Data_structures.Tournament tournament)
        {
            try
            {   
                List<SQL_Model.Models.Tournament> tournaments = Db.Tournaments.ToList();
                SQL_Model.Models.Tournament toAdd = new SQL_Model.Models.Tournament();
                toAdd.Key = generate_key(tournaments);
                toAdd.Name = tournament.nombreCm;
                toAdd.Rules = tournament.descripcionDeReglas;
                toAdd.InitialHour = DateTime.Parse(tournament.horaDeInicio).TimeOfDay;
                toAdd.FinalHour = DateTime.Parse(tournament.horaDeFin).TimeOfDay;
                toAdd.InitialDate = DateTime.Parse(tournament.fechaDeInicio);
                toAdd.FinalDate = DateTime.Parse(tournament.fechaDeFin);
                toAdd.Budget = tournament.presupuesto;
                List<SQL_Model.Models.Tournament> conflictingTournaments = Db.Tournaments.Where(t =>
                                                            (toAdd.FinalDate < t.FinalDate && toAdd.FinalDate > t.InitialDate) ||
                                                            (toAdd.InitialDate < t.FinalDate && toAdd.InitialDate > t.InitialDate) ||
                                                            (t.InitialDate == toAdd.InitialDate && toAdd.InitialHour <= t.FinalHour) ||
                                                            (t.FinalDate == toAdd.FinalDate && toAdd.FinalHour >= t.InitialHour)
                                                          ).ToList();

                if (conflictingTournaments.Count()==0)
                {
                    Db.Tournaments.Add(toAdd);
                    Db.SaveChanges();
                    return Ok(JsonConvert.SerializeObject(toAdd.Key));
                }
                else
                {
                    return BadRequest(JsonConvert.SerializeObject(1));
                }
            }
            catch (Exception e)
            {
                return BadRequest(JsonConvert.SerializeObject(2));
            }
        }
        
        private bool tournamentIsInDatabase(string key)
        {
            SQL_Model.Models.Tournament tournamentTested = Db.Tournaments.Find(key);
            if (tournamentTested == null)
            {
                return false;
            }
            else 
            { 
                return true;
            }
        }
        private string generate_key(List<SQL_Model.Models.Tournament> tournaments)
        {
             string key = RandomString(6);
             while(verifyIfKeyIsNotRepeated(key, tournaments))
             {
                key = RandomString(6);
             }return key;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        private bool verifyIfKeyIsNotRepeated(string key, List<SQL_Model.Models.Tournament> tournaments) {
            foreach (SQL_Model.Models.Tournament tournament in tournaments)
            {
                if (tournament.Key == key)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
