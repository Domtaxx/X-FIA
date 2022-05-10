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
                Carrera to_add = new Carrera();
                to_add.Nombre = race.Nombre;
                to_add.CampeonatoKey = race.CampeonatoKey;
                to_add.HoraDeInicio = DateTime.Parse(race.horaDeInicio).TimeOfDay;
                to_add.HoraDeFin = DateTime.Parse(race.horaDeFin).TimeOfDay;
                to_add.FechaDeInicio = DateTime.Parse(race.fechaDeInicio);
                to_add.FechaDeFin = DateTime.Parse(race.fechaDeFin);
                to_add.NombreDePista = race.NombreDePista;
                to_add.Estado = 0;
                to_add.Pais = race.Pais;

                Db.Carreras.Add(to_add);
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
