using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;

namespace nonogram_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver();
            // Navigate to Nonograms website
            driver.Navigate().GoToUrl(new Uri("https://www.puzzle-nonograms.com/?size=1"));

            Thread.Sleep(2000);

            // Accept cookies
            var acceptButton = driver.FindElementByCssSelector("button.knPNpm");
            acceptButton.Click();
            var columnGroups = driver.FindElementsByCssSelector("#taskTop div.task-group");
            var rowGroups = driver.FindElementsByCssSelector("#taskLeft div.task-group");
            var rowGroupCount = rowGroups.Count;
            // div.task-cell
            int column = 1;
            foreach (var columnGroup in columnGroups)
            {
                var rawColumnGroupValues = columnGroup.FindElements(By.CssSelector("div.task-cell"));
                var columnGroupValues = rawColumnGroupValues.Where(x => x.Text != "").ToList();
                int sum = 0;
                List<int> onOffList = new List<int>();
                int group = 1;
                var groupCount = columnGroupValues.Count;
                foreach (var columnGroupValue in columnGroupValues)
                {
                    var text = columnGroupValue.Text;
                    var number = Convert.ToInt32(text);
                    sum = sum + Convert.ToInt32(number);
                    onOffList.AddRange(Enumerable.Repeat(1, number));
                    if (group != groupCount) onOffList.Add(-1);
                    group++;
                }
                var sumPlusSpaces = sum + columnGroupValues.Count - 1;
                if (rowGroupCount == sumPlusSpaces)
                {
                    var row = 1;
                    foreach (var onOff in onOffList)
                    {
                        Console.Write(onOff);
                        if (onOff == 1)
                        {
                            driver.FindElementByCssSelector($".row:nth-child({row}) .cell:nth-child({column})").Click();
                        }
                        else if (onOff == -1)
                        {
                            var cell = driver.FindElementByCssSelector($".row:nth-child({row}) .cell:nth-child({column})");
                            cell.Click();
                            cell.Click();
                        }
                        row++;
                    }
                }
                column++;
            }
            foreach (var rowGroup in rowGroups)
            {
                var rawRowGroupValues = rowGroup.FindElements(By.CssSelector("div.task-cell"));
                var rowGroupValues = rawRowGroupValues.Where(x => x.Text != "").ToList();
                int sum = 0;
                foreach (var rowGroupValue in rowGroupValues)
                {
                    var text = rowGroupValue.Text;
                    Console.Write(text);
                    sum = sum + Convert.ToInt32(text);
                }
                var sumPlusSpaces = sum + rowGroupValues.Count - 1;

            }
            driver.Close();
            driver?.Quit();
        }
    }
}
