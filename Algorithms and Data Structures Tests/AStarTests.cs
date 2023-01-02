using DSA;
using System.Diagnostics;

namespace DSATests
{
    [TestClass]
    public class AStarTests
    {
        [TestMethod]
        public void ComplexPathTest()
        {
            Grid<WeightedNode> grid = GridTraversal.GridFromString(6,
                "000100\n" +
                "000100\n" +
                "001011\n" +
                "000111\n" +
                "001000\n" +
                "000010\n"
            );


            GridLocation[] expected = new GridLocation[] {
            new GridLocation(5,5),
            new GridLocation(4,4),
            new GridLocation(3,4),
            new GridLocation(2,3),
            new GridLocation(3,2),
            new GridLocation(4,1),
            new GridLocation(4,0),

            };
            List<GridLocation> path = new List<GridLocation>(GridTraversal.AStar(grid, 5, 5, 4, 0));

            CollectionAssert.AreEqual(path, expected);
        }

        [TestMethod]
        public void StraightLineTest ()
        {
            Grid<WeightedNode> grid = GridTraversal.GridFromString(6,
                "000100\n" +
                "000100\n" +
                "000100\n" +
                "000100\n" +
                "000100\n" +
                "000100\n"
            );


            GridLocation[] expected = new GridLocation[] {
            new GridLocation(0,2),
            new GridLocation(1,2),
            new GridLocation(2,2),
            new GridLocation(3,2),
            new GridLocation(4,2),
            new GridLocation(5,2),

            };
            List<GridLocation> path = new List<GridLocation>(GridTraversal.AStar(grid, 0, 2, 5, 2));

            CollectionAssert.AreEqual(path, expected);
        }

        [TestMethod]
        public void ImpossiblePathTest()
        {
            Grid<WeightedNode> grid = GridTraversal.GridFromString(6,
                "000100\n" +
                "000100\n" +
                "000100\n" +
                "000100\n" +
                "000100\n" +
                "000100\n"
            );

            List<GridLocation> path = new List<GridLocation>(GridTraversal.AStar(grid, 0, 2, 5, 2));

            Assert.AreEqual(0, path.Count);
        }

        [TestMethod]
        public void SameTargetTest()
        {
            Grid<WeightedNode> grid = GridTraversal.GridFromString(6,
                "000100\n" +
                "000100\n" +
                "000100\n" +
                "000100\n" +
                "000100\n" +
                "000100\n"
            );


            GridLocation[] expected = new GridLocation[] {
                new GridLocation(0,2),
            };
            List<GridLocation> path = new List<GridLocation>(GridTraversal.AStar(grid, 0, 2, 0, 2));

            CollectionAssert.AreEqual(path, expected);
        }
    }
}