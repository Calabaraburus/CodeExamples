using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AreaCalc.Tests
{
    [TestClass]
    public class Point2DTests
    {
        [TestMethod]
        public void GetLengthTest()
        {
            var p1 = new Point2D(1, 5);
            var p2 = new Point2D(4, 9);
            var len = (p1 - p2).GetLength();

            Assert.AreEqual(len, 5);
        }
    }
}
