using System;

namespace AreaCalc
{
    /// <summary>
    /// Circle figure
    /// </summary>
    public class Circle : IFigure
    {
        /// <summary>
        /// Circle center point
        /// </summary>
        public Point2D Center
        {
            get; set;
        }

        /// <summary>
        /// Circle radius
        /// </summary>
        public double Radius
        {
            get;
            set;
        }

        /// <summary>
        /// Intialize Circle
        /// </summary>
        public Circle()
        { }

        /// <summary>
        /// Intialize Circle
        /// </summary>
        /// <param name="center">Center</param>
        /// <param name="r">Radius</param>
        public Circle(Point2D center, double r) : this()
        {
            Center = center;
            Radius = r;
        }

        /// <summary>
        /// Get Circle area
        /// </summary>
        /// <returns>Returns figure area</returns>
        public double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }
}