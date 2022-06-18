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
            if (!data[3].Equals("Piloto") || !data[3].Equals("Constructor"))
            {
                return true;
            }
            if (!Int32.TryParse(data[4],out int res) || !Int32.TryParse(data[5], out int res1)|| !Int32.TryParse(data[11], out int res2))
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
                if(!s.Equals("Y")|| !s.Equals("Y")|| !s.Equals("N/A"))
                {
                    return true;
                }
            }return false;
        }
    }
}