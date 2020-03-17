//

using System;

namespace AreaCalc
{
    /// <summary>
    /// Polygon
    /// </summary>
    public class Polygon
    {
        private Point2D[] _vertices;

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="index">Point index</param>
        /// <returns>Get or set point with specified index</returns>
        public Point2D this[int index]
        {
            set
            {
                _vertices[index] = value;
            }
            get
            {
                return _vertices[index];
            }
        }

        /// <summary>
        /// Initialize <see cref="Polygon"/>
        /// </summary>
        public Polygon()
        {

        }

        /// <summary>
        /// Initialize <see cref="Polygon"/>
        /// </summary>
        /// <param name="points">Points</param>
        public Polygon(Point2D[] points)
        {
            int nNumOfPoitns = points.Length;
            try
            {
                if (nNumOfPoitns < 3)
                {
                    InvalidInputGeometryDataException ex =
                        new InvalidInputGeometryDataException();
                    throw ex;
                }
                else
                {
                    _vertices = new Point2D[nNumOfPoitns];
                    for (int i = 0; i < nNumOfPoitns; i++)
                    {
                        _vertices[i] = points[i];
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(
                    e.Message + e.StackTrace);
            }
        }

        /// <summary>
        /// From a given point, get its vertex index.
        /// </summary>
        /// <param name="vertex">Vertex</param>
        /// <returns>
        /// If the given point is not a polygon vertex,
        /// it will return -1
        /// </returns>
        public int VertexIndex(Point2D vertex)
        {
            int nIndex = -1;

            int nNumPts = _vertices.Length;
            for (int i = 0; i < nNumPts; i++) //each vertex
            {
                if (Point2D.SamePoints(_vertices[i], vertex))
                    nIndex = i;
            }
            return nIndex;
        }

        /// <summary>
        /// From a given vertex, get its previous vertex point.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns>
        /// If the given point is the first one, 
		/// it will return  the last vertex;
		/// If the given point is not a polygon vertex,
        /// it will return null; 
        /// </returns>
        public Point2D PreviousPoint(Point2D vertex)
        {
            int nIndex;

            nIndex = VertexIndex(vertex);
            if (nIndex == -1)
                return null;
            else //a valid vertex
            {
                if (nIndex == 0) //the first vertex
                {
                    int nPoints = _vertices.Length;
                    return _vertices[nPoints - 1];
                }
                else //not the first vertex
                    return _vertices[nIndex - 1];
            }
        }

        /// <summary>
        /// From a given vertex, get its next vertex point.
        /// If the given point is the last one, 
		///	it will return  the first vertex;
		///	If the given point is not a polygon vertex,
        /// it will return null
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public Point2D NextPoint(Point2D vertex)
        {
            Point2D nextPt = new Point2D();

            int nIndex;
            nIndex = VertexIndex(vertex);
            if (nIndex == -1)
                return null;
            else //a valid vertex
            {
                int nNumOfPt = _vertices.Length;
                if (nIndex == nNumOfPt - 1) //the last vertex
                {
                    return _vertices[0];
                }
                else //not the last vertex
                    return _vertices[nIndex + 1];
            }
        }


        /******************************************
		Calculate the polygon's area

		Good for polygon with holes, but the vertices make the 
		hole  should be in different direction with bounding 
		polygon.
		
		Restriction: the polygon is not self intersecting
		ref: www.swin.edu.au/astronomy/pbourke/geometry/polyarea/
		*******************************************/

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
        public double PolygonArea()
        {
            double dblArea = 0;
            int nNumOfVertices = _vertices.Length;

            int j;
            for (int i = 0; i < nNumOfVertices; i++)
            {
                j = (i + 1) % nNumOfVertices;
                dblArea += _vertices[i].X * _vertices[j].Y;
                dblArea -= (_vertices[i].Y * _vertices[j].X);
            }

            dblArea = dblArea / 2;
            return Math.Abs(dblArea);
        }

        /******************************************
		To calculate the area of polygon made by given points 

		Good for polygon with holes, but the vertices make the 
		hole  should be in different direction with bounding 
		polygon.
		
		Restriction: the polygon is not self intersecting
		ref: www.swin.edu.au/astronomy/pbourke/
			geometry/polyarea/

		As polygon in different direction, the result coulb be
		in different sign:
		If dblArea>0 : polygon in clock wise to the user 
		If dblArea<0: polygon in count clock wise to the user 		
		*******************************************/
        public static double PolygonArea(Point2D[] points)
        {
            double dblArea = 0;
            int nNumOfPts = points.Length;

            int j;
            for (int i = 0; i < nNumOfPts; i++)
            {
                j = (i + 1) % nNumOfPts;
                dblArea += points[i].X * points[j].Y;
                dblArea -= (points[i].Y * points[j].X);
            }

            dblArea = dblArea / 2;
            return dblArea;
        }

        /***********************************************
			To check a vertex concave point or a convex point
			-----------------------------------------------------------
			The out polygon is in count clock-wise direction
		************************************************/
        public VertexType PolygonVertexType(Point2D vertex)
        {
            VertexType vertexType = VertexType.ErrorPoint;

            if (PolygonVertex(vertex))
            {
                Point2D pti = vertex;
                Point2D ptj = PreviousPoint(vertex);
                Point2D ptk = NextPoint(vertex);

                double dArea = PolygonArea(new Point2D[] { ptj, pti, ptk });

                if (dArea < 0)
                    vertexType = VertexType.ConvexPoint;
                else if (dArea > 0)
                    vertexType = VertexType.ConcavePoint;
            }
            return vertexType;
        }


        /*********************************************
		To check the Line of vertex1, vertex2 is a Diagonal or not
  
		To be a diagonal, Line vertex1-vertex2 has no intersection 
		with polygon lines.
		
		If it is a diagonal, return true;
		If it is not a diagonal, return false;
		reference: www.swin.edu.au/astronomy/pbourke
		/geometry/lineline2d
		*********************************************/
        public bool Diagonal(Point2D vertex1, Point2D vertex2)
        {
            bool bDiagonal = false;
            int nNumOfVertices = _vertices.Length;
            int j = 0;
            for (int i = 0; i < nNumOfVertices; i++) //each point
            {
                bDiagonal = true;
                j = (i + 1) % nNumOfVertices;  //next point of i

                //Diagonal line:
                double x1 = vertex1.X;
                double y1 = vertex1.Y;
                double x2 = vertex1.X;
                double y2 = vertex1.Y;

                //CPolygon line:
                double x3 = _vertices[i].X;
                double y3 = _vertices[i].Y;
                double x4 = _vertices[j].X;
                double y4 = _vertices[j].Y;

                double de = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
                double ub = -1;

                if (Math.Abs(de - 0) > ConstantValue.SmallValue)  //lines are not parallel
                    ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / de;

                if ((ub > 0) && (ub < 1))
                {
                    bDiagonal = false;
                }
            }
            return bDiagonal;
        }


        /*************************************************
		To check FaVertices make a convex polygon or 
		concave polygon

		Restriction: the polygon is not self intersecting
		Ref: www.swin.edu.au/astronomy/pbourke
		/geometry/clockwise/index.html
		********************************************/
        public PolygonType GetPolygonType()
        {
            int nNumOfVertices = _vertices.Length;
            bool bSignChanged = false;
            int nCount = 0;
            int j = 0, k = 0;

            for (int i = 0; i < nNumOfVertices; i++)
            {
                j = (i + 1) % nNumOfVertices; //j:=i+1;
                k = (i + 2) % nNumOfVertices; //k:=i+2;

                double crossProduct = (_vertices[j].X - _vertices[i].X)
                    * (_vertices[k].Y - _vertices[j].Y);
                crossProduct = crossProduct - (
                    (_vertices[j].Y - _vertices[i].Y)
                    * (_vertices[k].X - _vertices[j].X)
                    );

                //change the value of nCount
                if ((crossProduct > 0) && (nCount == 0))
                    nCount = 1;
                else if ((crossProduct < 0) && (nCount == 0))
                    nCount = -1;

                if (((nCount == 1) && (crossProduct < 0))
                    || ((nCount == -1) && (crossProduct > 0)))
                    bSignChanged = true;
            }

            if (bSignChanged)
                return PolygonType.Concave;
            else
                return PolygonType.Convex;
        }

        /***************************************************
		Check a Vertex is a principal vertex or not
		ref. www-cgrl.cs.mcgill.ca/~godfried/teaching/
		cg-projects/97/Ian/glossay.html
  
		PrincipalVertex: a vertex pi of polygon P is a principal vertex if the
		diagonal pi-1, pi+1 intersects the boundary of P only at pi-1 and pi+1.
		*********************************************************/
        public bool PrincipalVertex(Point2D vertex)
        {
            bool bPrincipal = false;
            if (PolygonVertex(vertex)) //valid vertex
            {
                Point2D pt1 = PreviousPoint(vertex);
                Point2D pt2 = NextPoint(vertex);

                if (Diagonal(pt1, pt2))
                    bPrincipal = true;
            }
            return bPrincipal;
        }

        /*********************************************
        To check whether a given point is a CPolygon Vertex
		**********************************************/
        public bool PolygonVertex(Point2D point)
        {
            bool bVertex = false;
            int nIndex = VertexIndex(point);

            if ((nIndex >= 0) && (nIndex <= _vertices.Length - 1))
                bVertex = true;

            return bVertex;
        }

        /*****************************************************
		To reverse polygon vertices to different direction:
		clock-wise <------->count-clock-wise
		******************************************************/
        public void ReverseVerticesDirection()
        {
            int nVertices = _vertices.Length;
            Point2D[] aTempPts = new Point2D[nVertices];

            for (int i = 0; i < nVertices; i++)
                aTempPts[i] = _vertices[i];

            for (int i = 0; i < nVertices; i++)
                _vertices[i] = aTempPts[nVertices - 1 - i];
        }

        /*****************************************
		To check vertices make a clock-wise polygon or
		count clockwise polygon

		Restriction: the polygon is not self intersecting
		Ref: www.swin.edu.au/astronomy/pbourke/
		geometry/clockwise/index.html
		*****************************************/
        public PolygonDirection VerticesDirection()
        {
            int nCount = 0, j = 0, k = 0;
            int nVertices = _vertices.Length;

            for (int i = 0; i < nVertices; i++)
            {
                j = (i + 1) % nVertices; //j:=i+1;
                k = (i + 2) % nVertices; //k:=i+2;

                double crossProduct = (_vertices[j].X - _vertices[i].X)
                    * (_vertices[k].Y - _vertices[j].Y);
                crossProduct = crossProduct - (
                    (_vertices[j].Y - _vertices[i].Y)
                    * (_vertices[k].X - _vertices[j].X)
                    );

                if (crossProduct > 0)
                    nCount++;
                else
                    nCount--;
            }

            if (nCount < 0)
                return PolygonDirection.Count_Clockwise;
            else if (nCount > 0)
                return PolygonDirection.Clockwise;
            else
                return PolygonDirection.Unknown;
        }


        /*****************************************
		To check given points make a clock-wise polygon or
		count clockwise polygon

		Restriction: the polygon is not self intersecting
		*****************************************/
        public static PolygonDirection PointsDirection(
            Point2D[] points)
        {
            int nCount = 0, j = 0, k = 0;
            int nPoints = points.Length;

            if (nPoints < 3)
                return PolygonDirection.Unknown;

            for (int i = 0; i < nPoints; i++)
            {
                j = (i + 1) % nPoints; //j:=i+1;
                k = (i + 2) % nPoints; //k:=i+2;

                double crossProduct = (points[j].X - points[i].X)
                    * (points[k].Y - points[j].Y);
                crossProduct = crossProduct - (
                    (points[j].Y - points[i].Y)
                    * (points[k].X - points[j].X)
                    );

                if (crossProduct > 0)
                    nCount++;
                else
                    nCount--;
            }

            if (nCount < 0)
                return PolygonDirection.Count_Clockwise;
            else if (nCount > 0)
                return PolygonDirection.Clockwise;
            else
                return PolygonDirection.Unknown;
        }

        /*****************************************************
		To reverse points to different direction (order) :
		******************************************************/
        public static void ReversePointsDirection(
            Point2D[] points)
        {
            int nVertices = points.Length;
            Point2D[] aTempPts = new Point2D[nVertices];

            for (int i = 0; i < nVertices; i++)
                aTempPts[i] = points[i];

            for (int i = 0; i < nVertices; i++)
                points[i] = aTempPts[nVertices - 1 - i];
        }

    }
}