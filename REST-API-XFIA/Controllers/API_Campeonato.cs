using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.SQL_Model.Models;
using REST_API_XFIA.Data_structures;
using Newtonsoft.Json;
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
        private static DB_ProyectEspContext Db = new DB_ProyectEspContext();
        private static Random random = new Random();
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
        public ActionResult add([FromBody] Tournament tournament)
        {
            try
            {
                Campeonato toAdd = new Campeonato();
                List<Campeonato> tournaments = Db.Campeonatos.ToList();
                
                // filling campeonato to add to data base
                toAdd.Llave = generate_key(tournaments);
                toAdd.Nombre = tournament.nombreCm;
                toAdd.DescripcionDeReglas = tournament.descripcionDeReglas;
                toAdd.HoraDeInicio = DateTime.Parse(tournament.horaDeInicio).TimeOfDay;
                toAdd.HoraDeFin = DateTime.Parse(tournament.horaDeFin).TimeOfDay;
                toAdd.FechaDeInicio = DateTime.Parse(tournament.fechaDeInicio);
                toAdd.FechaDeFin = DateTime.Parse(tournament.fechaDeFin);
                toAdd.Presupuesto = tournament.presupuesto;
                Db.Campeonatos.Add(toAdd);
                Db.SaveChanges();
                if (tournamentIsInDatabase(toAdd.Llave))
                {   
                    return Ok(JsonConvert.SerializeObject(toAdd.Llave));
                }
                else
                {
                    return BadRequest("Dates are not valid");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        private bool tournamentIsInDatabase(string key)
        {
            Campeonato tournamentTested = Db.Campeonatos.Find(key);
            if (tournamentTested == null)
            {
                return false;
            }
            else 
            { 
                return true;
            }
        }
        private string generate_key(List<Campeonato> tournaments)
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
        
        private bool verifyIfKeyIsNotRepeated(string key, List<Campeonato> tournaments) {
            foreach (Campeonato tournament in tournaments)
            {
                if (tournament.Llave == key)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
