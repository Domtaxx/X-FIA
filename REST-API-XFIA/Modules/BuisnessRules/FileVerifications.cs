namespace REST_API_XFIA.Modules.BuisnessRules
{
    public class FileVerifications
    {
        public bool verifyIfRowIsNotValid(Stream stream) 
        {
            using (var reader = new StreamReader(stream))
            {
                string data;
                while ((data = reader.ReadLine()) != null)
                {
                    var row = data.Split(',');
                    if (row.Length != 16)
                    {
                        return true;

                    }
                    foreach (string item in row)
                    {
                        if (item.Equals(""))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}