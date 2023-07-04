using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Mutable;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace NumericalGeometryLib.Computers.Voronoi
{
    public sealed class VoronoiTriangle2D : ITriangle2D
    {
        public int PointIndex1 { get; }

        public int PointIndex2 { get; }

        public int PointIndex3 { get; }

        public VoronoiPointsList PointsList { get; }

        public double Point1X => PointsList[PointIndex1].X;

        public double Point1Y => PointsList[PointIndex1].Y;

        public double Point2X => PointsList[PointIndex2].X;

        public double Point2Y => PointsList[PointIndex2].Y;

        public double Point3X => PointsList[PointIndex3].X;

        public double Point3Y => PointsList[PointIndex3].Y;

        public bool IsValid()
        {
            return !double.IsNaN(Point1X) &&
                   !double.IsNaN(Point1Y) &&
                   !double.IsNaN(Point2X) &&
                   !double.IsNaN(Point2Y) &&
                   !double.IsNaN(Point3X) &&
                   !double.IsNaN(Point3Y);
        }

        public bool IntersectionTestsEnabled { get; set; } = true;

        public bool IsBad { get; internal set; }

        public Float64Vector2D Point1 => PointsList[PointIndex1];

        public Float64Vector2D Point2 => PointsList[PointIndex2];

        public Float64Vector2D Point3 => PointsList[PointIndex3];

        public VoronoiEdge2D Edge12 => new(PointsList, PointIndex1, PointIndex2);

        public VoronoiEdge2D Edge23 => new(PointsList, PointIndex2, PointIndex3);

        public VoronoiEdge2D Edge31 => new(PointsList, PointIndex3, PointIndex1);

        public IEnumerable<VoronoiEdge2D> Edges
        {
            get
            {
                yield return Edge12;
                yield return Edge23;
                yield return Edge31;
            }
        }


        internal VoronoiTriangle2D(VoronoiPointsList pointsList, int pointIndex1, int pointIndex2, int pointIndex3)
        {
            PointsList = pointsList;

            PointIndex1 = pointIndex1;
            PointIndex2 = pointIndex2;
            PointIndex3 = pointIndex3;
        }

        internal VoronoiTriangle2D(VoronoiEdge2D basEdge, int pointIndex3)
        {
            PointsList = basEdge.PointsList;

            PointIndex1 = basEdge.PointIndex1;
            PointIndex2 = basEdge.PointIndex2;
            PointIndex3 = pointIndex3;
        }


        public BoundingBox2D GetBoundingBox()
        {
            throw new System.NotImplementedException();
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
        {
            throw new System.NotImplementedException();
        }


        internal bool ContainsPoint(int pointIndex)
        {
            return PointIndex1 == pointIndex ||
                   PointIndex2 == pointIndex ||
                   PointIndex3 == pointIndex;
        }

        internal bool ContainsBoundingTrianglePoint()
        {
            var pointIndex1 = PointsList.BoundingTriangle.PointIndex1;
            var pointIndex2 = PointsList.BoundingTriangle.PointIndex2;
            var pointIndex3 = PointsList.BoundingTriangle.PointIndex3;

            return PointIndex1 == pointIndex1 ||
                   PointIndex1 == pointIndex2 ||
                   PointIndex1 == pointIndex3 ||
                   PointIndex2 == pointIndex1 ||
                   PointIndex2 == pointIndex2 ||
                   PointIndex2 == pointIndex3 ||
                   PointIndex3 == pointIndex1 ||
                   PointIndex3 == pointIndex2 ||
                   PointIndex3 == pointIndex3;
        }

        public bool CircumcircleContainsVa(IFloat64Vector2D point)
        {
            var ab = Point1X * Point1X + Point1Y * Point1Y;
            var cd = Point2X * Point2X + Point2Y * Point2Y;
            var ef = Point3X * Point3X + Point3Y * Point3Y;

            var centerX = 0.5d * (ab * (Point3Y - Point2Y) + cd * (Point1Y - Point3Y) + ef * (Point2Y - Point1Y)) / (Point1X * (Point3Y - Point2Y) + Point2X * (Point1Y - Point3Y) + Point3X * (Point2Y - Point1Y));
            var centerY = 0.5d * (ab * (Point3X - Point2X) + cd * (Point1X - Point3X) + ef * (Point2X - Point1X)) / (Point1Y * (Point3X - Point2X) + Point2Y * (Point1X - Point3X) + Point3Y * (Point2X - Point1X));
            var center = Float64Vector2D.Create((Float64Scalar)centerX, (Float64Scalar)centerY);

            var radiusSquared = center.GetDistanceSquaredToPoint(Point1X, Point1Y);

            var distSquared = center.GetDistanceSquaredToPoint(point);

            return distSquared <= radiusSquared;
        }

        public bool CircumcircleContains(double pointX, double pointY)
        {
            var point1X = Point1X;
            var point1Y = Point1Y;

            var point2X = Point2X;
            var point2Y = Point2Y;

            var point3X = Point3X;
            var point3Y = Point3Y;

            //Begin GMac Macro Code Generation, 2018-12-28T18:22:43.3883884+02:00
            //Macro: GeometryComposerLib.cga4d.PointToCircumcircleDistanceSquared
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 37 sub-expressions, 0 generated temps, 37 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 1 total.
            //Computations: 1.13157894736842 average, 43 total.
            //Memory Reads: 1.60526315789474 average, 61 total.
            //Memory Writes: 38 total.
            //
            //Macro Binding Data: 
            //   result = variable: var distanceSquared
            //   point.#e1# = variable: pointX
            //   point.#e2# = variable: pointY
            //   p1.#e1# = variable: point1X
            //   p1.#e2# = variable: point1Y
            //   p2.#e1# = variable: point2X
            //   p2.#e2# = variable: point2Y
            //   p3.#e1# = variable: point3X
            //   p3.#e2# = variable: point3Y
            
            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            
            //Sub-expression: LLDI004F = Power[LLDI0008,2]
            tmp0 = point3X * point3X;
            
            //Sub-expression: LLDI0050 = Power[LLDI0009,2]
            tmp1 = point3Y * point3Y;
            
            //Sub-expression: LLDI0051 = Plus[LLDI004F,LLDI0050]
            tmp0 = tmp0 + tmp1;
            
            //Sub-expression: LLDI0052 = Times[-1,LLDI0006]
            tmp1 = -point2X;
            
            //Sub-expression: LLDI0053 = Plus[LLDI0004,LLDI0052]
            tmp1 = point1X + tmp1;
            
            //Sub-expression: LLDI0054 = Times[Rational[1,2],LLDI0051,LLDI0053]
            tmp1 = 0.5 * tmp0 * tmp1;
            
            //Sub-expression: LLDI0055 = Power[LLDI0004,2]
            tmp2 = point1X * point1X;
            
            //Sub-expression: LLDI0056 = Power[LLDI0005,2]
            tmp3 = point1Y * point1Y;
            
            //Sub-expression: LLDI0057 = Plus[LLDI0055,LLDI0056]
            tmp2 = tmp2 + tmp3;
            
            //Sub-expression: LLDI0058 = Times[-1,LLDI0006,LLDI0057]
            tmp3 = -1 * point2X * tmp2;
            
            //Sub-expression: LLDI0059 = Power[LLDI0006,2]
            tmp4 = point2X * point2X;
            
            //Sub-expression: LLDI005A = Power[LLDI0007,2]
            tmp5 = point2Y * point2Y;
            
            //Sub-expression: LLDI005B = Plus[LLDI0059,LLDI005A]
            tmp4 = tmp4 + tmp5;
            
            //Sub-expression: LLDI005C = Times[LLDI0004,LLDI005B]
            tmp5 = point1X * tmp4;
            
            //Sub-expression: LLDI005D = Plus[LLDI0058,LLDI005C]
            tmp3 = tmp3 + tmp5;
            
            //Sub-expression: LLDI005E = Times[Rational[1,2],LLDI005D]
            tmp3 = 0.5 * tmp3;
            
            //Sub-expression: LLDI005F = Times[-1,LLDI005E]
            tmp3 = -tmp3;
            
            //Sub-expression: LLDI0060 = Plus[LLDI0054,LLDI005F]
            tmp1 = tmp1 + tmp3;
            
            //Sub-expression: LLDI0061 = Times[-1,LLDI0057]
            tmp3 = -tmp2;
            
            //Sub-expression: LLDI0062 = Plus[LLDI0061,LLDI005B]
            tmp3 = tmp3 + tmp4;
            
            //Sub-expression: LLDI0063 = Times[Rational[1,2],LLDI0062]
            tmp3 = 0.5 * tmp3;
            
            //Sub-expression: LLDI0064 = Times[LLDI0008,LLDI0063]
            tmp5 = point3X * tmp3;
            
            //Sub-expression: LLDI0065 = Plus[LLDI0060,LLDI0064]
            tmp1 = tmp1 + tmp5;
            
            //Sub-expression: LLDI0066 = Times[-1,LLDI0003,LLDI0065]
            tmp1 = -1 * pointY * tmp1;
            
            //Sub-expression: LLDI0067 = Times[-1,LLDI0007]
            tmp5 = -point2Y;
            
            //Sub-expression: LLDI0068 = Plus[LLDI0005,LLDI0067]
            tmp5 = point1Y + tmp5;
            
            //Sub-expression: LLDI0069 = Times[Rational[1,2],LLDI0051,LLDI0068]
            tmp0 = 0.5 * tmp0 * tmp5;
            
            //Sub-expression: LLDI006A = Times[-1,LLDI0007,LLDI0057]
            tmp2 = -1 * point2Y * tmp2;
            
            //Sub-expression: LLDI006B = Times[LLDI0005,LLDI005B]
            tmp4 = point1Y * tmp4;
            
            //Sub-expression: LLDI006C = Plus[LLDI006A,LLDI006B]
            tmp2 = tmp2 + tmp4;
            
            //Sub-expression: LLDI006D = Times[Rational[1,2],LLDI006C]
            tmp2 = 0.5 * tmp2;
            
            //Sub-expression: LLDI006E = Times[-1,LLDI006D]
            tmp2 = -tmp2;
            
            //Sub-expression: LLDI006F = Plus[LLDI0069,LLDI006E]
            tmp0 = tmp0 + tmp2;
            
            //Sub-expression: LLDI0070 = Times[LLDI0009,LLDI0063]
            tmp2 = point3Y * tmp3;
            
            //Sub-expression: LLDI0071 = Plus[LLDI006F,LLDI0070]
            tmp0 = tmp0 + tmp2;
            
            //Sub-expression: LLDI0072 = Times[LLDI0002,LLDI0071]
            tmp0 = pointX * tmp0;
            
            //Sub-expression: LLDI0073 = Plus[LLDI0066,LLDI0072]
            tmp0 = tmp1 + tmp0;
            
            //Output: LLDI0001 = Times[-2,LLDI0073]
            var distanceSquared = -2 * tmp0;
            
            
            //Finish GMac Macro Code Generation, 2018-12-28T18:22:43.4043776+02:00

            return distanceSquared <= 0;
        }
    }
}