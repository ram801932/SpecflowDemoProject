using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using DeltaHRMS.Pages;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Data;

namespace com.DeltaHRMS
{
    class AnotherValidation : BaseClass
    {
       

        public void testleaves()
        {
            //login using HR credentials
            driver.FindElement(By.Id("username")).SendKeys("skoppineni@deltaintech.com");
            driver.FindElement(By.Id("password")).SendKeys("staging@123");
            driver.FindElement(By.Id("loginsubmit")).Click();

            //Go to Self-service page
            driver.FindElement(By.XPath("//div[@class='side-menu-thumbnail']//li[text()='Self Service']")).Click();

            //Go to Leaves
            driver.FindElement(By.XPath("//span[@id='acc_li_toggle_31']/b[text()='Leaves']")).Click();

            //Then select "Employee Leave" tab
            driver.FindElement(By.LinkText("Employee Leave")).Click();

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


                    if (nextpage.GetAttribute("class").Equals("nextNew"))
                    {
                        //click on next page
                        nextpage.Click();
                        Thread.Sleep(2000);

                        //scrollup the page
                        js.ExecuteScript("window.scrollBy(0,-1000)");
                    }
                }

                catch
                {
                    Console.WriteLine("Reached last page and execution is completed");
                    break;
                }

            }



        }


        public void TestLoop()
        {

            Dictionary<string, string> dct = new Dictionary<string, string>();
            List<Dictionary<string, string>> ldct = new List<Dictionary<string, string>>();



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
                    ldct.Add(dct);
                }


            }
            // Console.WriteLine("No.of dcts present are : " + dct.Count);
            // Console.WriteLine("No.of lists present are : " + ldct.Count);

            //DataTable dtable = new DataTable();

            try
            {
                foreach (string item in ldct[0].Keys)
                {
                    Console.Write(item + "   ");

                }
                Console.WriteLine();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //foreach (var item in collection)
            //{

            //}


            foreach (var text in ldct)
            {
                foreach (var dctitem in text)
                {
                    Console.Write(dctitem.Value + "      ");
                }
                Console.WriteLine();
            }


            //Element.Clear();

        }
    }
}
