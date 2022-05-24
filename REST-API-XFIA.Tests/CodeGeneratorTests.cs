using REST_API_XFIA.Modules;

namespace REST_API_XFIA.Tests
{
    public class CodeGeneratorTests
    {
        [Fact]
        public void GenerateKeyNotRepeatedTest()
        {
            var tournaments = new List<SQL_Model.Models.Tournament>();
            var toTest = new List<string>();
            for(int i =1; i<50; i++)
            {
                var temp = new SQL_Model.Models.Tournament();
                temp.Key = CodeGenerator.generate_key(tournaments);
                Assert.DoesNotContain(temp.Key ,toTest);
                toTest.Add(temp.Key);
                tournaments.Add(temp);
            }
        }

        [Fact]
        public void RandomStringIsAlfanumericTest()
        {
            string[] invalidChar = {"@", "#", "$", "!", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", ";", ".", ":", ",", "/", "?", "`", "~", "[", "]", "|", "\\", "<", ">", "'", "\""};
            for (int i = 1; i < 50; i++)
            {   
                string Key = CodeGenerator.RandomString(6);
                foreach(string s in invalidChar)
                {
                    Assert.DoesNotContain(s, Key);
                }
            }
        }


    }
}