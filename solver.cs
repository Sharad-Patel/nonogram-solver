using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using objects;

namespace solver
{
    internal static class Solver
    {
        internal static Grid Solve(Map map)
        {
            Console.WriteLine("SOLVE: Starting...");
            map = FirstPass(map);
            map = SecondPass(map);
            Console.WriteLine("SOLVE: Finished");
            return map.grid;
        }

        internal static Map FirstPass(Map map)
        {
            Console.WriteLine("SOLVE: First pass...");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var size = map.columnGroups.Count;
            int column = 0;
            foreach (var columnGroup in map.columnGroups)
            {
                int sum = 0;
                int valueIteration = 1;
                var valuesCount = columnGroup.Count();
                List<int> onOffList = new List<int>();
                foreach (var columnGroupValue in columnGroup)
                {
                    sum = sum + columnGroupValue;
                    onOffList.AddRange(Enumerable.Repeat(1, columnGroupValue));
                    if (valueIteration != valuesCount) onOffList.Add(-1);
                    valueIteration++;
                }
                var sumPlusSpaces = sum + valuesCount - 1;
                if (sumPlusSpaces == size)
                {
                    for (int i = 0; i < size; i++)
                    {
                        map.grid.grid[i, column] = onOffList[i];
                    }
                }
                column++;
            }
            int row = 0;
            foreach (var rowGroup in map.rowGroups)
            {
                int sum = 0;
                int valueIteration = 1;
                var valuesCount = rowGroup.Count();
                List<int> onOffList = new List<int>();
                foreach (var rowGroupValue in rowGroup)
                {
                    sum = sum + rowGroupValue;
                    onOffList.AddRange(Enumerable.Repeat(1, rowGroupValue));
                    if (valueIteration != valuesCount) onOffList.Add(-1);
                    valueIteration++;
                }
                var sumPlusSpaces = sum + valuesCount - 1;
                if (sumPlusSpaces == size)
                {
                    for (int j = 0; j < size; j++)
                    {
                        map.grid.grid[row, j] = onOffList[j];
                    }
                }
                row++;
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("SOLVE: Finsihed first pass: " + ts.TotalMilliseconds + "ms");
            return map;
        }

        internal static Map SecondPass(Map map)
        {
            Console.WriteLine("SOLVE: Second pass...");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var size = map.columnGroups.Count;
            var halfSize = Convert.ToDouble(size/2.0);

            int column = 0;
            foreach (var columnGroup in map.columnGroups)
            {
                var max = columnGroup.Max();
                if (max > halfSize)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (i > size - max - 1 && i < max)
                        {
                            map.grid.grid[i, column] = 1;
                        }
                    }
                }

                column++;
            }

            int row = 0;
            foreach (var rowGroup in map.rowGroups)
            {
                var max = rowGroup.Max();
                if (max > halfSize)
                {
                    for (int j = 0; j < size; j++)
                    {
  
                        if (j > size - max - 1 && j < max)
                        {
                            map.grid.grid[row, j] = 1;
                        }
                    }
                }

                row++;
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("SOLVE: Finsihed second pass: " + ts.TotalMilliseconds + "ms");
            return map;
        }
    }
}