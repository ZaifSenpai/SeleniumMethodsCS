// Created by @zaifsenpai

using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

/// <summary>
/// Name providing Methods class
/// </summary>
namespace SeleniumMethods
{
    /// <summary>
    /// Contains methods for common functionalities to be used in Selenium automation programs
    /// </summary>
    class Methods
    {
        /// <summary>
        /// Scrolls and moves mouse to element of given seletor.
        /// </summary>
        /// <param name="Driver">The IWebDriver being used.</param>
        /// <param name="by">OpenQA.Selenium.By Selector of element where to be moved.</param>
        public static void MoveToEl(IWebDriver Driver, By by)
        {
            MoveToEl(Driver, Driver.FindElement(by));
        }

        /// <summary>
        /// Scrolls and moves mouse to given element.
        /// </summary>
        /// <param name="Driver">The IWebDriver being used.</param>
        /// <param name="Element">IWebElement where to be moved.</param>
        public static void MoveToEl(IWebDriver Driver, IWebElement Element)
        {
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Element).Perform();
        }

        /// <summary>
        /// Performs a mouse LMB click on element of given seletor.
        /// </summary>
        /// <param name="Driver">The IWebDriver being used.</param>
        /// <param name="by">OpenQA.Selenium.By Selector of element where mouse should be clicked.</param>
        public static void ClickEl(IWebDriver Driver, By by)
        {
            ClickEl(Driver, Driver.FindElement(by));
        }

        /// <summary>
        /// Performs a mouse LMB click on given element.
        /// </summary>
        /// <param name="Driver">The IWebDriver being used.</param>
        /// <param name="Element">IWebElement where mouse should be clicked.</param>
        public static void ClickEl(IWebDriver Driver, IWebElement Element)
        {
            ClickEl(Driver, Element, 0);
        }

        private static void ClickEl(IWebDriver Driver, IWebElement Element, int attempt)
        {
            Actions actions = new Actions(Driver);

            try
            {
                actions.MoveToElement(Element).Click().Perform();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("input area") && attempt < 10)
                {
                    KeysToEl(Driver, Element, OpenQA.Selenium.Keys.ArrowDown);
                    ClickEl(Driver, Element, attempt);
                }
            }
        }

        /// <summary>
        /// Send keyboard keys to element of given selector.
        /// </summary>
        /// <param name="Driver">The IWebDriver being used.</param>
        /// <param name="by">OpenQA.Selenium.By Selector of element where keys should be pressed.</param>
        /// <param name="keys">The string of keys to be send to element.</param>
        public static void KeysToEl(IWebDriver Driver, By by, string keys)
        {
            KeysToEl(Driver, Driver.FindElement(by), keys);
        }

        /// <summary>
        /// Sends keyboard keys to given element.
        /// </summary>
        /// <param name="Driver">The IWebDriver being used.</param>
        /// <param name="Element">IWebElement where keys should be pressed.</param>
        /// <param name="keys">The string of keys to be send to element.</param>
        public static void KeysToEl(IWebDriver Driver, IWebElement Element, string keys)
        {
            if (string.IsNullOrEmpty(keys)) return;

            Actions actions = new Actions(Driver);

            actions.MoveToElement(Element).Perform();
            ClickEl(Driver, Element);
            Element.Clear();
            actions.SendKeys(keys).Perform();
        }

        /// <summary>
        /// Checks if element of given selector present on current page or not.
        /// </summary>
        /// <param name="Driver">The IWebDriver being used.</param>
        /// <param name="by">OpenQA.Selenium.By Selector of element to check for existence.</param>
        /// <returns>bool value indicating the status of element existence.</returns>
        public static bool ElementExist(IWebDriver Driver, By by)
        {
            try
            {
                Driver.FindElement(by);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Waits for given element to load
        /// </summary>
        /// <param name="Driver">The IWebDriver which being used.</param>
        /// <param name="by">OpenQA.Selenium.By Selector of element for which the wait is being done.</param>
        /// <param name="Timeout">Optional: Maximimum number of seconds to wait for this element.</param>
        public static void WaitForElement(IWebDriver Driver, By by, int Timeout = 15)
        {
            int counter = 0;
            while (!ElementExist(Driver, by))
            {
                Thread.Sleep(1000);
                ++counter;

                if (counter == Timeout)
                    throw new Exception("Unable to load.");
            }
        }

        /// <summary>
        /// Checks if browser window is running or not.
        /// </summary>
        /// <param name="Driver">The IWebDriver which being used.</param>
        /// <returns>true if browser window is open and running, false otherwise.</returns>
        public static bool BrowserIsOpen(IWebDriver Driver)
        {
            return Driver != null && Driver.WindowHandles != null && Driver.WindowHandles.Count > 0;
        }
    }
}
