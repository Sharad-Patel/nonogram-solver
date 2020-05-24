using System;
using objects;
using OpenQA.Selenium;

namespace drawer
{
    internal static class Drawer
    {
        internal static void Draw(IWebDriver driver, Grid grid)
        {
            Console.WriteLine("DRAW: Starting...");

            for (int i = 0; i < grid.grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.grid.GetLength(1); j++)
                {
                    var cell = driver.FindElement(By.CssSelector($".row:nth-child({i+1}) .cell:nth-child({j+1})"));
                    if (grid.grid[i,j] == 1)
                    {
                        cell.Click();
                    }
                    else if (grid.grid[i,j] == -1)
                    {
                        cell.Click();
                        cell.Click();
                    }
                }
            }
            Console.WriteLine("DRAW: Finished");
        }
    }
}