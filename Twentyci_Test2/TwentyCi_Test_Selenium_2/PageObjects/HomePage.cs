using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyCi_Test_Selenium_2.PageObjects
{
    public class HomePage
    {
        //Menu
        public string Menu_Home_Buy = "//ul[@class='globalNav-content']//li[@class='globalNav-item']//a[@class='globalNav-link' and @href='/property-for-sale.html']";
        //Search
        public string TxtInput_Home_Search_Location = "//form[@id='initialSearch']//input[@id='searchLocation']";
        public string Btn_Home_ForSale = "//form[@id='initialSearch']//button[@id='buy']";
        public string Btn_Home_ToRent = "//form[@id='initialSearch']//button[@id='rent']";

        //Result Search
        public string Option_Result_Search = "//div[@class='pos-rel']//li//a//span";
    }
}
