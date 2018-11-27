using System;
using System.Collections.Generic;
using System.Text;
using TwentyCi_Test_Selenium_2.Utilities;
using TwentyCi_Test_Selenium_2.PageObjects;
using TwentyCi_Test_Selenium_2.JsonObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace TwentyCi_Test_Selenium_2.Utilities
{
    public class WebDriverSingleton
    {

        public static IWebDriver driver;

        public static IWebDriver getInstance()
        {
            if (driver == null)
            {
                var options = new ChromeOptions();
                //options.AddArgument("incognito");
                //"--headless", "--disable-gpu", "--window-size=1920,1200"
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;
                //options.AddArguments("--headless", "--disable-gpu", "--window-size=1920,1200");
                options.AddArguments("--start-maximized");
                driver = new ChromeDriver(chromeDriverService, options);
                //driver = new ChromeDriver(options);
            }
            return driver;
        }

    }
    public class TwentyCi_Utilities
    {
        public UtilitiesCommon u = new UtilitiesCommon();
        public HomePage h = new HomePage();
        public SearchPage s = new SearchPage();
        public PropertySearch_ResultPage p = new PropertySearch_ResultPage();
        public Json_TwentyCi j = new Json_TwentyCi();
        IWebDriver driver = WebDriverSingleton.getInstance();
        public void RightMove_StartFunction(Json_TwentyCi j)
        {
            try
            {
                u.NavigateUrl(j.URL);
                Console.WriteLine("<----TwentyCi/Start Function---->");
                RightMove_SearchFunction(j);
                u.CloseDriver();
            }
            catch (Exception e)
            {
                Console.WriteLine("<----TwentyCi/Start Function : FAILED with {0}---->", e);      
                u.CloseDriver();
                Assert.Fail();                
            }
        }
        public void RightMove_SearchFunction(Json_TwentyCi j)
        {
            Console.WriteLine("<----TwentyCi/Start Search Function---->");
            u.InputTextIntoElement(h.TxtInput_Home_Search_Location, j.SearchValue);
            u.ClickElement(h.Btn_Home_ToRent, 1000);
            RightMove_InputSearchValue(j);
            RightMove_CheckResult(j);
        }
        public void RightMove_InputSearchValue(Json_TwentyCi j)
        {
            Console.WriteLine("<----TwentyCi/Start Select Search Value Function---->");
            IList<IWebElement> lst_e_options_LocationIdentifier = driver.FindElements(By.XPath(s.Options_LocationIdentifier));
            try
            {
                for (int i = 0; i < lst_e_options_LocationIdentifier.Count; i++)
                {
                    if (u.Compare(lst_e_options_LocationIdentifier[i].Text, j.CompareSearchValue) == true)
                    {
                        u.ClickElementWithElementInput(lst_e_options_LocationIdentifier[i], 1000);
                    }
                    else { }
                }
                if (j.AreaRadius == "")
                {
                    if (j.MinPrice == "")
                    {
                        u.ClickElement(s.Drp_PriceRange_Max, 1000);
                        string Options_PriceRange_Max_WithValue = "//select[@id='maxPrice']//option[@value='" + j.MaxPrice + "']";
                        u.ClickElement(Options_PriceRange_Max_WithValue, 1000);
                        u.ClickElement(s.Drp_BedRoom_Min, 1000);
                        string Options_BedRoom_Min_WithValue = "//select[@id='minBedrooms']//option[@value='" + j.MinBed + "']";
                        u.ClickElement(Options_BedRoom_Min_WithValue, 1000);
                        u.ClickElement(s.Drp_BedRoom_Max, 1000);
                        string Options_BedRoom_Max_WithValue = "//select[@id='maxBedrooms']//option[@value='" + j.MaxBed + "']";
                        u.ClickElement(Options_BedRoom_Max_WithValue, 1000);
                        if (j.PropertyValue == "")
                        {
                            if (j.AddedToSite == "")
                            {
                                u.ClickElement(s.Btn_FindProperties, 1000);
                            }
                        }
                    }
                    else
                    {
                        u.ClickElement(s.Drp_PriceRange_Min, 1000);
                        string Options_PriceRange_Min_WithValue = "//select[@id='minPrice']//option[@value='" + j.MinPrice + "']";
                        u.ClickElement(Options_PriceRange_Min_WithValue, 1000);
                        u.ClickElement(s.Drp_PriceRange_Max, 1000);
                        string Options_PriceRange_Max_WithValue = "//select[@id='minPrice']//option[@value='" + j.MaxPrice + "']";
                        u.ClickElement(Options_PriceRange_Max_WithValue, 1000);
                        u.ClickElement(s.Drp_BedRoom_Min, 1000);
                        string Options_BedRoom_Min_WithValue = "//select[@id='minBedrooms']//option[@value='" + j.MinBed + "']";
                        u.ClickElement(s.Drp_BedRoom_Max, 1000);
                        string Options_BedRoom_Max_WithValue = "//select[@id='maxBedrooms']//option[@value='" + j.MaxBed + "']";
                        u.ClickElement(Options_BedRoom_Max_WithValue, 1000);
                        if (j.PropertyValue == "")
                        {
                            if (j.AddedToSite == "")
                            {
                                u.ClickElement(s.Btn_FindProperties, 1000);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("<----TwentyCi/Start Select Search Value Function : FAILED with {0}---->", e);
                u.CloseDriver();
                Assert.Fail();
            }
        }
        public void RightMove_CheckResult(Json_TwentyCi j)
        {
            Console.WriteLine("<----TwentyCi/Start Select Search Value Function / Check Result---->");
            string strMaxPrice = u.GetAttribute(p.Select_ResultPage_MaxPrice, "title").Replace("PCM","").Replace(",","").Replace("£","");
            if (u.CheckContainText(strMaxPrice, j.MaxPrice) == true)
            {
                if (u.CheckContainText(u.GetAttribute(p.Select_ResultPage_MinBedRoom, "title"), j.MinBed) == true)
                {
                    if (u.CheckContainText(u.GetAttribute(p.Select_ResultPage_MaxBedRoom, "title"), j.MaxBed) == true)
                    {
                        if (u.CheckContainText(u.GetElementText(p.H1_ResultPage), j.SearchValue) == true)
                        {
                            Console.WriteLine("<----TwentyCi/Start Select Search Value Function / Check Result : PASSED---->");
                        }
                        else {
                            Console.WriteLine("<----TwentyCi/Start Select Search Value Function / Check Result : FAILED with Header---->");
                            u.CloseDriver();
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        Console.WriteLine("<----TwentyCi/Start Select Search Value Function / Check Result : FAILED with Max Bed---->");
                        u.CloseDriver();
                        Assert.Fail();
                    }
                }
                else
                {
                    Console.WriteLine("<----TwentyCi/Start Select Search Value Function / Check Result : FAILED with Min Bed---->");
                    u.CloseDriver();
                    Assert.Fail();
                }
            }
            else {
                Console.WriteLine("<----TwentyCi/Start Select Search Value Function / Check Result : FAILED with Max price---->");
                u.CloseDriver();
                Assert.Fail();
            }
        }
    }
}
