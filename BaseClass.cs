using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;



namespace DeltaHRMS.Pages
{
    public class BaseClass
    {
        public IWebDriver driver;

        public ExtentReports extent;
        public ExtentTest test;
        public int ScreenshotCount;
        public string exceptionMessage;

        [OneTimeSetUp]
        public void LaunchBrowser()
        {
            var reporter = new ExtentV3HtmlReporter(@"C:\Users\ram\source\repos\com.DeltaHRMS\ExtentReport\TestReport.html");
            reporter.LoadConfig(@"C:\Users\ram\source\repos\com.DeltaHRMS\extent-config.xml");

            extent = new ExtentReports();
            extent.AttachReporter(reporter);

            extent.AddSystemInfo("Application ", "Delta HRMS");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Machine", Environment.MachineName);
            extent.AddSystemInfo("Operating System", Environment.OSVersion.VersionString);

            driver = new ChromeDriver();

            driver.Url = "http://deltahrmsqa.deltaintech.com/index.php/";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Manage().Window.Maximize();
        }

        [SetUp]
        public void login()
        {
            //login using HR credentials
            driver.FindElement(By.Id("username")).SendKeys("skoppineni@deltaintech.com");
            driver.FindElement(By.Id("password")).SendKeys("staging@123");
            // test.Info("Entered username and password", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            driver.FindElement(By.Id("loginsubmit")).Click();
            // test.Info("Clicked on submit button ", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

        }

        [TearDown]
        public void logout()
        {
            var isPassFail = TestContext.CurrentContext.Result.Outcome.Status;

            switch (isPassFail)
            {

                case TestStatus.Skipped:
                    test.Skip("Testcase is Skipped", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
                    break;

                case TestStatus.Passed:
                    test.Pass("Testcase is Passed", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
                    break;

                case TestStatus.Warning:
                    test.Warning("Testcase gives Warning", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
                    break;

                case TestStatus.Failed:
                    test.Fail(exceptionMessage, MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
                    break;
            }


            driver.FindElement(By.Id("logoutbutton")).Click();
            // test.Info("Click on logout", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
            driver.FindElement(By.LinkText("Logout")).Click();

        }

        public string screencapture()
        {
            ScreenshotCount += 1;
            var strPath = @"C:\Users\ram\source\repos\com.DeltaHRMS\ExtentReport\screenshot" + ScreenshotCount + ".png";
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(strPath);
            // test.AddScreenCaptureFromPath(strPath);
            return strPath;
        }

        //public void IsDisplayedCaptureScreenshot(IWebElement element, string text)
        //{
        //    if (element.Displayed)
        //    {
        //        test.Pass(text, MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
        //    }
            
        //}

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.Quit();
            extent.Flush();

        }

        public bool IsElementPresent(By by)
        {
            bool isElementDisplayed ;

            while (true)
            {
            try
            {
                if(driver.FindElement(by).Displayed)
                    isElementDisplayed =  true;
            }
            catch (NoSuchElementException)
            {
                    isElementDisplayed = false;
                    break;
            }
            }
            return isElementDisplayed;
        }
    }
}
