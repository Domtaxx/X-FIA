﻿namespace REST_API_XFIA.Data_structures
{
    public class AllUserInfo
    {

        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TeamsName { get; set; } = null!;
        public IFormFile TeamsLogo { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string NameSubteam1 { get; set; } = null!;
        public string Car1 { get; set; } = null!;
        public int pilot1Subteam1 { get; set; }
        public int pilot2Subteam1 { get; set; }
        public int pilot3Subteam1 { get; set; }
        public int pilot4Subteam1 { get; set; }
        public int pilot5Subteam1 { get; set; }
        public string NameSubteam2 { get; set; } = null!;
        public string Car2 { get; set; } = null!;
        public int pilot1Subteam2 { get; set; }
        public int pilot2Subteam2 { get; set; }
        public int pilot3Subteam2 { get; set; }
        public int pilot4Subteam2 { get; set; }
        public int pilot5Subteam2 { get; set; }
    }
}
