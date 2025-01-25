using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;

public sealed class Float64BoundingSphere3D :
    IFloat64BorderSurface3D
{
    public double Radius { get; }

    public double CenterX { get; }

    public double CenterY { get; }

    public double CenterZ { get; }

    public bool IsValid()
    {
        return !double.IsNaN(Radius) &&
               !double.IsNaN(CenterX) &&
               !double.IsNaN(CenterY);
    }

    public bool IntersectionTestsEnabled { get; set; } = true;

    public LinFloat64Vector3D Center
    {
        get { return LinFloat64Vector3D.Create(CenterX, CenterY, CenterZ); }
    }


    public Float64BoundingSphere3D(double centerX, double centerY, double centerZ, double radius)
    {
        CenterX = centerX;
        CenterY = centerY;
        CenterZ = centerZ;
        Radius = radius;
    }


    public IFloat64BorderSurface3D MapUsing(IFloat64AffineMap3D affineMap)
    {
        var s1 = affineMap.MapVector(LinFloat64Vector3D.E1).VectorENorm();
        var s2 = affineMap.MapVector(LinFloat64Vector3D.E2).VectorENorm();
        var s3 = affineMap.MapVector(LinFloat64Vector3D.E3).VectorENorm();

        var sMax = s1 > s2 ? s1 : s2;
        if (s3 > sMax) sMax = s3;

        var center = affineMap.MapPoint(Center);

        return new Float64BoundingSphere3D(center.X, center.Y, center.Z, sMax * Radius);
    }

    public Float64BoundingBox3D GetBoundingBox()
    {
        var point1 = LinFloat64Vector3D.Create(CenterX - Radius, CenterY - Radius, CenterZ - Radius);
        var point2 = LinFloat64Vector3D.Create(CenterX + Radius, CenterY + Radius, CenterZ + Radius);

        return Float64BoundingBox3D.CreateFromPoints(point1, point2);
    }

    public Float64BoundingBoxComposer3D GetBoundingBoxComposer()
    {
        var point1 = LinFloat64Vector3D.Create(CenterX - Radius, CenterY - Radius, CenterZ - Radius);
        var point2 = LinFloat64Vector3D.Create(CenterX + Radius, CenterY + Radius, CenterZ + Radius);

        return Float64BoundingBoxComposer3D.CreateFromPoints(point1, point2);
    }

    public bool TestLineSegmentIntersection(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2)
    {
        var d1 = (point1.X - CenterX) * (point1.X - CenterX) +
                 (point1.Y - CenterY) * (point1.Y - CenterY) +
                 (point1.Z - CenterZ) * (point1.Z - CenterZ);

        var d2 = (point2.X - CenterX) * (point2.X - CenterX) +
                 (point2.Y - CenterY) * (point2.Y - CenterY) +
                 (point2.Z - CenterZ) * (point2.Z - CenterZ);

        var r = Radius * Radius;

        return d1 <= r && d2 >= r || d2 <= r && d1 >= r;
    }
}