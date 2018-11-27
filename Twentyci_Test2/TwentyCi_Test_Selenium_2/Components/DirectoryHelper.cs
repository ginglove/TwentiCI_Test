using System.IO;
using System.Linq;

namespace TwentyCi_Test_Selenium_2.Components
{
    public class DirectoryHelper
    {
        public static string[] GetFiles(string directoryPath)
        {
            return Directory
                .EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                .ToArray();
        }
    }
}
