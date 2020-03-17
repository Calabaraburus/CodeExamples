using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AreaCalc.Tests
{
    [TestClass]
    public class FigureTests
    {
        [TestMethod]
        public void GetTriangleAreaTest()
        {
            var triangle = CreateTriangle();

            var area = triangle.GetArea();

            Assert.AreEqual(area, 25d);
        }

        [TestMethod]
        public void ComplexTriangleAreaTest()
        {
            var p1 = new Point2D(-1, -3);
            var p2 = new Point2D(3, 4);
            var p3 = new Point2D(5, -5);

            var triangle = new Triangle(p1, p2, p3);
            var ctriangle = new TriangleComplex(p1, p2, p3);

            var area1 = triangle.GetArea();
            var area2 = ctriangle.GetArea();

            Assert.AreEqual(area1, area2);
        }

        [TestMethod]
        public void FigureListTest()
        {
            List<IFigure> list = new List<IFigure>();

            list.Add(CreateTriangle());
            list.Add(CreateCircle());

            var area1 = list[0].GetArea();
            var area2 = list[1].GetArea();

            Assert.AreEqual(area1, 25d);
            Assert.AreEqual(area2, Math.PI);
        }

        private IFigure CreateTriangle()
        {
            var p1 = new Point2D(-1, -3);
            var p2 = new Point2D(3, 4);
            var p3 = new Point2D(5, -5);

            return new Triangle(p1, p2, p3);
        }

        private IFigure CreateCircle()
        {
            return new Circle(new Point2D(1,5), 1);
        }
    }
}
