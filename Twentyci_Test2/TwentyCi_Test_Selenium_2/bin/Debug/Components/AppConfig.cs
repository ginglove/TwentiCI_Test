using Atata.Configuration.Json;

namespace TwentyCi_Test_Selenium_2.Components
{
    public class AppConfig : JsonConfig<AppConfig>
    {
        public string ScreenShotFileOutput { get; set; }

        public string RtbConnectionString { get; set; }
    }
}
