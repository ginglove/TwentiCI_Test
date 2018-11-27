using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwentyCi_Test_Selenium_2.Components;
using TwentyCi_Test_Selenium_2.CommonTest;
using TwentyCi_Test_Selenium_2.JsonObjects;
using NUnit.Framework;

namespace TwentyCi_Test_Selenium_2.UnitTest
{
    public class TwentyCi_Test_2_UnitTest:TwentyCi_CommonTest
    {
        public Json_TwentyCi GetJsonFile(string path)
        {
            Json_TwentyCi j = new Json_TwentyCi();
            dynamic input = JsonHelper.JsonObjectFromFile<object>(path);
            j.URL = input.Input.URL.ToString();
            j.SearchValue = input.Input.SearchValue.ToString();
            j.CompareSearchValue = input.Input.CompareSearchValue.ToString();
            j.AreaRadius = input.Input.AreaRadius.ToString();
            j.MinBed = input.Input.MinBed.ToString();
            j.MaxBed = input.Input.MaxBed.ToString();
            j.MinPrice = input.Input.MinPrice.ToString();
            j.MaxPrice = input.Input.MaxPrice.ToString();
            j.PropertyValue = input.Input.PropertyValue.ToString();
            j.AddedToSite = input.Input.AddedToSite.ToString();
            return j;
        }

        [Category("TwentyCi_SearchFunction")]
        [TestCase(@"TestCases\TwentyCi_Test.json", TestName = "TwentyCi_Test")]
        public void TwentyCi_Test(string path)
        {
           TwentyCi_Test_2_Start(GetJsonFile(path));
        }
    }
}
