using System;

namespace REST_API_XFIA.Modules
{
    public class CodeGenerator
    {
        private static Random random = new Random();
        public static string generate_key(List<SQL_Model.Models.Tournament> tournaments)
        {
            string key = RandomString(6);
            while (Verifications.IfKeyIsNotRepeatedInDB(key, tournaments))
            {
                key = RandomString(6);
            }
            return key;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
