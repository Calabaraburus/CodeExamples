using System;

namespace AreaCalc
{
    /// <summary>
    /// Triangle figure
    /// </summary>
    public class TriangleComplex : ComplexFigure
    {
        /// <summary>
        /// Vertex count
        /// </summary>
        public const int VertexCount = 3;

        /// <summary>
        /// Triangle point A
        /// </summary>
        public Point2D A
        {
            get { return PointList[0]; }
            set { PointList[0] = value; }
        }

        /// <summary>
        /// Triangle point B
        /// </summary>
        public Point2D B
        {
            get { return PointList[1]; }
            set { PointList[1] = value; }
        }

        /// <summary>
        /// Triangle point C
        /// </summary>
        public Point2D C
        {
            get { return PointList[2]; }
            set { PointList[2] = value; }
        }

        /// <summary>
        /// Intialize Triangle
        /// </summary>
        public TriangleComplex()
        {
            PointList.AddRange(new Point2D[VertexCount]);
        }

        /// <summary>
        /// Intialize Triangle
        /// </summary>
        public TriangleComplex(Point2D a, Point2D b, Point2D c) : this()
        {
            A = a;
            B = b;
            C = c;
        }
    }
}