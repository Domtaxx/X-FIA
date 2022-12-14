using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Service;

namespace REST_API_XFIA.Modules.Mappers
{
    public class DocMapper
    {
        
        public static Data_structures.PilotDocument MapDocPilot(string[] data)
        {
            var pilot = new Data_structures.PilotDocument();
            pilot.id = data[0];
            pilot.team = data[1];
            pilot.name = data[2].Split(" ")[0];
            pilot.lastName = data[2].Split(" ")[1];
            pilot.price = double.Parse(data[4], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            pilot.classificationPos = Int32.Parse(data[5]);
            pilot.racePos = Int32.Parse(data[11]);
            if (data[6].Equals("Y"))
            {
                pilot.Q1 = true;
            }
            if (data[7].Equals("Y"))
            {
                pilot.Q2 = true;
            }
            if (data[8].Equals("Y"))
            {
                pilot.Q3 = true;
            }
            if (data[9].Equals("Y"))
            {
                pilot.didNotDoClassification = true;
            }
            if (data[10].Equals("Y"))
            {
                pilot.disqClassification = true;
            }
            if (data[12].Equals("Y"))
            {
                pilot.didFastestLap = true;
            }
            if (data[13].Equals("Y"))
            {
                pilot.wonParter = true;
            }
            if (data[14].Equals("Y"))
            {
                pilot.didNotDoRace = true;
            }
            if (data[15].Equals("Y"))
            {
                pilot.disqRace = true;
            }
            
            return pilot;
        }
        public static Data_structures.TeamDocument MapTeams(string[] data)
        {
            var team = new Data_structures.TeamDocument();
            team.id = data[1];
            team.name = data[2];
            team.price = double.Parse(data[4], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            return team;
        }
    }
}
