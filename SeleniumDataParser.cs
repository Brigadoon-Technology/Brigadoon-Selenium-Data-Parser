using OpenQA.Selenium;
using OpenQA.Selenium.Edge;  // Or use ChromeDriver, FirefoxDriver, etc.
using OpenQA.Selenium.Support.UI;
using HtmlAgilityPack;  // Equivalent to BeautifulSoup in Python
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

/// <summary>
/// The main class for web automation and data extraction using Selenium and HtmlAgilityPack.
/// </summary>
class Program
{
    /// <summary>
    /// WebDriver instance to control the browser. Defaults to EdgeDriver. 
    /// Change to ChromeDriver or FirefoxDriver as needed.
    /// </summary>
    static IWebDriver driver = new EdgeDriver();  // Change to ChromeDriver, FirefoxDriver if needed

    /// <summary>
    /// Entry point of the program. Initializes navigation and teardown processes.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    static void Main(string[] args)
    {
        try
        {
            // Pass base URL and search term dynamically
            NavigateWeb("https://www.stackoverflow.com/directory", "Entity");
        }
        finally
        {
            TearDown();
        }
    }

    /// <summary>
    /// Navigates the website, performs a search, and grabs data from multiple pages.
    /// </summary>
    /// <param name="baseUrl">The base URL to navigate to.</param>
    /// <param name="searchTerm">The term to search for on the website.</param>
    static void NavigateWeb(string baseUrl, string searchTerm)
    {
        Console.WriteLine("Preparing to parse data...");
        driver.Navigate().GoToUrl(baseUrl);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        driver.Manage().Window.Maximize();

        while (true)
        {
            try
            {
                // Perform search operation
                Console.WriteLine("Searching for results...");
                driver.FindElement(By.Id("searchbox")).Clear();
                driver.FindElement(By.Id("searchbox")).SendKeys(searchTerm);
                driver.FindElement(By.Id("searchbutton")).Click();
                Console.WriteLine("Grabbing data from the first page...");
                GrabAllData();

                // Simulate navigating through pages (2 to 5)
                for (int i = 2; i <= 5; i++)
                {
                    string pageInput = $"{searchTerm}&page={i}&s={(i - 1) * 2000}";
                    driver.FindElement(By.Id("searchbox")).Clear();
                    driver.FindElement(By.Id("searchbox")).SendKeys(pageInput);
                    driver.FindElement(By.Id("searchbutton")).Click();
                    Console.WriteLine($"Navigating to page {i}...");
                    GrabAllData();
                }

                Thread.Sleep(3000);  // Delay between page requests
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("No such element found.");
            }
            catch (ElementNotVisibleException)
            {
                Console.WriteLine("Element not visible.");
            }
            catch (ElementNotSelectableException)
            {
                Console.WriteLine("Element not selectable.");
            }
            finally
            {
                break;
            }
        }
    }

    /// <summary>
    /// Extracts data from the web page and saves it into a CSV file.
    /// </summary>
    static void GrabAllData()
    {
        var pageSource = driver.PageSource;
        var doc = new HtmlDocument();
        doc.LoadHtml(pageSource);

        var data = new List<string>();
        var divs = doc.DocumentNode.SelectNodes("//section[@class='RecordInformation cf']");

        if (divs != null)
        {
            foreach (var div in divs)
            {
                var entityInfo = new List<string>();
                var dataHolders = div.SelectNodes(".//div[@class='DataHolder']");
                if (dataHolders != null)
                {
                    foreach (var tag in dataHolders)
                    {
                        entityInfo.Add(tag.InnerText.Trim());
                    }

                    data.Add(string.Join(",", entityInfo));
                }
            }

            // Append data to a CSV file
            File.AppendAllLines("output.csv", data);
        }
    }

    /// <summary>
    /// Closes the WebDriver instance and terminates the browser session.
    /// </summary>
    static void TearDown()
    {
        Console.WriteLine("Closing WebDriver...");
        driver.Quit();
    }
}
