using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

public sealed class Float64BoundingCircle2D :
    IFloat64BorderCurve2D
{
    public static Float64BoundingCircle2D Create(double centerX, double centerY, double radius)
    {
        return new Float64BoundingCircle2D(centerX, centerY, radius);
    }

    public static Float64BoundingCircle2D Create(ILinFloat64Vector2D center, double radius)
    {
        return new Float64BoundingCircle2D(center.X, center.Y, radius);
    }

    public static Float64BoundingCircle2D CreateFromPoints(IEnumerable<ILinFloat64Vector2D> pointsList, double margin = 0, bool tightBound = true)
    {
        var pointsArray = pointsList.ToArray();

        if (!tightBound)
        {
            var center = pointsArray.GetCenterPoint();
            var radius =
                Math.Sqrt(
                    center
                        .GetDistancesSquaredToPoints(pointsArray)
                        .Max()
                );

            //This is the fast but not tight method
            return new Float64BoundingCircle2D(center, radius + margin);
        }

        //This is the slower but tighter method
        var maxDistance = 0.0d;
        var maxPoint1 = pointsArray[0];
        var maxPoint2 = pointsArray[0];

        for (var i = 0; i < pointsArray.Length - 1; i++)
        {
            var p1 = pointsArray[i];

            for (var j = i + 1; j < pointsArray.Length; j++)
            {
                var p2 = pointsArray[j];
                var distance = p1.GetDistanceToPoint(p2);

                if (distance <= maxDistance)
                    continue;

                maxDistance = distance;
                maxPoint1 = p1;
                maxPoint2 = p2;
            }
        }

        var center1 = LinFloat64Vector2D.Create(0.5 * (maxPoint1.X + maxPoint2.X),
            0.5 * (maxPoint1.Y + maxPoint2.Y));

        var radius1 = 0.5 * maxDistance;

        return new Float64BoundingCircle2D(center1, radius1 + margin);
    }


    public double Radius { get; }

    public double CenterX { get; }

    public double CenterY { get; }

    public LinFloat64Vector2D Center
    {
        get { return LinFloat64Vector2D.Create((Float64Scalar)CenterX, (Float64Scalar)CenterY); }
    }

    public bool IntersectionTestsEnabled { get; set; } = true;

    public bool IsValid()
    {
        return !double.IsNaN(Radius) &&
               !double.IsNaN(CenterX) &&
               !double.IsNaN(CenterY);
    }


    internal Float64BoundingCircle2D(ILinFloat64Vector2D center, double radius)
    {
        CenterX = center.X;
        CenterY = center.Y;
        Radius = Math.Abs(radius);

        Debug.Assert(IsValid());
    }

    internal Float64BoundingCircle2D(double centerX, double centerY, double radius)
    {
        CenterX = centerX;
        CenterY = centerY;
        Radius = Math.Abs(radius);

        Debug.Assert(IsValid());
    }


    public Float64BoundingBox2D GetBoundingBox()
    {
        var point1 = LinFloat64Vector2D.Create((Float64Scalar)(CenterX - Radius), (Float64Scalar)(CenterY - Radius));
        var point2 = LinFloat64Vector2D.Create((Float64Scalar)(CenterX + Radius), (Float64Scalar)(CenterY + Radius));

        return Float64BoundingBox2D.Create(point1, point2);
    }

    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        var point1 = LinFloat64Vector2D.Create((Float64Scalar)(CenterX - Radius), (Float64Scalar)(CenterY - Radius));
        var point2 = LinFloat64Vector2D.Create((Float64Scalar)(CenterX + Radius), (Float64Scalar)(CenterY + Radius));

        return Float64BoundingBoxComposer2D.CreateFromPoints(point1, point2);
    }

    public IFloat64BorderCurve2D MapUsing(IFloat64AffineMap2D affineMap)
    {
        var s1 = affineMap.MapVector(LinFloat64Vector2D.E1).ToLinVector2D().VectorENorm();
        var s2 = affineMap.MapVector(LinFloat64Vector2D.E2).ToLinVector2D().VectorENorm();

        var sMax = s1 > s2 ? s1 : s2;

        var center = affineMap.MapPoint(Center);

        return new Float64BoundingCircle2D(
            center.X,
            center.Y,
            sMax * Radius
        );
    }
}