using Microsoft.EntityFrameworkCore;
using REST_API_XFIA.Data_structures;
using REST_API_XFIA.SQL_Model.DB_Context;

namespace REST_API_XFIA.Modules.Fetcher
{
    public class PublicLeagueFetcher
    {
        private static RESTAPIXFIA_dbContext Db = new RESTAPIXFIA_dbContext();

        public static List<PublicLeagueResponse> getPublicLeagueList(SQL_Model.Models.Tournament tournament, int page, int amountByPage)
        {
            List<PublicLeagueResponse> AllPublicLeagueRes = getEveryoneInList(tournament);
            List<PublicLeagueResponse> DataInPage = new List<PublicLeagueResponse>();
            int actualPage = 0;
            for (int i = 0; i < AllPublicLeagueRes.Count; i++)
            {
                if (i % amountByPage == 0)
                {
                    actualPage++;
                }

                if (actualPage == page)
                {
                    DataInPage.Add(AllPublicLeagueRes[i]);
                }
                else if (actualPage > page)
                {
                    break;
                }
            }
            return DataInPage;
        }
        public static List<PublicLeagueResponse> getUserPublicLeague(SQL_Model.Models.Tournament tournament, string userEmail)
        {
            PointsFetcher.addPointsForTeam(tournament);
            List<PublicLeagueResponse> AllPublicLeagueRes = getEveryoneInList(tournament);
            List<PublicLeagueResponse> userPos = new List<PublicLeagueResponse>();
            foreach (PublicLeagueResponse response in AllPublicLeagueRes)
            {
                if (response.UserEmail.Equals(userEmail))
                {
                    userPos.Add(response);
                }
            }
            return userPos;
        }


        private static List<PublicLeagueResponse> getEveryoneInList(SQL_Model.Models.Tournament tournament)
        {
            List<PublicLeagueResponse> AllPublicLeagueRes = new List<PublicLeagueResponse>();
            List<SQL_Model.Models.User> users = tournament.UserEmails.ToList();
            foreach (SQL_Model.Models.User user in users)
            {
                PublicLeagueResponse data;
                List<SQL_Model.Models.Subteam> subTeamsInTour = SubTeamFetcher.getLatestSubTeam(user.Email, tournament);
                foreach (SQL_Model.Models.Subteam subTeam in subTeamsInTour)
                {
                    data = new PublicLeagueResponse();
                    var pilotsInSub = Db.HasPilots.Where(HP => HP.SubTeamsId == subTeam.Id).ToList();
                    data.Points = (int)Db.SubteamPoints.Where(STP => STP.SubTeamId == subTeam.Id && STP.TournamentKey == tournament.Key).Single().Points;
                    data.SubteamName = subTeam.Name;
                    data.TeamName = user.TeamsName;
                    data.UserName = user.Username;
                    data.UserEmail = user.Email;
                    AllPublicLeagueRes.Add(data);
                }
            }
            AllPublicLeagueRes = AllPublicLeagueRes.OrderByDescending(PLR => PLR.Points).ToList();
            for (int i = 0; i < AllPublicLeagueRes.Count(); i++)
            {
                AllPublicLeagueRes[i].Position = (uint)i + 1;
            }
            return AllPublicLeagueRes;
        }
    }
}
