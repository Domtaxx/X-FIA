namespace REST_API_XFIA.Modules.BuisnessRules
{
    public interface IAddingRules
    {
        public abstract int IsValid(Data_structures.AllUserInfo user);
        public abstract int IsValid(SQL_Model.Models.Tournament tour);
        public abstract int IsValid(SQL_Model.Models.Race race);

        public abstract int IsValid(SQL_Model.Models.Privateleague privateleague);
    }
}
