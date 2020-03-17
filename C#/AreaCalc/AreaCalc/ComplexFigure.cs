using System;
using System.Collections.Generic;

namespace AreaCalc
{
    /// <summary>
    /// Complex figure
    /// </summary>
    public class ComplexFigure : IFigure
    {
        /// <summary>
        /// Point list
        /// </summary>
        public List<Point2D> PointList { get; private set; } = new List<Point2D>();

        /// <summary>
        /// Poligon Area
        /// </summary>
        /// <returns>Return figure area</returns>
        public virtual double GetArea()
        {
            double dblArea = 0;
            int nNumOfVertices = PointList.Count;

            int j;
            for (int i = 0; i < nNumOfVertices; i++)
            {
                j = (i + 1) % nNumOfVertices;
                dblArea += PointList[i].X * PointList[j].Y;
                dblArea -= (PointList[i].Y * PointList[j].X);
            }

            dblArea = dblArea / 2;
            return Math.Abs(dblArea);
        }
    }
}
