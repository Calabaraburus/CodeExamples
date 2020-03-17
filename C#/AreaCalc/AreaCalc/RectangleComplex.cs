using System;

namespace AreaCalc
{
    /// <summary>
    /// Rectangle figure
    /// </summary>
    public class RectangleComplex : ComplexFigure
    {
        /// <summary>
        /// Vertex count
        /// </summary>
        public const int VertexCount = 4;

        /// <summary>
        /// Rectangle point A
        /// </summary>
        public Point2D A
        {
            get { return PointList[0]; }
            set { PointList[0] = value; }
        }

        /// <summary>
        /// Rectangle point B
        /// </summary>
        public Point2D B
        {
            get { return PointList[1]; }
            set { PointList[1] = value; }
        }

        /// <summary>
        /// Rectangle point C
        /// </summary>
        public Point2D C
        {
            get { return PointList[2]; }
            set { PointList[2] = value; }
        }

        /// <summary>
        /// Rectangle point C
        /// </summary>
        public Point2D D
        {
            get { return PointList[3]; }
            set { PointList[3] = value; }
        }

        /// <summary>
        /// Intialize Rectangle
        /// </summary>
        public RectangleComplex()
        {
            PointList.AddRange(new Point2D[VertexCount]);
        }

        /// <summary>
        /// Intialize Rectangle
        /// </summary>
        public RectangleComplex(Point2D a, Point2D b, Point2D c) : this()
        {
            A = a;
            B = b;
            C = c;
        }
    }
}