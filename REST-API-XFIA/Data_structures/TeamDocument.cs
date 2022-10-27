namespace REST_API_XFIA.Data_structures
{
    public class TeamDocument
    {
        public TeamDocument()
        {
            id = "";
            name = "";
            price = -1.0;
        }
        public string id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
    }
}
