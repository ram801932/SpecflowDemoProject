using DeltaHRMS.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace com.DeltaHRMS
{
    class Leaves2 : BaseClass
    {
        [Test, Order(0), Category("Leaves")]
        public void GotoEmployeeLeaveTab()
        {
            test = extent.CreateTest(MethodBase.GetCurrentMethod().Name);

            //Go to Self-service page
            driver.FindElement(By.XPath("//div[@class='side-menu-thumbnail']//li[text()='Self Service']")).Click();
            test.Info("Logged on to HRMS and navigated to Self-service");

            //Go to Leaves
            driver.FindElement(By.XPath("//span[@id='acc_li_toggle_31']/b[text()='Leaves']")).Click();
            test.Info("Clicked on Leaves");

            //Then select "Employee Leave" tab
            driver.FindElement(By.LinkText("Employee Leave")).Click();
            test.Info("selected \"Employee leave\" tab ");
        }
    }
}
