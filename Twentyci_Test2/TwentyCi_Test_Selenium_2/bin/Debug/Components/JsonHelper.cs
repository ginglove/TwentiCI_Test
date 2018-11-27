using Newtonsoft.Json;
using System.IO;

namespace TwentyCi_Test_Selenium_2.Components
{
    public class JsonHelper
    {
        public static T JsonObjectFromFile<T>(string filePath)
        {
            using (StreamReader file = File.OpenText(filePath))
            {
                T input = JsonConvert.DeserializeObject<T>(file.ReadToEnd());
                return input;
            }
        }
    }
}
