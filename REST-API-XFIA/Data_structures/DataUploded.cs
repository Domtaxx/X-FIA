using System.ComponentModel.DataAnnotations;

namespace REST_API_XFIA.Data_structures
{
    public class DataUploded
    {
        public string tournamentKey { get; set; }
        public string race { get; set; }
        public IFormFile file { get; set; }
        

    }
}

