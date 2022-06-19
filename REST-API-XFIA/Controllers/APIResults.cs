using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.Data_structures;
using REST_API_XFIA.Modules.Fetcher;
using REST_API_XFIA.Modules.Mappers;
using REST_API_XFIA.Modules.Service;
using REST_API_XFIA.SQL_Model.DB_Context;
using static REST_API_XFIA.Modules.Service.DocumentReader;

namespace REST_API_XFIA.Controllers
{
    [Route("Resultados")]
    public class APIResults : Controller
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();


        [HttpPost]
        public ActionResult postFile([FromForm] DataUploded dataUploaded)
        {
            try
            {   List<SQL_Model.Models.Pilot> pilotsInDb = Db.Pilots.ToList();
                List<SQL_Model.Models.Realteam> teamsInDb = Db.Realteams.ToList();
                List<PilotDocument> pilotsInDoc = new();
                List<TeamDocument> teamsInDoc = new();
                try
                {
                    if (!dataUploaded.file.ContentType.Substring(dataUploaded.file.ContentType.Length - 3).Equals("csv"))
                    {
                        throw (new InternalDocumentFormatException("Formato Invalido de archivo"));
                    }
                    pilotsInDoc = getPilotsInfo(dataUploaded.file.OpenReadStream());
                    teamsInDoc = getTeamsInfo(dataUploaded.file.OpenReadStream());

                    
                }catch(InternalDocumentFormatException a)
                {
                    return BadRequest(1);
                }
                List<SQL_Model.Models.PilotRace> pilotRaces = PointsFetcher.getPilotRaces(pilotsInDoc, dataUploaded.race, dataUploaded.tournamentKey);
                List<SQL_Model.Models.RealTeamRace> carRaces = PointsFetcher.getRealTeamRaces(teamsInDoc, pilotRaces, dataUploaded.race, dataUploaded.tournamentKey);
                var pointsForRace = Db.PilotRaces.Where(PR => PR.Name.Equals(dataUploaded.race) && PR.TournamentKey.Equals(dataUploaded.tournamentKey)).ToList();
                var pointsForTeamsRace = Db.RealTeamRaces.Where(PR => PR.Name.Equals(dataUploaded.race) && PR.TournamentKey.Equals(dataUploaded.tournamentKey)).ToList();
                if (pointsForRace.Count == 0)
                {
                    Db.PilotRaces.AddRange(pilotRaces);
                    Db.RealTeamRaces.AddRange(carRaces);
                }
                else
                {
                    foreach(SQL_Model.Models.PilotRace PR in pilotRaces)
                    {
                        var tempPilot = pointsForRace.Find(PRP => PRP.PilotId.Equals(PR.PilotId));
                        tempPilot.Points = PR.Points;
                        Db.PilotRaces.Update(tempPilot);
                    }
                    foreach (SQL_Model.Models.RealTeamRace RTR in pointsForTeamsRace)
                    {
                        var tempTeam = pointsForTeamsRace.Find(RT => RT.RealTeamName.Equals(RTR.RealTeamName));
                        tempTeam.Points = RTR.Points;
                        Db.RealTeamRaces.Update(tempTeam);
                    }
                }
                Db.SaveChanges();
                var tour = Db.Tournaments.Include(T => T.UserEmails).Where(T => T.Key.Equals(dataUploaded.tournamentKey)).Single();
                PointsFetcher.addPointsForTeam(tour);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem("Error del servidor");
            }
        }
    }
}
