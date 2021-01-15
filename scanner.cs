using System;
using System.Linq;
using objects;
using OpenQA.Selenium;

namespace scanner
{
    internal static class Scanner
    {
        internal static Map Scan(IWebDriver driver)
        {
            Console.WriteLine("SCAN: Starting...");
            var map = new Map();

            var columnGroups = driver.FindElements(By.CssSelector("#taskTop div.task-group"));
            var rowGroups = driver.FindElements(By.CssSelector("#taskLeft div.task-group"));
            var rowGroupCount = rowGroups.Count;
            var size = rowGroupCount;
            map.grid = new Grid(rowGroupCount);

            Console.WriteLine("SCAN: Scanning column groups...");
            foreach (var columnGroup in columnGroups)
            {
                var rawColumnGroupValues = columnGroup.FindElements(By.CssSelector("div.task-cell"));
                var columnGroupValues = rawColumnGroupValues
                    .Where(x => x.Text != "")
                    .Select(x => x.Text)
                    .Select(x => Convert.ToInt32(x))
                    .ToList();
                map.columnGroups.Add(columnGroupValues);
            }
            Console.WriteLine("SCAN: Scanning row groups...");
            foreach (var rowGroup in rowGroups)
            {
                var rawRowGroupValues = rowGroup.FindElements(By.CssSelector("div.task-cell"));
                var rowGroupValues = rawRowGroupValues
                    .Where(x => x.Text != "")
                    .Select(x => x.Text)
                    .Select(x => Convert.ToInt32(x))
                    .ToList();
                map.rowGroups.Add(rowGroupValues);
            }
            // Console.WriteLine("SCAN: Scanning grid...");
            // for (int i = 0; i < size; i++)
            // {
            //     for (int j = 0; j < size; j++)
            //     {
            //         var cell = driver.FindElement(By.CssSelector($".row:nth-child({i+1}) .cell:nth-child({j+1})"));
            //         var selected = cell.Selected;
            //         var classes = cell.GetAttribute("class");
            //         int value = 0;
            //         if (classes.Contains("cell-on"))
            //         {
            //             value = 1;
            //         }
            //         else if (classes.Contains("cell-x"))
            //         {
            //             value = -1;
            //         }
            //         else if (classes.Contains("cell-off"))
            //         {
            //             value = 0;
            //         }
            //         else
            //         {
            //             throw new Exception("Invalid cell");
            //         }
            //         map.grid.grid[i,j] = value;
            //     }
            // }
            Console.WriteLine("SCAN: Finished");
            return map;
        }
    }
}