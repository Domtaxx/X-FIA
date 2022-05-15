using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.SQL_Model.Models;
using REST_API_XFIA.Data_structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace REST_API_XFIA.Controllers
{
    public class API_Carreras : Controller
    {

        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();
        [Route("Admin/Carreras")]
        [HttpGet]
        public ActionResult listAll()
        {
            return Ok(Db.Races.ToList());
        }

        [Route("Admin/Carreras")]
        [HttpPost]
        public ActionResult add([FromBody] Data_structures.Race race){
            try{
                SQL_Model.Models.Race toAdd = new SQL_Model.Models.Race();
                toAdd.Name = race.Nombre;
                toAdd.TournamentKey = race.CampeonatoKey;
                toAdd.InitialHour = DateTime.Parse(race.horaDeInicio).TimeOfDay;
                toAdd.FinalHour = DateTime.Parse(race.horaDeFin).TimeOfDay;
                toAdd.InitialDate = DateTime.Parse(DateTime.Parse(race.fechaDeInicio).ToString("yyyy-MM-dd"));
                toAdd.FinalDate = DateTime.Parse(DateTime.Parse(race.fechaDeFin).ToString("yyyy-MM-dd"));
                toAdd.TrackName = race.NombreDePista;
                toAdd.State = 0;
                toAdd.Country = race.Pais;
                List<SQL_Model.Models.Race> conflictinRaces = Db.Races.Where(c =>
                                                    (toAdd.FinalDate < c.FinalDate && toAdd.FinalDate > c.InitialDate) ||
                                                    (toAdd.InitialDate < c.FinalDate && toAdd.InitialDate > c.InitialDate) ||
                                                    (c.InitialDate == toAdd.InitialDate && toAdd.InitialHour <= c.FinalHour) ||
                                                    (c.FinalDate == toAdd.FinalDate && toAdd.FinalHour >= c.InitialHour)
                                                ).ToList();
                if (conflictinRaces.Count() == 0)
                {
                    if (raceWithNameExists(toAdd.Name, toAdd.TournamentKey))
                    {
                        return BadRequest(JsonConvert.SerializeObject(1));// El objeto ya existe
                    }else if (raceIsNotInChampDates(toAdd))
                    {
                        return BadRequest(JsonConvert.SerializeObject(2));//la fecha esta fuera de la fecha de campeonatos
                    }
                    
                    else{
                        Db.Races.Add(toAdd);
                        Db.SaveChanges();
                        return Ok(JsonConvert.SerializeObject(0));//Se agrego con exito
                    }
                }
                else{
                    return BadRequest(JsonConvert.SerializeObject(3));//Fallo de fechas
                }
            }catch(Exception e)
            {
                return BadRequest(JsonConvert.SerializeObject(4));//Fallo del servidor
            }
        }

        [Route("Pais")]
        [HttpGet]
        public ActionResult listAllCountrys()
        {
            return Ok(Db.Countries.ToList());
        }

        private bool raceWithNameExists(String rName, String champKey)
        {
            List<SQL_Model.Models.Race> existingRace = Db.Races.Where(c=> c.TournamentKey == champKey && c.Name == rName).ToList();
            if(existingRace.Count() > 0)
            {
                return true;
            }                
            return false;
        }
        private bool raceIsNotInChampDates(SQL_Model.Models.Race race)
        {
            SQL_Model.Models.Tournament tour = Db.Tournaments.Find(race.TournamentKey);
            if (
                (
                    tour.InitialDate < race.InitialDate && 
                    race.InitialDate < tour.FinalDate || 
                    tour.InitialDate == race.InitialDate && 
                    race.InitialHour >= tour.InitialHour
                ) && (
                    tour.InitialDate < race.FinalDate && 
                    race.FinalDate < tour.FinalDate || 
                    tour.FinalDate == race.FinalDate && 
                    race.InitialHour >= tour.InitialHour
                )    
               ){return false;}
            return true;
        }

    }
}
