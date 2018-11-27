using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyCi_Test_Selenium_2.PageObjects
{
    public class SearchPage
    {
        public string H1_PageTitle = "//h1[@id='headerTitle']";

        public string Options_LocationIdentifier = "//select[@id='locationIdentifier']//option";
        public string Drp_Search_Radius = "//select[@id='radius']";
        public string Options_Radius = "//select[@id='radius']//option";

        public string Drp_PriceRange_Min = "//select[@id='minPrice']";
        public string Options_PriceRange_Min = "//select[@id='minPrice']//option";

        public string Drp_PriceRange_Max = "//select[@id='maxPrice']";
        public string Options_PriceRange_Max = "//select[@id='maxPrice']//option";

        public string Drp_BedRoom_Min = "//select[@id='minBedrooms']";
        public string Drp_BedRoom_Max = "//select[@id='maxBedrooms']";

        public string Btn_FindProperties = "//button[@id='submit']";
    }
}
