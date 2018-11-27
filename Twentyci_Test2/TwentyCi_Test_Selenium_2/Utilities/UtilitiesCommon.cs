using System;
using System.Collections.Generic;
using System.Text;
using Atata;
using OpenQA.Selenium;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using TwentyCi_Test_Selenium_2.Utilities;

namespace TwentyCi_Test_Selenium_2.Utilities
{
    public class UtilitiesCommon
    {
        public IWebDriver driver = WebDriverSingleton.getInstance();
        public void ScrollToBottom()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }
        public void ScrollMiddle()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(50, document.body.scrollHeight);");
        }
        public void ScrollTop()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0);");
        }

        public bool IsElementPresent(string XPath)
        {
            try
            {
                driver.FindElement(By.XPath(XPath));
                Console.WriteLine("Element {0} find by FindElement", XPath);
                return true;
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Cannot find Element {0} find by FindElement", XPath);
                return false;
            }
        }
        public void HoverToElement(string Xpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                Actions action = new Actions(driver);
                action.MoveToElement(element).Perform();
                Console.WriteLine("Hover to Element {0} successful", Xpath);
            }
            catch (ElementNotVisibleException e)
            {
                Console.WriteLine("Hover to Element {0} not successful with error : Element not visible ->>  {1}", Xpath, e);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Hover to Element {0} not successful with error : Element not exist ->>  {1}", Xpath, e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hover to Element {0} not successful with error : Another Error ->>  {1}", Xpath, e);
            }
        }
        public bool Compare(dynamic Actual, dynamic Expected)
        {
            bool value = true;
            try
            {
                if (Actual.Equals(Expected))
                {
                    Console.WriteLine(Actual + " Equals to " + Expected);
                    return true;
                }
                else
                {
                    Console.WriteLine(Actual + " Not Equals to " + Expected);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception is : {0}", e);
            }
            return value;
        }
        public List<int> GetImageHeight(string ImageXpath)
        {
            List<int> GetImageHeight = new List<int>();
            try
            {
                IWebElement element = driver.FindElement(By.XPath(ImageXpath));
                int Width = element.Size.Width;
                int Height = element.Size.Height;
                GetImageHeight.Add(Width);
                GetImageHeight.Add(Height);
                return GetImageHeight;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetImageHeight with" + ImageXpath + " is FAILED with {0}", e);
                return GetImageHeight;
            }
        }
        public void CompareText(string Actual, string Expected)
        {
            try
            {
                if (Actual.Equals(Expected))
                {
                    Console.WriteLine(Actual + " Equals to " + Expected);
                }
                else
                {
                    string msg = Actual + " Not Equals to " + Expected;
                    throw new Exception(msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception is : {0}", e);
            }
        }
        public bool CheckContainText(string CurrentText, string CheckContainText)
        {
            bool value = true;
            try
            {
                if (CurrentText.Contains(CheckContainText))
                {
                    Console.WriteLine(CurrentText + " Contains " + CheckContainText);
                    return value = true;
                }
                else
                {
                    string msg = CurrentText + " Not Contains " + CheckContainText;
                    return value = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception is : {0}", e);
            }
            return value;
        }
        public bool IsElementDisplayedWithInputElement(IWebElement ele)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            bool value = ele.Displayed;
            try
            {
                if (value == true)
                {
                    Console.WriteLine("Element {0} is displayed ", ele);
                    return value = true;
                }
                else
                {
                    Console.WriteLine("Element {0} is not displayed ", ele);
                    return value = false;
                }
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element {0} not displays and return exception : {1}", ele, e);
            }
            return value;
        }
        public bool IsElementDisplayed(string Xpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            bool value = driver.FindElement(By.XPath(Xpath)).Displayed;
            if (value == true)
            {
                Console.WriteLine("Element {0} is displayed ", Xpath);
                return value = true;
            }
            else
            {
                Console.WriteLine("Element {0} is not displayed ", Xpath);
                return value = false;
            }
        }
        public void WaitForPageLoad()
        {
            IWebElement page = null;
            if (page != null)
            {
                var waitForCurrentPageToStale = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitForCurrentPageToStale.Until(ExpectedConditions.StalenessOf(page));
            }
            Thread.Sleep(5000);
            var waitForDocumentReady = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            waitForDocumentReady.Until((wdriver) => (driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete"));
            page = driver.FindElement(By.TagName("html"));
        }
        public void WaitForPageLoadForCheck(int sec)
        {
            IWebElement page = null;
            if (page != null)
            {
                var waitForCurrentPageToStale = new WebDriverWait(driver, TimeSpan.FromSeconds(sec));
                waitForCurrentPageToStale.Until(ExpectedConditions.StalenessOf(page));
            }

            var waitForDocumentReady = new WebDriverWait(driver, TimeSpan.FromSeconds(sec));
            waitForDocumentReady.Until((wdriver) => (driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete"));

            page = driver.FindElement(By.TagName("html"));
        }
        public void DrawCanvas(string Xpath)
        {
            //canvas will take Xpath follow //canvas[]
            IWebElement canvas = driver.FindElement(By.XPath(Xpath));
            string pencil = "//div[@class='viewer-tool viewer-tool-draw active']";
            ClickElement(pencil, 10);
            var size = canvas.Size;
            Actions builder = new Actions(driver);
            builder
            .ClickAndHold(canvas)
            .MoveByOffset(0, 200)
           .MoveByOffset(0, 200).
            MoveByOffset(-200, 0).
            MoveByOffset(-100, 0).Release(canvas).Build();
            builder.Perform();
        }
        public void MoveCanVas(string Canvas)
        {
            IWebElement canvas = driver.FindElement(By.XPath(Canvas));
            var size = canvas.Size;
            Actions action = new Actions(driver);
            action.ClickAndHold(canvas).MoveByOffset(0, 400).Release(canvas).Build();
            action.Perform();
        }
        public void ClickElement(string Xpath, int milisecond)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(Xpath)));
            //IWebElement element = driver.FindElement(By.XPath(Xpath));
            try
            {
                element.Click();
                Thread.Sleep(milisecond);
                Console.WriteLine("Click successful to {0}", element);
            }
            catch (ElementClickInterceptedException e)
            {
                Console.WriteLine("Element not click :{0}", e);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element " + element + " was not found in DOM "
                        + e.StackTrace.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Element " + element + " was not clickable "
                        + e.StackTrace.ToString());
            }
        }
        public void ClickElementWithElementInput(IWebElement element, int milisecond)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            element = wait.Until(ExpectedConditions.ElementToBeClickable(element));
            //IWebElement element = driver.FindElement(By.XPath(Xpath));
            try
            {
                element.Click();
                Thread.Sleep(milisecond);
                Console.WriteLine("Click successful to {0}", element);
            }
            catch (ElementClickInterceptedException e)
            {
                Console.WriteLine("Element not click :{0}", e);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element " + element + " was not found in DOM "
                        + e.StackTrace.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Element " + element + " was not clickable "
                        + e.StackTrace.ToString());
            }
        }
        public void ClickElementNoTime(string Xpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            //IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(Xpath)));
            IWebElement element = driver.FindElement(By.XPath(Xpath));
            try
            {
                element.Click();
                Console.WriteLine("Click successful to {0}", element);
            }
            catch (ElementClickInterceptedException e)
            {
                Console.WriteLine("Element not click :{0}", e);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element " + element + " was not found in DOM "
                        + e.StackTrace.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Element " + element + " was not clickable "
                        + e.StackTrace.ToString());
            }
        }

        public void ClickElementJavaScript(string Xpath, int miliseconds)
        {
            try
            {

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath(Xpath)));
                Thread.Sleep(miliseconds);
                Console.WriteLine("Click successful to {0} by JS", Xpath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Element " + Xpath + " was not clickable "
                        + e.StackTrace.ToString());
            }
        }
        public void RightClick(string Xpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
            try
            {
                Actions action = new Actions(driver).ContextClick(element);
                action.Build().Perform();
                Console.WriteLine("Successfully Right clicked on the element");
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine("Element is not attached to the page document "
                        + e.StackTrace.ToString());
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element " + element + " was not found in DOM "
                        + e.StackTrace.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Element " + element + " was not clickable "
                        + e.StackTrace.ToString());
            }
        }
        public string GetElementText(string Xpath)
        {
            string Text = "";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                Text = element.Text;
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Get Element Text with Exception : {0}", e);
            }
            return Text;
        }
        public string GetCSSValue(string Xpath, string CSSValue)
        {
            string Text = "";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                Text = element.GetCssValue(CSSValue);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Get Element Text with Exception : {0}", e);
            }
            return Text;
        }
        public string GetAttriButeWithEleInput(IWebElement ele, string Attribute)
        {
            string Text = "";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                if (IsElementDisplayedWithInputElement(ele) == true)
                {
                    Text = ele.GetAttribute(Attribute);
                }
                else
                {
                    Console.WriteLine("Element is not displayed : {0}", ele);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Get Element Text with Exception : {0}", e);
            }
            return Text;
        }
        public string GetAttribute(string Xpath, string Attribute)
        {
            string Text = "";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                //IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                IWebElement element = driver.FindElement(By.XPath(Xpath));
                Text = element.GetAttribute(Attribute);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Get Element Text with Exception : {0}", e);
            }
            return Text;
        }
        public bool IsAttributePresent(string Xpath, string Attribute)
        {
            bool result = true;
            string Text = "";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                //IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                IWebElement element = driver.FindElement(By.XPath(Xpath));
                Text = element.GetAttribute(Attribute);
                if (Text != "null")
                {
                    Console.WriteLine("Attribute is displayed");
                    return result = true;
                }
                else
                {
                    Console.WriteLine("Attribute is not displayed");
                    return result = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Get Element Text with Exception : {0}", e);
                return result = false;
            }
        }
        public void InputTextIntoElement(string Xpath, string InputText)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            try
            {
                //IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                IWebElement element = driver.FindElement(By.XPath(Xpath));
                element.SendKeys(InputText);
                Console.WriteLine("Input {0} into element {1} successfully", InputText, element);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Input text into Element: {0}", e);
            }
        }
        public void InputTextIntoElementAndPressEnter(string Xpath, string InputText)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            try
            {
                //IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                IWebElement element = driver.FindElement(By.XPath(Xpath));
                element.SendKeys(InputText);
                element.SendKeys(Keys.Enter);
                Console.WriteLine("Input {0} and Enter into element {1} successfully", InputText, element);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Input text into Element: {0}", e);
            }
        }
        public void ClearTextInTextBox(string Xpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            try
            {
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
                element.Clear();
                Console.WriteLine("Clear text in {0} successfully", element);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Clear text in Textbox: {0}", e);
            }
        }

        public void SlideOnSlider(string SliderHandle, int position)
        {
            try
            {
                IWebElement slider = driver.FindElement(By.XPath(SliderHandle));
                Actions move = new Actions(driver);
                new Actions(driver).DragAndDropToOffset(slider, position, 0).Build().Perform();
                Console.WriteLine("Slide to element {0} successful", SliderHandle);
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                Console.WriteLine("Slide exception is : {0}", e);
            }
        }
        public void NavigateUrl(string URL)
        {
            try
            {
                driver.Navigate().GoToUrl(URL);
                Console.WriteLine("Navigate successful to : {0}", URL);
            }
            catch (Exception e)
            {
                Console.WriteLine("Navigate Exception is : {0}", e);
            }
        }
        public void WaitForAjax()
        {
            while (true) // Handle timeout somewhere
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                    break;
                Thread.Sleep(1000);
            }
        }
        public bool scroll_Page(string Xpath, int scrollPoints)
        {
            IWebElement element = driver.FindElement(By.XPath(Xpath));
            HoverToElement(Xpath);
            try
            {
                Actions dragger = new Actions(driver);
                // drag downwards
                int numberOfPixelsToDragTheScrollbarDown = 10;
                for (int i = 10; i < scrollPoints; i = i + numberOfPixelsToDragTheScrollbarDown)
                {
                    dragger.MoveToElement(element).ClickAndHold().MoveByOffset(0, numberOfPixelsToDragTheScrollbarDown).Release(element).Build().Perform();
                }
                Thread.Sleep(500);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Scroll with Error : {0}", e);
                return false;
            }
        }
        public void ReloadPage()
        {
            Console.WriteLine("Start Reload Page");
            driver.Navigate().Refresh();
            WaitForPageLoad();
        }
        public void CloseDriver()
        {
            driver.Close();
           System.Diagnostics.Process process = new System.Diagnostics.Process();
           System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C  taskkill /im chromedriver.exe /f";
            process.StartInfo = startInfo;
            process.Start();
        }
        public void TakeScreenshot(string ImagePath)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string Datetime = DateTime.Now.ToString("ddmmyy-HhMmss");
            ss.SaveAsFile(ImagePath + "\\" + Datetime + ".png",
            ScreenshotImageFormat.Png);
        }
        public string GetURL()
        {
            string URL = "";
            URL = driver.Url;
            return URL;
        }
        public IList<IWebElement> FindListOfElement(string XPath)
        {
            IList<IWebElement> lstElement = null;
            try
            {
                if (IsElementPresent(XPath) == true)
                {
                    return lstElement = driver.FindElements(By.XPath(XPath));
                }
                else
                {
                    Console.WriteLine("Cannot Find list of Element ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Scroll with Error : {0}", e);
                return lstElement = null;
            }
            return lstElement;
        }

    }
}
