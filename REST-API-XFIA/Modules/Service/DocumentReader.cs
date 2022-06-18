using REST_API_XFIA.Data_structures;
using REST_API_XFIA.Modules.BuisnessRules;
using REST_API_XFIA.Modules.Mappers;

namespace REST_API_XFIA.Modules.Service
{
    public class DocumentReader
    {
        public static List<PilotDocument> getPilotsInfo(Stream? stream)
        {
            var pilotsInDoc = new List<PilotDocument>();
            using (var reader = new StreamReader(stream))
            {
                string data;
                while ((data = reader.ReadLine()) != null)
                {
                    var row = data.Split(',');
                    if (FileVerifications.verifyIfRowIsNotValid(row))
                    {
                        throw new InternalDocumentFormatException("Formato dentro del archivo es invalido");
                    }
                    if (row[3].Equals("Piloto"))
                    {
                        pilotsInDoc.Add(DocMapper.MapDocPilot(row));
                    }
                }
            }
            return pilotsInDoc;
        }

        internal static List<TeamDocument> getTeamsInfo(Stream stream)
        {
            var teamsInDoc = new List<TeamDocument>();
            using (var reader = new StreamReader(stream))
            {
                string data;
                while ((data = reader.ReadLine()) != null)
                {
                    var row = data.Split(',');
                    if (FileVerifications.verifyIfRowIsNotValid(row))
                    {
                        throw new InternalDocumentFormatException("Formato dentro del archivo es invalido");
                    }
                    if (row[3].Equals("Constructor"))
                    {
                        teamsInDoc.Add(DocMapper.MapTeams(row));
                    }
                }
            }
            return teamsInDoc;
        }

        public class InternalDocumentFormatException: Exception
        {
            public InternalDocumentFormatException(string message) : base(message)
            {
            }
        }
    }
}
