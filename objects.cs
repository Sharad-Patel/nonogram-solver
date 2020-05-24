using System.Collections.Generic;

namespace objects
{
    internal class Map
    {
        internal List<List<int>> columnGroups;
        internal List<List<int>> rowGroups;
        internal Grid grid;

        internal Map()
        {
            columnGroups = new List<List<int>>();
            rowGroups = new List<List<int>>();
        }
    }

    internal class Grid
    {
        internal int[,] grid;

        internal Grid(int size)
        {
            grid = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i,j] = 0;
                }
            }
        }
    }
}