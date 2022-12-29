namespace DSA
{
    /// <summary>
    /// Represents a weighted grid graph node
    /// </summary>
    public class WeightedNode
    {
        public float Weight { get; set; }

        internal float Distance { get; set; }

        internal float Heuristic { get; set; }

        internal float Cost { get { return Distance + Heuristic; } }

        internal WeightedNode? Last;

        internal GridLocation GridLocation = new GridLocation(0, 0);

        public WeightedNode(float weight)
        {
            Weight = weight;
        }

        public override string ToString()
        {
            return $"{Weight}";
        }
    }

    public static class GridTraversalExtensions
    {
        /// <summary>
        /// Uses the A* pathfinding algorithm to find the shortest path between two points on a weighted grid
        /// </summary>
        /// <param name="grid">A grid of weighted nodes</param>
        /// <param name="startCol">The column value of the cell to start at</param>
        /// <param name="startRow">The row value of the cell to start at</param>
        /// <param name="endCol">The column value of the cell to end at</param>
        /// <param name="endRow">The row value of the cell to end at</param>
        /// <returns></returns>
        public static IEnumerable<GridLocation> AStar(this Grid<WeightedNode> grid, int startCol, int startRow, int endCol, int endRow)
        {
            HashSet<GridLocation> visited = new HashSet<GridLocation>();
            HashSet<GridLocation> unvisited = new HashSet<GridLocation>();

            GridLocation current = new GridLocation(startCol, startRow);
            GridLocation target = new GridLocation(endCol, endRow);

            // Initialize the grid
            for (int x = 0; x < grid.Size; x++)
            {
                for (int y = 0; y < grid.Size; y++)
                {
                    WeightedNode node = grid.Get(x, y);
                    node.GridLocation = new GridLocation(x, y);

                    if (x == startCol && y == startRow)
                    {
                        grid.Get(x, y).Distance = 0;
                        visited.Add(current);
                        continue;
                    }

                    node.Distance = float.PositiveInfinity;
                    node.Heuristic = Distance(x, y, endCol, endRow);
                    unvisited.Add(new GridLocation(x, y));
                }
            }

            bool hasFound = false;

            while (!hasFound)
            {
                unvisited.Remove(current);

                WeightedNode node = grid.Get(current.Col, current.Row);

                // Update all neighbors
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        int px = current.Col + x;
                        int py = current.Row + y;

                        // Avoid index out of bounds errors
                        if (px < 0 || px >= grid.Size)
                        {
                            continue;
                        }
                        if (py < 0 || py >= grid.Size)
                        {
                            continue;
                        }

                        WeightedNode neighbor = grid.Get(px, py);

                        float newDist = node.Distance + neighbor.Weight;
                        if (newDist < neighbor.Distance)
                        {
                            neighbor.Last = node;
                            neighbor.Distance = newDist;
                        }
                    }
                }

                // Find the cell with the lowest total cost
                current = FindMin(unvisited.ToList(), grid);

                if (current == target)
                {
                    hasFound = true;
                }
            }


            Stack<GridLocation> result = new Stack<GridLocation>();

            // A route has been found! Traverse and return it
            if (hasFound)
            {
                WeightedNode? node = grid.Get(current.Col, current.Row);
                while (node != null)
                {
                    current = node.GridLocation;
                    result.Push(current);
                    node = node.Last;
                }
            }

            return result;
        }

        /// <summary>
        /// A Utility method for easily creating grids for testing.
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static Grid<WeightedNode> GridFromString(string contents)
        {
            int size = contents.Split("\n").Length;
            Grid<WeightedNode> grid = new Grid<WeightedNode>(size);

            int x = 0;
            int y = 0;

            foreach (char c in contents)
            {
                switch (c)
                {
                    case '\n':
                        y++;
                        x = 0;
                        break;
                    case '1':
                        grid.Set(x, y, new WeightedNode(1));
                        x++;
                        break;
                    default:
                        grid.Set(x, y, new WeightedNode(float.PositiveInfinity));
                        x++;
                        break;
                }
            }

            return grid;
        }

        private static GridLocation FindMin(List<GridLocation> cells, Grid<WeightedNode> grid)
        {
            float minF = float.PositiveInfinity;
            GridLocation? minLoc = null;

            foreach (GridLocation cell in cells)
            {
                WeightedNode node = grid.Get(cell.Col, cell.Row);

                if (minLoc is null || node.Cost < minF)
                {
                    minF = node.Cost;
                    minLoc = cell;
                }
            }

            if (minLoc is null)
            {
                throw new ArgumentException();
            }

            return minLoc;
        }

        private static float Distance(int x1, int y1, int x2, int y2)
        {
            return MathF.Sqrt(MathF.Pow(x1 - x2, 2) + MathF.Pow(y1 - y2, 2));
        }
    }
}
