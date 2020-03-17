using System;

namespace AreaCalc
{
    /// <summary>
    /// Triangle figure
    /// </summary>
    public class Triangle : IFigure
    {
        /// <summary>
        /// Triangle point A
        /// </summary>
        public Point2D A
        {
            get; set;
        }

        /// <summary>
        /// Triangle point B
        /// </summary>
        public Point2D B
        {
            get; set;
        }

        /// <summary>
        /// Triangle point C
        /// </summary>
        public Point2D C
        {
            get; set;
        }

        /// <summary>
        /// Intialize Triangle
        /// </summary>
        public Triangle()
        { }

        /// <summary>
        /// Intialize Triangle
        /// </summary>
        public Triangle(Point2D a, Point2D b, Point2D c) : this()
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// Get triangle area
        /// </summary>
        /// <returns>Returns figure area</returns>
        public double GetArea()
        {
            return 0.5 * Math.Abs((B.X - A.X) * (C.Y - A.Y) - (C.X - A.X) * (B.Y - A.Y));
        }
    }
}