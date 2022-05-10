using Microsoft.AspNetCore.Mvc;
using REST_API_XFIA.DB_Context;
using REST_API_XFIA.SQL_Model.Models;
using REST_API_XFIA.Data_structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

                Db.Carreras.Add(toAdd);
                Db.SaveChanges();
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e);
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
