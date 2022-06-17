namespace REST_API_XFIA.Data_structures
{
    public class PublicLeagueResponse
    {
        public int Id { get; set; }
        public uint Position { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string TeamName { get; set; }
        public int Points { get; set; }
        public string SubteamName { get; set; }
    }
}
