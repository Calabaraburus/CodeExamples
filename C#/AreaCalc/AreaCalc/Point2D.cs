using System;

namespace AreaCalc
{
    /// <summary>
    /// 2D point
    /// </summary>
    public class Point2D
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public double X;

        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y;

        /// <summary>
        /// Initializes <see cref="Point2D"/>
        /// </summary>
        public Point2D() { }

        /// <summary>
        /// Initializes <see cref="Point2D"/>
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public static Point2D operator +(Point2D point1, Point2D point2)
        {
            return new Point2D(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static Point2D operator -(Point2D point1, Point2D point2)
        {
            return new Point2D(point1.X - point2.X, point1.Y - point2.Y);
        }
    }
}