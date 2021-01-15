using System;
using System.Threading;
using drawer;
using OpenQA.Selenium.Chrome;
using scanner;
using solver;

namespace nonogram_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver();
            // Navigate to Nonograms website
            //?size=1
            driver.Navigate().GoToUrl(new Uri("https://www.puzzle-nonograms.com/?size=2"));

            Thread.Sleep(2000);

            // Accept cookies
            var acceptButton = driver.FindElementByCssSelector("button.knPNpm");
            acceptButton.Click();

            var map = Scanner.Scan(driver);
            var grid = Solver.Solve(map);
            Drawer.Draw(driver, grid);

            driver.Close();
            driver?.Quit();
        }
    }
}
