using DSA;
using System.Diagnostics;

namespace DSATests
{
    [TestClass]
    public class AStarTests
    {
        [TestMethod]
        public void StraightLineTest ()
        {
            Grid<WeightedNode> grid = GridTraversalExtensions.GridFromString(
                "000100\n" +
                "000100\n" +
                "001011\n" +
                "000111\n" +
                "001000\n" +
                "000010\n"
            );

            

            Debug.WriteLine(grid);

            GridLocation[] expected = new GridLocation[] { 
            new GridLocation(5,5),
            new GridLocation(4,4),
            new GridLocation(3,4),
            new GridLocation(2,3),
            new GridLocation(3,2),
            new GridLocation(4,1),
            new GridLocation(4,0),

            };
            List<GridLocation> path = new List<GridLocation>(grid.AStar(5, 5, 4, 0));

            CollectionAssert.AreEqual(path, expected);
        }
    }
}