using System.IO;

namespace ByteDev.Hibp.IntTests
{
    public static class ApiKeys
    {
        private const string ApiKeyFilePath = @"Z:\Dev\ByteDev.Hibp.apikey";

        public static string Valid { get; }

        public static string NotValid { get; } = "11111111111111111111111111111111";
        
        static ApiKeys()
        {
            Valid = File.ReadAllText(ApiKeyFilePath).Trim();
        }
    }
}