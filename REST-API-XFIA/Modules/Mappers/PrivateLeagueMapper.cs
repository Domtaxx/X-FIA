using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.SQL_Model.DB_Context;
using REST_API_XFIA.Modules.Service;
using REST_API_XFIA.Modules.Fetcher;

namespace REST_API_XFIA.Modules.Mappers
{
    public class PrivateLeagueMapper
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        public static SQL_Model.Models.Privateleague fillSQLPrivateLeague(Data_structures.PrivateLeague privateLeague)
        {
            List<SQL_Model.Models.Privateleague> privateleagues = Db.Privateleagues.ToList();
            SQL_Model.Models.Privateleague toAdd = new SQL_Model.Models.Privateleague();
            toAdd.Name = privateLeague.name;
            toAdd.MaxUser = privateLeague.maxUser;
            toAdd.OwnerEmail = privateLeague.ownerEmail;
            toAdd.TournamentKey = TournamentFetcher.GetActiveTournament().Key;
            toAdd.PrivateLeagueKey = CodeGenerator.generate_key(privateleagues);
            return toAdd;
        }
    }
}
