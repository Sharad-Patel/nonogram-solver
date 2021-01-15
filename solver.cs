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
            map = ComlpeteFullMapping(map);
            map = FillOverlaps(map);
            map = ExtendEdges(map);
            Console.WriteLine("SOLVE: Finished");
            return map.grid;
        }

        internal static Map ComlpeteFullMapping(Map map)
        {
            Console.WriteLine("SOLVE: Comlpeting full maps...");
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
            Console.WriteLine("SOLVE: Finsihed comlpeting full maps: " + ts.TotalMilliseconds + "ms");
            return map;
        }

        internal static Map FillOverlaps(Map map)
        {
            Console.WriteLine("SOLVE: Filling overlaps...");
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
            Console.WriteLine("SOLVE: Finsihed filling overlaps: " + ts.TotalMilliseconds + "ms");
            return map;
        }

        internal static Map ExtendEdges(Map map)
        {
            Console.WriteLine("SOLVE: Extending edges...");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var size = map.columnGroups.Count;

            int column = 0;
            foreach (var columnGroup in map.columnGroups)
            {
                if (map.grid.grid[0, column] == 1)
                {
                    for (int i = 0; i < columnGroup.First(); i++)
                    {
                        map.grid.grid[i, column] = 1;
                    }
                }
                column++;
            }

            int row = 0;
            foreach (var rowGroup in map.rowGroups)
            {
                if (map.grid.grid[row, 0] == 1)
                {
                    for (int j = 0; j < rowGroup.First(); j++)
                    {
                        map.grid.grid[row, j] = 1;
                    }
                }
                row++;
            }

            column = 0;
            foreach (var columnGroup in map.columnGroups)
            {
                if (map.grid.grid[size - 1, column] == 1)
                {
                    for (int i = size - 1; i > size - 1 - columnGroup.Last(); i--)
                    {
                        map.grid.grid[i, column] = 1;
                    }
                }
                column++;
            }

            row = 0;
            foreach (var rowGroup in map.rowGroups)
            {
                if (map.grid.grid[row, size - 1] == 1)
                {
                    for (int j = size - 1; j < size - 1 - rowGroup.Last(); j--)
                    {
                        map.grid.grid[row, j] = 1;
                    }
                }
                row++;
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("SOLVE: Finsihed extending edges: " + ts.TotalMilliseconds + "ms");
            return map;
        }
    }
}