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

        private static DB_ProyectEspContext Db = new DB_ProyectEspContext();
        [Route("Admin/Carreras")]
        [HttpGet]
        public ActionResult listAll()
        {
            return Ok(Db.Carreras.ToList());
        }

        [Route("Admin/Carreras")]
        [HttpPost]
        public ActionResult add([FromBody] Race race){
            try{
                Carrera toAdd = new Carrera();
                toAdd.Nombre = race.Nombre;
                toAdd.CampeonatoKey = race.CampeonatoKey;
                toAdd.HoraDeInicio = DateTime.Parse(race.horaDeInicio).TimeOfDay;
                toAdd.HoraDeFin = DateTime.Parse(race.horaDeFin).TimeOfDay;
                toAdd.FechaDeInicio = DateTime.Parse(race.fechaDeInicio);
                toAdd.FechaDeFin = DateTime.Parse(race.fechaDeFin);
                toAdd.NombreDePista = race.NombreDePista;
                toAdd.Estado = 0;
                toAdd.Pais = race.Pais;
                List<Carrera> conflictinRaces = Db.Carreras.Where(c =>
                                                    (toAdd.FechaDeFin < c.FechaDeFin && toAdd.FechaDeFin > c.FechaDeInicio) ||
                                                    (toAdd.FechaDeInicio < c.FechaDeFin && toAdd.FechaDeInicio > c.FechaDeInicio) ||
                                                    (c.FechaDeInicio == toAdd.FechaDeInicio && toAdd.HoraDeInicio <= c.HoraDeFin) ||
                                                    (c.FechaDeFin == toAdd.FechaDeFin && toAdd.HoraDeFin >= c.HoraDeInicio)
                                                ).ToList();
                if (conflictinRaces.Count() == 0)
                {
                    Db.Carreras.Add(toAdd);
                    Db.SaveChanges();
                    return Ok(JsonConvert.SerializeObject(0));//Se agrego con exito
                }
                else
                {
                    return BadRequest(JsonConvert.SerializeObject(1));//Fallo de fechas
                }
            }catch(Exception e)
            {
                return BadRequest(JsonConvert.SerializeObject(2));//Fallo del servidor
            }
        }

        [Route("Pais")]
        [HttpGet]
        public ActionResult listAllCountrys()
        {
            return Ok(Db.Paises.ToList());
        }
    }
}
