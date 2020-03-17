
namespace AreaCalc
{
	using System;

	///<summary>
	///	Line in the given coordinate system
	///	and related calculations
	///	Line Equation:ax+by+c=0
	///</summary>
	public class Line
	{
		//line: ax+by+c=0;
		protected double a;
		protected double b;
		protected double c;

		private void Initialize(Double angleInRad, Point2D point)
		{
			//angleInRad should be between 0-Pi

			try
			{
				//if ((angleInRad<0) ||(angleInRad>Math.PI))
				if (angleInRad > 2 * Math.PI)
				{
					string errMsg = string.Format(
						"The input line angle" +
						" {0} is wrong. It should be between 0-2*PI.", angleInRad);

					InvalidInputGeometryDataException ex = new
						InvalidInputGeometryDataException(errMsg);

					throw ex;
				}

				if (Math.Abs(angleInRad - Math.PI / 2) <
					ConstantValue.SmallValue) //vertical line
				{
					a = 1;
					b = 0;
					c = -point.X;
				}
				else //not vertical line
				{
					a = -Math.Tan(angleInRad);
					b = 1;
					c = -a * point.X - b * point.Y;
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Trace.WriteLine(e.Message + e.StackTrace);
			}
		}


		public Line(Double angleInRad, Point2D point)
		{
			Initialize(angleInRad, point);
		}

		public Line(Point2D point1, Point2D point2)
		{
			try
			{
				if (Point2D.SamePoints(point1, point2))
				{
					string errMsg = "The input points are the same";
					InvalidInputGeometryDataException ex = new
						InvalidInputGeometryDataException(errMsg);
					throw ex;
				}

				//Point1 and Point2 are different points:
				if (Math.Abs(point1.X - point2.X)
					< ConstantValue.SmallValue) //vertical line
				{
					Initialize(Math.PI / 2, point1);
				}
				else if (Math.Abs(point1.Y - point2.Y)
					< ConstantValue.SmallValue) //Horizontal line
				{
					Initialize(0, point1);
				}
				else //normal line
				{
					double m = (point2.Y - point1.Y) / (point2.X - point1.X);
					double alphaInRad = Math.Atan(m);
					Initialize(alphaInRad, point1);
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Trace.WriteLine(e.Message + e.StackTrace);
			}
		}

		public Line(Line copiedLine)
		{
			this.a = copiedLine.a;
			this.b = copiedLine.b;
			this.c = copiedLine.c;
		}

		/*** calculate the distance from a given point to the line ***/
		public double GetDistance(Point2D point)
		{
			double x0 = point.X;
			double y0 = point.Y;

			double d = Math.Abs(a * x0 + b * y0 + c);
			d = d / (Math.Sqrt(a * a + b * b));

			return d;
		}

		/*** point(x, y) in the line, based on y, calculate x ***/
		public double GetX(double y)
		{
			//if the line is a horizontal line (a=0), it will return a NaN:
			double x;
			try
			{
				if (Math.Abs(a) < ConstantValue.SmallValue) //a=0;
				{
					throw new NonValidReturnException();
				}

				x = -(b * y + c) / a;
			}
			catch (Exception e)  //Horizontal line a=0;
			{
				x = System.Double.NaN;
				System.Diagnostics.Trace.
					WriteLine(e.Message + e.StackTrace);
			}

			return x;
		}

		/*** point(x, y) in the line, based on x, calculate y ***/
		public double GetY(double x)
		{
			//if the line is a vertical line, it will return a NaN:
			double y;
			try
			{
				if (Math.Abs(b) < ConstantValue.SmallValue)
				{
					throw new NonValidReturnException();
				}
				y = -(a * x + c) / b;
			}
			catch (Exception e)
			{
				y = System.Double.NaN;
				System.Diagnostics.Trace.
					WriteLine(e.Message + e.StackTrace);
			}
			return y;
		}

		/*** is it a vertical line:***/
		public bool VerticalLine()
		{
			if (Math.Abs(b - 0) < ConstantValue.SmallValue)
				return true;
			else
				return false;
		}

		/*** is it a horizontal line:***/
		public bool HorizontalLine()
		{
			if (Math.Abs(a - 0) < ConstantValue.SmallValue)
				return true;
			else
				return false;
		}

		/*** calculate line angle in radian: ***/
		public double GetLineAngle()
		{
			if (b == 0)
			{
				return Math.PI / 2;
			}
			else //b!=0
			{
				double tanA = -a / b;
				return Math.Atan(tanA);
			}
		}

		public bool Parallel(Line line)
		{
			bool bParallel = false;
			if (this.a / this.b == line.a / line.b)
				bParallel = true;

			return bParallel;
		}

		/**************************************
		 Calculate intersection point of two lines
		 if two lines are parallel, return null
		 * ************************************/
		public Point2D IntersecctionWith(Line line)
		{
			Point2D point = new Point2D();
			double a1 = this.a;
			double b1 = this.b;
			double c1 = this.c;

			double a2 = line.a;
			double b2 = line.b;
			double c2 = line.c;

			if (!(this.Parallel(line))) //not parallen
			{
				point.X = (c2 * b1 - c1 * b2) / (a1 * b2 - a2 * b1);
				point.Y = (a1 * c2 - c1 * a2) / (a2 * b2 - a1 * b2);
			}
			return point;
		}
	}
}