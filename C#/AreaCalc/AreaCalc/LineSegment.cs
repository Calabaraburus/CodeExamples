using System;

namespace AreaCalc
{
    /// <summary>
    /// Line segment
    /// </summary>
    public class LineSegment : Line
    {
        //line: ax+by+c=0, with start point and end point
        //direction from start point ->end point
        private Point2D m_startPoint;
        private Point2D m_endPoint;

        /// <summary>
        /// Start point
        /// </summary>
        public Point2D StartPoint
        {
            get
            {
                return m_startPoint;
            }
        }

        /// <summary>
        /// End point
        /// </summary>
        public Point2D EndPoint
        {
            get
            {
                return m_endPoint;
            }
        }

        /// <summary>
        /// Initialize <see cref="LineSegment"/>
        /// </summary>
        /// <param name="startPoint">Start point</param>
        /// <param name="endPoint">End point</param>
        public LineSegment(Point2D startPoint, Point2D endPoint)
            : base(startPoint, endPoint)
        {
            this.m_startPoint = startPoint;
            this.m_endPoint = endPoint;
        }

        /// <summary>
        /// Change the line's direction 
        /// </summary>
        public void ChangeLineDirection()
        {
            Point2D tempPt;
            tempPt = this.m_startPoint;
            this.m_startPoint = this.m_endPoint;
            this.m_endPoint = tempPt;
        }

        /// <summary>
        /// To calculate the line segment length
        /// </summary>
        /// <returns></returns>
        public double GetLineSegmentLength()
        {
            double d = (m_endPoint.X - m_startPoint.X) * (m_endPoint.X - m_startPoint.X);
            d += (m_endPoint.Y - m_startPoint.Y) * (m_endPoint.Y - m_startPoint.Y);
            d = Math.Sqrt(d);

            return d;
        }

        /// <summary>
        /// Get point location, using windows coordinate system: 
        /// y-axes points down.
        /// </summary>
        /// <param name="point">Point</param>
        /// <returns>
        /// -1:point at the left of the line(or above the line if the line is horizontal)
        /// 0: point in the line segment or in the line segment 's extension
        /// 1: point at right of the line(or below the line if the line is horizontal)
        /// </returns>
        public int GetPointLocation(Point2D point)
        {
            double Ax, Ay, Bx, By, Cx, Cy;
            Bx = m_endPoint.X;
            By = m_endPoint.Y;

            Ax = m_startPoint.X;
            Ay = m_startPoint.Y;

            Cx = point.X;
            Cy = point.Y;

            if (this.HorizontalLine())
            {
                if (Math.Abs(Ay - Cy) < ConstantValue.SmallValue) //equal
                    return 0;
                else if (Ay > Cy)
                    return -1;   //Y Axis points down, point is above the line
                else //Ay<Cy
                    return 1;    //Y Axis points down, point is below the line
            }
            else //Not a horizontal line
            {
                //make the line direction bottom->up
                if (m_endPoint.Y > m_startPoint.Y)
                    this.ChangeLineDirection();

                double L = this.GetLineSegmentLength();
                double s = ((Ay - Cy) * (Bx - Ax) - (Ax - Cx) * (By - Ay)) / (L * L);

                //Note: the Y axis is pointing down:
                if (Math.Abs(s - 0) < ConstantValue.SmallValue) //s=0
                    return 0; //point is in the line or line extension
                else if (s > 0)
                    return -1; //point is left of line or above the horizontal line
                else //s<0
                    return 1;
            }
        }

        /// <summary>
        /// Get the minimum x value of the points in the line
        /// </summary>
        /// <returns>Returns line X minimum</returns>
        public double GetXmin()
        {
            return Math.Min(m_startPoint.X, m_endPoint.X);
        }

        /// <summary>
        /// Get the maximum  x value of the points in the line
        /// </summary>
        /// <returns>Returns line X max</returns>
        public double GetXmax()
        {
            return Math.Max(m_startPoint.X, m_endPoint.X);
        }

        /// <summary>
        /// Get the minimum y value of the points in the line
        /// </summary>
        /// <returns>Returns line Y minimum</returns>
        public double GetYmin()
        {
            return Math.Min(m_startPoint.Y, m_endPoint.Y);
        }

        /// <summary>
        /// Get the maximum y value of the points in the line
        /// </summary>
        /// <returns>Returns line Y maximum</returns>
        public double GetYmax()
        {
            return Math.Max(m_startPoint.Y, m_endPoint.Y);
        }

        /// <summary>
        /// Check whether this line is in a longer line
        /// </summary>
        /// <param name="longerLineSegment">Other line segment</param>
        /// <returns>Returns true if line is in longer line othyrwise false</returns>
        public bool InLine(LineSegment longerLineSegment)
        {
            bool bInLine = false;
            if ((m_startPoint.InLine(longerLineSegment)) &&
                (m_endPoint.InLine(longerLineSegment)))
                bInLine = true;
            return bInLine;
        }

        /// <summary>
        /// Offset the line segment to generate a new line segment
        /// If the offset direction is along the x-axis or y-axis, 
        /// Parameter is true, other wise it is false
        /// </summary>
        /// <param name="distance">Distance</param>
        /// <param name="rightOrDown">Right or down</param>
        /// <returns>Returns line with offset</returns>
        public LineSegment OffsetLine(double distance, bool rightOrDown)
        {
            LineSegment line;
            Point2D newStartPoint = new Point2D();
            Point2D newEndPoint = new Point2D();

            double alphaInRad = this.GetLineAngle(); // 0-PI
            if (rightOrDown)
            {
                if (this.HorizontalLine()) //offset to y+ direction
                {
                    newStartPoint.X = this.m_startPoint.X;
                    newStartPoint.Y = this.m_startPoint.Y + distance;

                    newEndPoint.X = this.m_endPoint.X;
                    newEndPoint.Y = this.m_endPoint.Y + distance;
                    line = new LineSegment(newStartPoint, newEndPoint);
                }
                else //offset to x+ direction
                {
                    if (Math.Sin(alphaInRad) > 0)
                    {
                        newStartPoint.X = m_startPoint.X + Math.Abs(distance * Math.Sin(alphaInRad));
                        newStartPoint.Y = m_startPoint.Y - Math.Abs(distance * Math.Cos(alphaInRad));

                        newEndPoint.X = m_endPoint.X + Math.Abs(distance * Math.Sin(alphaInRad));
                        newEndPoint.Y = m_endPoint.Y - Math.Abs(distance * Math.Cos(alphaInRad));

                        line = new LineSegment(
                                       newStartPoint, newEndPoint);
                    }
                    else //sin(FalphaInRad)<0
                    {
                        newStartPoint.X = m_startPoint.X + Math.Abs(distance * Math.Sin(alphaInRad));
                        newStartPoint.Y = m_startPoint.Y + Math.Abs(distance * Math.Cos(alphaInRad));
                        newEndPoint.X = m_endPoint.X + Math.Abs(distance * Math.Sin(alphaInRad));
                        newEndPoint.Y = m_endPoint.Y + Math.Abs(distance * Math.Cos(alphaInRad));

                        line = new LineSegment(
                            newStartPoint, newEndPoint);
                    }
                }
            }//{rightOrDown}
            else //leftOrUp
            {
                if (this.HorizontalLine()) //offset to y directin
                {
                    newStartPoint.X = m_startPoint.X;
                    newStartPoint.Y = m_startPoint.Y - distance;

                    newEndPoint.X = m_endPoint.X;
                    newEndPoint.Y = m_endPoint.Y - distance;
                    line = new LineSegment(
                        newStartPoint, newEndPoint);
                }
                else //offset to x directin
                {
                    if (Math.Sin(alphaInRad) >= 0)
                    {
                        newStartPoint.X = m_startPoint.X - Math.Abs(distance * Math.Sin(alphaInRad));
                        newStartPoint.Y = m_startPoint.Y + Math.Abs(distance * Math.Cos(alphaInRad));
                        newEndPoint.X = m_endPoint.X - Math.Abs(distance * Math.Sin(alphaInRad));
                        newEndPoint.Y = m_endPoint.Y + Math.Abs(distance * Math.Cos(alphaInRad));

                        line = new LineSegment(
                            newStartPoint, newEndPoint);
                    }
                    else //sin(FalphaInRad)<0
                    {
                        newStartPoint.X = m_startPoint.X - Math.Abs(distance * Math.Sin(alphaInRad));
                        newStartPoint.Y = m_startPoint.Y - Math.Abs(distance * Math.Cos(alphaInRad));
                        newEndPoint.X = m_endPoint.X - Math.Abs(distance * Math.Sin(alphaInRad));
                        newEndPoint.Y = m_endPoint.Y - Math.Abs(distance * Math.Cos(alphaInRad));

                        line = new LineSegment(
                            newStartPoint, newEndPoint);
                    }
                }
            }
            return line;
        }

        /// <summary>
        /// Check whether 2 lines segments have an intersection
        /// </summary>
        /// <param name="line">Line to check</param>
        /// <returns>Returns true if lines have intersection otherwise - false</returns>
        public bool IntersectedWith(LineSegment line)
        {
            double x1 = this.m_startPoint.X;
            double y1 = this.m_startPoint.Y;
            double x2 = this.m_endPoint.X;
            double y2 = this.m_endPoint.Y;
            double x3 = line.m_startPoint.X;
            double y3 = line.m_startPoint.Y;
            double x4 = line.m_endPoint.X;
            double y4 = line.m_endPoint.Y;

            double de = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
            //if de<>0 then //lines are not parallel
            if (Math.Abs(de - 0) < ConstantValue.SmallValue) //not parallel
            {
                double ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / de;
                double ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / de;

                if ((ub > 0) && (ub < 1))
                    return true;
                else
                    return false;
            }
            else    //lines are parallel
                return false;
        }

    }
}