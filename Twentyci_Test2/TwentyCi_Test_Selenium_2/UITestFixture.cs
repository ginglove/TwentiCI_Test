using Atata;
using NUnit.Framework;
using System.IO;
using System.Linq;
using LightHouseScreenTest;
using System.Data.SqlClient;
using System;
using Dapper;
using OpenQA.Selenium;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using OpenQA.Selenium.Interactions;
using LightHouseScreenTest.Components;
using LightHouseScreenTest.Components.JsonObjects;

namespace LightHouseScreenTest
{
    [TestFixture]
    public class UITestFixture
    {
        protected string _updateDBQuery;
        protected int _itemId;

        [SetUp]
        public void SetUp()
        {
            //KillProcesses();
            string filePath = "Configs/Atata";

            AtataContext.Configure()
                .ApplyJsonConfig<AppConfig>(filePath)
#if DEV
                ApplyJsonConfig<AppConfig>(filePath, environmentAlias: "DEV");
#elif QA
                ApplyJsonConfig<AppConfig>(filePath, environmentAlias: "QA");
#elif STAGING
                ApplyJsonConfig<AppConfig>(filePath, environmentAlias: "STAGING");
#endif
                .AddNUnitTestContextLogging()
                .AddScreenshotFileSaving()
                .WithFolderPath(() =>
                {
                    string screenshotFileOutput = AppConfig.Current.ScreenShotFileOutput;
                    string folderPath =
                        $@"Outputs\{AtataContext.BuildStart:yyyy-MM-dd HH_mm_ss}\{AtataContext.Current.TestName}";

                    folderPath = Path.Combine(screenshotFileOutput, folderPath);

                    return folderPath;
                })
                .WithFileName(screenshotInfo =>
                {
                    string fileName =
                        $"{screenshotInfo.Number:D2}-{AtataContext.Current.TestName}{screenshotInfo.Title?.Prepend("-")}";

                    return fileName;
                })
                .UseChrome()
                .WithArguments(AppConfig.Current.Drivers.First(d => d.Type == "chrome").Options.Arguments)
                .WithArguments("--disable-notifications", "--disable-popup-blocking", "--disable-extensions")
                .WithFixOfCommandExecutionDelay()
                .WithLocalDriverPath()
                .Build();
        }

        [TearDown]
        public void TearDown()
        {
            if (AtataContext.Current != null)
            {
                AtataContext.Current.CleanUp();
            }

        }

        //public void KillProcesses()
        //{
        //    Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
        //    try
        //    {
        //        foreach (var chromeDriverProcess in chromeDriverProcesses)
        //        {
        //            chromeDriverProcess.Kill();
        //        }
        //    }
        //    catch (Win32Exception e)
        //    {
        //        e.Message.ToString();
        //    }
        //}
    }
}
