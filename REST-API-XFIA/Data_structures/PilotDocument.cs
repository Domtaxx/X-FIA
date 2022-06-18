namespace REST_API_XFIA.Data_structures
{
    public class PilotDocument
    {
        public PilotDocument()
        {
            team = "";
            name = "";
            price = -1;
            classificationPos = -1;
            Q1 = false;
            Q2 = false;
            Q3 = false;
            didNotDoClassification = false;
            disqClassification = false;
            racePos = -1;
            didFastestLap = false;
            wonParter = false;
            didNotDoRace = false;
            disqRace = false;

        }
        public string id { get; set; }
        public string team { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public int price { get; set; }
        public int classificationPos{ get; set; }
        public bool Q1 { get; set; }
        public bool Q2 { get; set; }
        public bool Q3 { get; set; }
        public bool didNotDoClassification { get; set; }
        public bool disqClassification { get; set; }
        public int racePos { get; set; }
        public bool didFastestLap { get; set; }
        public bool wonParter { get; set; }
        public bool didNotDoRace { get; set; }
        public bool disqRace { get; set; }
    }
}
