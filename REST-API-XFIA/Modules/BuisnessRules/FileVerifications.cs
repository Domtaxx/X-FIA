namespace REST_API_XFIA.Modules.BuisnessRules
{
    public class FileVerifications
    {
        public static bool verifyIfRowIsNotValid(string[] data) 
        {
            if (data.Length != 16)
            {
                return true;
            }
            if (!data[3].Equals("Piloto") && !data[3].Equals("Constructor"))
            {
                return true;
            }
            if (!Int32.TryParse(data[4],out int res) && !Int32.TryParse(data[5], out int res1) && !Int32.TryParse(data[11], out int res2))
            {
                return true;
            }
            string[] temp = { data[6], data[7], data[8], data[9], data[10], data[12], data[13], data[14], data[15]};
            if (CheckListNotValid(temp))
            {
                return true;
            }
            return false;
        }

        private static bool CheckListNotValid(string[] list)
        {
            foreach (string s in list)
            {
                if(!s.Equals("Y") && !s.Equals("N") && !s.Equals("N/A"))
                {
                    return true;
                }
            }return false;
        }

        public static bool CheckIfAllPilotsNotValid(List<Data_structures.PilotDocument>pilotsInDoc, List<SQL_Model.Models.Pilot> pilotsInDb)
        {
            foreach(SQL_Model.Models.Pilot pilot in pilotsInDb)
            {
                var temp = pilotsInDoc.FindAll(P=> P.id == pilot.Id);
                if(temp.Count > 1 || temp.Count == 0)
                {
                    return true;
                }
            }return false;
        }
        public static bool CheckIfAllTeamsNotValid(List<Data_structures.TeamDocument> TeamsInDoc, List<SQL_Model.Models.Realteam> TeamsInDb)
        {
            foreach (SQL_Model.Models.Realteam team in TeamsInDb)
            {
                var temp = TeamsInDoc.FindAll(T => T.name == team.Name);
                if (temp.Count > 1 || temp.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}