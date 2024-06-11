using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Mutable;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Immutable;

public sealed record BoundingBox3D : IBoundingBox3D
{
    public static BoundingBox3D CreateInfinite()
    {
        return new BoundingBox3D(
            double.NegativeInfinity,
            double.NegativeInfinity,
            double.NegativeInfinity,
            double.PositiveInfinity,
            double.PositiveInfinity,
            double.PositiveInfinity
        );
    }

    public static BoundingBox3D CreateInfiniteX(double y1, double z1, double y2, double z2)
    {
        if (y1 > y2)
        {
            (y1, y2) = (y2, y1);
        }

        if (z1 > z2)
        {
            (z1, z2) = (z2, z1);
        }

        return new BoundingBox3D(
            double.NegativeInfinity, y1, z1,
            double.PositiveInfinity, y2, z2
        );
    }

    public static BoundingBox3D CreateInfiniteY(double x1, double z1, double x2, double z2)
    {
        if (x1 > x2)
        {
            (x1, x2) = (x2, x1);
        }

        if (z1 > z2)
        {
            (z1, z2) = (z2, z1);
        }

        return new BoundingBox3D(
            x1, double.NegativeInfinity, z1,
            x2, double.PositiveInfinity, z2
        );
    }

    public static BoundingBox3D CreateInfiniteZ(double x1, double y1, double x2, double y2)
    {
        if (x1 > x2)
        {
            (x1, x2) = (x2, x1);
        }

        if (y1 > y2)
        {
            (y1, y2) = (y2, y1);
        }

        return new BoundingBox3D(
            x1, y1, double.NegativeInfinity,
            x2, y2, double.PositiveInfinity
        );
    }

    public static BoundingBox3D CreateInfiniteXy(double z1, double z2)
    {
        if (z1 > z2)
        {
            (z1, z2) = (z2, z1);
        }

        return new BoundingBox3D(
            double.NegativeInfinity, double.NegativeInfinity, z1,
            double.PositiveInfinity, double.PositiveInfinity, z2
        );
    }

    public static BoundingBox3D CreateInfiniteYz(double x1, double x2)
    {
        if (x1 > x2)
        {
            (x1, x2) = (x2, x1);
        }

        return new BoundingBox3D(
            x1, double.NegativeInfinity, double.NegativeInfinity,
            x2, double.PositiveInfinity, double.PositiveInfinity
        );
    }

    public static BoundingBox3D CreateInfiniteXz(double y1, double y2)
    {
        if (y1 > y2)
        {
            (y1, y2) = (y2, y1);
        }

        return new BoundingBox3D(
            double.NegativeInfinity, y1, double.NegativeInfinity,
            double.PositiveInfinity, y2, double.PositiveInfinity
        );
    }


    public static BoundingBox3D CreateAround(double centerX, double centerY, double centerZ, double deltaX, double deltaY, double deltaZ)
    {
        var minX = centerX - deltaX;
        var maxX = centerX + deltaX;
        var minY = centerY - deltaY;
        var maxY = centerY + deltaY;
        var minZ = centerZ - deltaZ;
        var maxZ = centerZ + deltaZ;

        if (deltaX < 0)
        {
            (maxX, minX) = (minX, maxX);
        }

        if (deltaY < 0)
        {
            (maxY, minY) = (minY, maxY);
        }

        if (deltaZ < 0)
        {
            (maxZ, minZ) = (minZ, maxZ);
        }

        return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static BoundingBox3D CreateAround(ILinFloat64Vector3D center, double deltaX, double deltaY, double deltaZ)
    {
        var minX = center.X - deltaX;
        var maxX = center.X + deltaX;
        var minY = center.Y - deltaY;
        var maxY = center.Y + deltaY;
        var minZ = center.Z - deltaZ;
        var maxZ = center.Z + deltaZ;

        if (deltaX < 0)
        {
            (maxX, minX) = (minX, maxX);
        }

        if (deltaY < 0)
        {
            (maxY, minY) = (minY, maxY);
        }

        if (deltaZ < 0)
        {
            (maxZ, minZ) = (minZ, maxZ);
        }

        return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }


    public static BoundingBox3D CreateFromPoint(ILinFloat64Vector3D point)
    {
        return new BoundingBox3D(
            point.X,
            point.Y,
            point.Z,
            point.X,
            point.Y,
            point.Z
        );
    }

    public static BoundingBox3D CreateFromPoints(double point1X, double point1Y, double point1Z, double point2X, double point2Y, double point2Z)
    {
        double minX, minY, minZ, maxX, maxY, maxZ;

        if (point1X <= point2X)
        {
            minX = point1X;
            maxX = point2X;
        }
        else
        {
            minX = point2X;
            maxX = point1X;
        }

        if (point1Y <= point2Y)
        {
            minY = point1Y;
            maxY = point2Y;
        }
        else
        {
            minY = point2Y;
            maxY = point1Y;
        }

        if (point1Z <= point2Z)
        {
            minZ = point1Z;
            maxZ = point2Z;
        }
        else
        {
            minZ = point2Z;
            maxZ = point1Z;
        }

        return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static BoundingBox3D CreateFromPoints(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2)
    {
        double minX, minY, minZ, maxX, maxY, maxZ;

        if (point1.X <= point2.X)
        {
            minX = point1.X;
            maxX = point2.X;
        }
        else
        {
            minX = point2.X;
            maxX = point1.X;
        }

        if (point1.Y <= point2.Y)
        {
            minY = point1.Y;
            maxY = point2.Y;
        }
        else
        {
            minY = point2.Y;
            maxY = point1.Y;
        }

        if (point1.Z <= point2.Z)
        {
            minZ = point1.Z;
            maxZ = point2.Z;
        }
        else
        {
            minZ = point2.Z;
            maxZ = point1.Z;
        }

        return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static BoundingBox3D CreateFromPoints(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, ILinFloat64Vector3D point3)
    {
        var minX = point1.X;
        var minY = point1.Y;
        var minZ = point1.Z;

        var maxX = point1.X;
        var maxY = point1.Y;
        var maxZ = point1.Z;

        if (minX > point2.X) minX = point2.X;
        if (minX > point3.X) minX = point3.X;

        if (minY > point2.Y) minY = point2.Y;
        if (minY > point3.Y) minY = point3.Y;

        if (minZ > point2.Z) minZ = point2.Z;
        if (minZ > point3.Z) minZ = point3.Z;

        if (maxX < point2.X) maxX = point2.X;
        if (maxX < point3.X) maxX = point3.X;

        if (maxY < point2.Y) maxY = point2.Y;
        if (maxY < point3.Y) maxY = point3.Y;

        if (maxZ < point2.Z) maxZ = point2.Z;
        if (maxZ < point3.Z) maxZ = point3.Z;

        return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static BoundingBox3D CreateFromPoints(params ILinFloat64Vector3D[] pointsList)
    {
        var point1 = pointsList[0];

        var minX = point1.X;
        var minY = point1.Y;
        var minZ = point1.Z;

        var maxX = point1.X;
        var maxY = point1.Y;
        var maxZ = point1.Z;

        foreach (var point in pointsList.Skip(1))
        {
            if (minX > point.X) minX = point.X;
            if (minY > point.Y) minY = point.Y;
            if (minZ > point.Z) minZ = point.Z;

            if (maxX < point.X) maxX = point.X;
            if (maxY < point.Y) maxY = point.Y;
            if (maxZ < point.Z) maxZ = point.Z;
        }

        return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static BoundingBox3D CreateFromPoints(IEnumerable<ILinFloat64Vector3D> pointsList)
    {
        double minX = 0, minY = 0, minZ = 0;
        double maxX = 0, maxY = 0, maxZ = 0;

        var flag = false;
        foreach (var point in pointsList)
        {
            if (!flag)
            {
                minX = point.X;
                minY = point.Y;
                minZ = point.Z;

                maxX = point.X;
                maxY = point.Y;
                maxZ = point.Z;

                flag = true;
                continue;
            }

            if (minX > point.X) minX = point.X;
            if (minY > point.Y) minY = point.Y;
            if (minZ > point.Z) minZ = point.Z;

            if (maxX < point.X) maxX = point.X;
            if (maxY < point.Y) maxY = point.Y;
            if (maxZ < point.Z) maxZ = point.Z;
        }

        return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static BoundingBox3D CreateFromPoints(IEnumerable<ILinFloat64Vector3D> pointsList, double scalingFactor)
    {
        double minX = 0, minY = 0, minZ = 0;
        double maxX = 0, maxY = 0, maxZ = 0;

        var flag = false;
        foreach (var point in pointsList)
        {
            if (!flag)
            {
                minX = point.X;
                minY = point.Y;
                minZ = point.Z;

                maxX = point.X;
                maxY = point.Y;
                maxZ = point.Z;

                flag = true;
                continue;
            }

            if (minX > point.X) minX = point.X;
            if (minY > point.Y) minY = point.Y;
            if (minZ > point.Z) minZ = point.Z;

            if (maxX < point.X) maxX = point.X;
            if (maxY < point.Y) maxY = point.Y;
            if (maxZ < point.Z) maxZ = point.Z;
        }

        var midX = 0.5d * (maxX + minX);
        var midY = 0.5d * (maxY + minY);
        var midZ = 0.5d * (maxZ + minZ);

        minX = (minX - midX) * scalingFactor + midX;
        minY = (minY - midY) * scalingFactor + midY;
        minZ = (minZ - midZ) * scalingFactor + midZ;

        maxX = (maxX - midX) * scalingFactor + midX;
        maxY = (maxY - midY) * scalingFactor + midY;
        maxZ = (maxZ - midZ) * scalingFactor + midZ;

        return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }


    public static BoundingBox3D Create(ILinFloat64Vector3D point)
    {
        return new BoundingBox3D(
            point.X,
            point.Y,
            point.Z,
            point.X,
            point.Y,
            point.Z
        );
    }

    public static BoundingBox3D Create(double point1X, double point1Y, double point1Z, double point2X, double point2Y, double point2Z)
    {
        double minX, minY, minZ, maxX, maxY, maxZ;

        if (point1X <= point2X)
        {
            minX = point1X;
            maxX = point2X;
        }
        else
        {
            minX = point2X;
            maxX = point1X;
        }

        if (point1Y <= point2Y)
        {
            minY = point1Y;
            maxY = point2Y;
        }
        else
        {
            minY = point2Y;
            maxY = point1Y;
        }

        if (point1Z <= point2Z)
        {
            minZ = point1Z;
            maxZ = point2Z;
        }
        else
        {
            minZ = point2Z;
            maxZ = point1Z;
        }

        return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }


    public static BoundingBox3D Create(IBoundingBox3D boundingBox)
    {
        return new BoundingBox3D(boundingBox);
    }

    public static BoundingBox3D Create(IBoundingBox3D b1, IBoundingBox3D b2)
    {
        return new BoundingBox3D(
            Math.Min(b1.MinX, b2.MinX),
            Math.Min(b1.MinY, b2.MinY),
            Math.Min(b1.MinZ, b2.MinZ),
            Math.Max(b1.MaxX, b2.MaxX),
            Math.Max(b1.MaxY, b2.MaxY),
            Math.Max(b1.MaxZ, b2.MaxZ)
        );
    }

    public static BoundingBox3D Create(params IBoundingBox3D[] boundingBoxesList)
    {
        var result = new MutableBoundingBox3D();

        var flag = false;
        foreach (var boundingBox in boundingBoxesList)
        {
            if (!flag)
            {
                result = boundingBox.GetMutableBoundingBox();

                flag = true;
                continue;
            }

            result.ExpandToInclude(boundingBox);
        }

        return result.GetBoundingBox();
    }

    public static BoundingBox3D Create(IEnumerable<IBoundingBox3D> boundingBoxesList)
    {
        var result = new MutableBoundingBox3D();

        var flag = false;
        foreach (var boundingBox in boundingBoxesList)
        {
            if (!flag)
            {
                result = boundingBox.GetMutableBoundingBox();

                flag = true;
                continue;
            }

            result.ExpandToInclude(boundingBox);
        }

        return result.GetBoundingBox();
    }

    public static BoundingBox3D CreateFromIntersection(IBoundingBox3D b1, IBoundingBox3D b2)
    {
        return new BoundingBox3D(
            Math.Max(b1.MinX, b2.MinX),
            Math.Max(b1.MinY, b2.MinY),
            Math.Max(b1.MinZ, b2.MinZ),
            Math.Min(b1.MaxX, b2.MaxX),
            Math.Min(b1.MaxY, b2.MaxY),
            Math.Min(b1.MaxZ, b2.MaxZ)
        );
    }


    public static BoundingBox3D Create(IFiniteGeometricShape3D geometricObject)
    {
        return geometricObject.GetBoundingBox();
    }

    public static BoundingBox3D Create(params IFiniteGeometricShape3D[] geometricObjectsList)
    {
        var result = new MutableBoundingBox3D();

        var flag = false;
        foreach (var geometricObject in geometricObjectsList)
        {
            if (!flag)
            {
                result = geometricObject.GetMutableBoundingBox();

                flag = true;
                continue;
            }

            result.ExpandToInclude(geometricObject.GetBoundingBox());
        }

        return result.GetBoundingBox();
    }

    public static BoundingBox3D Create<T>(IEnumerable<T> geometricObjectsList)
        where T : IFiniteGeometricShape3D
    {
        var result = new MutableBoundingBox3D();

        var flag = false;
        foreach (var geometricObject in geometricObjectsList)
        {
            if (!flag)
            {
                result = geometricObject.GetMutableBoundingBox();

                flag = true;
                continue;
            }

            result.ExpandToInclude(geometricObject.GetBoundingBox());
        }

        return result.GetBoundingBox();
    }


    public double MinX { get; }

    public double MinY { get; }

    public double MinZ { get; }

    public double MaxX { get; }

    public double MaxY { get; }

    public double MaxZ { get; }

    public double MidX
        => 0.5d * (MinX + MaxX);

    public double MidY
        => 0.5d * (MinY + MaxY);

    public double MidZ
        => 0.5d * (MinZ + MaxZ);

    public bool IsValid()
    {
        return MinX.IsValid() &&
               MinY.IsValid() &&
               MinZ.IsValid() &&
               MaxX.IsValid() &&
               MaxY.IsValid() &&
               MaxZ.IsValid();
    }

    public bool IntersectionTestsEnabled { get; set; } = true;


    public BoundingBox3D(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
    {
        MinX = minX;
        MinY = minY;
        MinZ = minZ;

        MaxY = maxY;
        MaxX = maxX;
        MaxZ = maxZ;

        Debug.Assert(IsValid());
    }

    public BoundingBox3D(IBoundingBox3D boundingBox)
    {
        MinX = boundingBox.MinX;
        MinY = boundingBox.MinY;
        MinZ = boundingBox.MinZ;

        MaxY = boundingBox.MaxY;
        MaxX = boundingBox.MaxX;
        MaxZ = boundingBox.MaxZ;

        Debug.Assert(IsValid());
    }


    public IBorderSurface3D MapUsing(IAffineMap3D affineMap)
    {
        throw new NotImplementedException();
    }

    public BoundingBox3D GetBoundingBox()
    {
        return this;
    }

    public MutableBoundingBox3D GetMutableBoundingBox()
    {
        return new MutableBoundingBox3D(this);
    }

    public bool TestLineSegmentIntersection(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2)
    {
        throw new NotImplementedException();
    }
}