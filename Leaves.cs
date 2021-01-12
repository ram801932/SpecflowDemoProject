using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using DeltaHRMS.Pages;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Data;
using AventStack.ExtentReports;
using System.Reflection;
using NUnit.Framework.Interfaces;

namespace com.DeltaHRMS
{

    public class Leaves : BaseClass
    {


        [Test] //, Order(1), Category("Leaves")]
        public void AnEmployeeTableLeaves()
        {
            test = extent.CreateTest(MethodBase.GetCurrentMethod().Name);

            //Go to Self-service page
            driver.FindElement(By.XPath("//div[@class='side-menu-thumbnail']//li[text()='Self Service']")).Click();
            test.Pass("Clicked on Self-service ", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            //Go to Leaves
            driver.FindElement(By.XPath("//span[@id='acc_li_toggle_31']/b[text()='Leaves']")).Click();
            // test.Pass("Clicked on Leaves", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            //Then select "Employee Leave" tab
            driver.FindElement(By.LinkText("Employee Leave")).Click();
            test.Pass("selected \"Employee leave\" tab under Leaves tab", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            while (true)
            {
                //Go to Test Loop method
                TestLoop();

                //scroll down the page
                IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
                js.ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");

                try
                {
                    var nextpage = driver.FindElement(By.XPath("//a[@class='nextNew']"));

                    // var prevpage = driver.FindElement(By.XPath("//a[@class='previousNew disabled']"));
                    //Console.WriteLine("prevbutton is enabled : " + driver.FindElement(By.XPath("//a[@class='nextNew']")).Enabled);
                    //Console.WriteLine("nextnew is enabled : " + driver.FindElement(By.XPath("//a[@class='nextNew']")).Enabled);
                    //Console.WriteLine("nextnew is displayed : " + driver.FindElement(By.XPath("//a[@class='nextNew']")).Displayed);

                    if (nextpage.GetAttribute("class").Equals("nextNew"))
                    {
                        //click on next page
                        nextpage.Click();
                        Thread.Sleep(3000);

                        //scrollup the page
                        // js.ExecuteScript("window.scrollBy(0,-1000)");
                    }
                }

                catch
                {
                    Console.WriteLine("Reached last page and execution is completed");
                    test.Pass("And fetched all the employee data", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

                    break;
                }

            }



        }


        public void TestLoop()
        {

            Dictionary<string, string> dct = new Dictionary<string, string>();
            List<Dictionary<string, string>> listdct = new List<Dictionary<string, string>>();

            var tablehead = driver.FindElement(By.XPath("//div[@id='manageremployeevacations']//thead"));
            var hrows = tablehead.FindElements(By.TagName("tr"));
            var hdata = hrows[0].FindElements(By.TagName("th"));

            var tablebody = driver.FindElement(By.XPath("//div[@id='manageremployeevacations']//tbody"));
            var tablerows = tablebody.FindElements(By.TagName("tr"));

            //for (int m = 0; m < hdata.Count; m++)
            //{
            //    Console.WriteLine(m+" is m value of header : "+hdata[m].Text);
            //}

            for (int j = 1; j < tablerows.Count; j++)
            {
                var tabledata = tablerows[j].FindElements(By.TagName("td"));

                dct = new Dictionary<string, string>();
                if (tabledata[2].Text.Equals("Earned Leave"))
                {
                    for (int l = 1; l < tabledata.Count; l++)
                    {
                        dct.Add(hdata[l].Text, tabledata[l].Text);

                    }
                    listdct.Add(dct);
                }


            }
            // Console.WriteLine("No.of dcts present are : " + dct.Count);
            // Console.WriteLine("No.of lists present are : " + ldct.Count);


            DataTable dtable = new DataTable();

            try
            {
                foreach (var dcol in listdct[0].Keys)
                {
                    dtable.Columns.Add(dcol);
                }
            }
            catch
            {
                Console.WriteLine("Execution is completed");
            }

            foreach (var ditem in listdct)
            {
                DataRow drow = dtable.NewRow();

                foreach (var column in ditem.Keys)
                {
                    drow[column] = ditem[column];
                    Console.Write(drow[column]);
                }
                Console.WriteLine();
                dtable.Rows.Add(drow);

            }

            //foreach (var text in ldct)
            //{
            //    foreach (var dctitem in text)
            //    {
            //        Console.Write(dctitem.Value+"      ");
            //     }
            //    Console.WriteLine();
            //}


            //Element.Clear();

        }


        [Test, Order(2), Category("Leaves")]
        public void ApplyLeaves()
        {
            string datevalue = DateTime.Now.ToString("yyyy-MM-dd");
            string LeaveType = "Earned Leave";
          
            
            test = extent.CreateTest(MethodBase.GetCurrentMethod().Name);
            //Go to Self-service page
            driver.FindElement(By.XPath("//div[@class='side-menu-thumbnail']//li[text()='Self Service']")).Click();
            test.Pass("Select Leaves tab", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            //Go to Leaves 
            driver.FindElement(By.XPath("//span[@id='acc_li_toggle_31']/b[text()='Leaves']")).Click();

            //click on leave request
            driver.FindElement(By.LinkText("Leave Request")).Click();
            test.Pass("Selected \"leave request\" tab under leaves", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());

            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            js.ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");

            string table = "//table[@class='fc-border-separate']/tbody";
            var tablebody = driver.FindElement(By.XPath(table));

            var tablerows = driver.FindElements(By.TagName("tr"));

            //Console.WriteLine(" count of table rows " + tablerows.Count);
            for (int i = 1; i < tablerows.Count; i++)
            {
                var tabledata = tablerows[i].FindElements(By.TagName("td"));
                // Console.WriteLine("Count of table data " + tabledata.Count);
                for (int j = 1; j < tabledata.Count; j++)
                {
                    if (tabledata[j].GetAttribute("data-date").Equals(datevalue))
                    {
                        tabledata[j].Click();
                        break;
                    }
                }
            }

            //select Leave type dropdown
            driver.FindElement(By.XPath("//span[text()='Select Leave Type']")).Click();

            //Select Earned Leave
            driver.FindElement(By.XPath("//span[contains(.,'" + LeaveType + "')]")).Click();


            // var elementPresent = driver.FindElement(By.XPath("//div[@class='blockUI blockMsg blockPage']//img")).Displayed;
            if (IsElementPresent(By.XPath("//div[@class='blockUI blockMsg blockPage']//img")) == false)
            {
             test.Pass("Earned leave is selected from dropdown", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
            }
            
            //Fetch to whom the user is reporting
            string rptmanager = driver.FindElement(By.Name("rep_mang_id")).GetAttribute("value");
            Console.WriteLine("User is reporting to the :" + rptmanager);

            //Enter the reason
            driver.FindElement(By.Name("reason")).SendKeys("Test Automation");
            test.Pass("Text is entered in Reason Textfield", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
            
            try
            {
                //click on submit button
                driver.FindElement(By.Id("submitbutton")).Click();
                if (IsElementPresent(By.XPath("//div[@class='blockUI blockMsg blockPage']//img")) == false)
                {
                    test.Pass("Clicked on Apply button", MediaEntityBuilder.CreateScreenCaptureFromPath(screencapture()).Build());
                }
                Console.WriteLine("Success message : " + driver.FindElement(By.XPath("//div[@id='messageData']//div")).Text);
            }

            catch (Exception e)
            {
                string erromMessage = driver.FindElement(By.XPath("//span[@id='errors-from_date'][2]")).Text;
                exceptionMessage = e.Message ;
                throw new Exception();
            }
        }

    }

}
