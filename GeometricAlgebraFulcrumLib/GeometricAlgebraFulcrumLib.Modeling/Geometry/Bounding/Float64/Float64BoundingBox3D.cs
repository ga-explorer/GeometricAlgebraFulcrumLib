using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

public sealed record Float64BoundingBox3D :
    IFloat64BoundingBox<Float64BoundingBox3D, LinFloat64Vector3D>,
    IFloat64BoundingSurface3D
{
    public static Float64BoundingBox3D EmptyInstance { get; }
        = new Float64BoundingBox3D();

    public static Float64BoundingBox3D Empty(int vSpaceDimensions = 3)
        => EmptyInstance;

    public static Float64BoundingBox3D Infinite { get; }
        = new Float64BoundingBox3D(
            double.NegativeInfinity,
            double.NegativeInfinity,
            double.NegativeInfinity,
            double.PositiveInfinity,
            double.PositiveInfinity,
            double.PositiveInfinity
        );


    public static Float64BoundingBox3D InfiniteX(double y1, double z1, double y2, double z2)
    {
        if (y1 > y2)
        {
            (y1, y2) = (y2, y1);
        }

        if (z1 > z2)
        {
            (z1, z2) = (z2, z1);
        }

        return new Float64BoundingBox3D(
            double.NegativeInfinity, y1, z1,
            double.PositiveInfinity, y2, z2
        );
    }

    public static Float64BoundingBox3D InfiniteY(double x1, double z1, double x2, double z2)
    {
        if (x1 > x2)
        {
            (x1, x2) = (x2, x1);
        }

        if (z1 > z2)
        {
            (z1, z2) = (z2, z1);
        }

        return new Float64BoundingBox3D(
            x1, double.NegativeInfinity, z1,
            x2, double.PositiveInfinity, z2
        );
    }

    public static Float64BoundingBox3D InfiniteZ(double x1, double y1, double x2, double y2)
    {
        if (x1 > x2)
        {
            (x1, x2) = (x2, x1);
        }

        if (y1 > y2)
        {
            (y1, y2) = (y2, y1);
        }

        return new Float64BoundingBox3D(
            x1, y1, double.NegativeInfinity,
            x2, y2, double.PositiveInfinity
        );
    }

    public static Float64BoundingBox3D InfiniteXy(double z1, double z2)
    {
        if (z1 > z2)
        {
            (z1, z2) = (z2, z1);
        }

        return new Float64BoundingBox3D(
            double.NegativeInfinity, double.NegativeInfinity, z1,
            double.PositiveInfinity, double.PositiveInfinity, z2
        );
    }

    public static Float64BoundingBox3D InfiniteYz(double x1, double x2)
    {
        if (x1 > x2)
        {
            (x1, x2) = (x2, x1);
        }

        return new Float64BoundingBox3D(
            x1, double.NegativeInfinity, double.NegativeInfinity,
            x2, double.PositiveInfinity, double.PositiveInfinity
        );
    }

    public static Float64BoundingBox3D InfiniteXz(double y1, double y2)
    {
        if (y1 > y2)
        {
            (y1, y2) = (y2, y1);
        }

        return new Float64BoundingBox3D(
            double.NegativeInfinity, y1, double.NegativeInfinity,
            double.PositiveInfinity, y2, double.PositiveInfinity
        );
    }


    public static Float64BoundingBox3D Around(double centerX, double centerY, double centerZ, double deltaX, double deltaY, double deltaZ)
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

        return new Float64BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static Float64BoundingBox3D Around(LinFloat64Vector3D center, double deltaX, double deltaY, double deltaZ)
    {
        var minX = center.Item1 - deltaX;
        var maxX = center.Item1 + deltaX;
        var minY = center.Item2 - deltaY;
        var maxY = center.Item2 + deltaY;
        var minZ = center.Item3 - deltaZ;
        var maxZ = center.Item3 + deltaZ;

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

        return new Float64BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }


    public static Float64BoundingBox3D Containing(LinFloat64Vector3D point)
    {
        return new Float64BoundingBox3D(
            point.Item1,
            point.Item2,
            point.Item3,
            point.Item1,
            point.Item2,
            point.Item3
        );
    }

    public static Float64BoundingBox3D Containing(double point1X, double point1Y, double point1Z, double point2X, double point2Y, double point2Z)
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

        return new Float64BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static Float64BoundingBox3D Containing(LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        double minX, minY, minZ, maxX, maxY, maxZ;

        if (point1.Item1 <= point2.Item1)
        {
            minX = point1.Item1;
            maxX = point2.Item1;
        }
        else
        {
            minX = point2.Item1;
            maxX = point1.Item1;
        }

        if (point1.Item2 <= point2.Item2)
        {
            minY = point1.Item2;
            maxY = point2.Item2;
        }
        else
        {
            minY = point2.Item2;
            maxY = point1.Item2;
        }

        if (point1.Item3 <= point2.Item3)
        {
            minZ = point1.Item3;
            maxZ = point2.Item3;
        }
        else
        {
            minZ = point2.Item3;
            maxZ = point1.Item3;
        }

        return new Float64BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static Float64BoundingBox3D Containing(LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var minX = point1.Item1;
        var minY = point1.Item2;
        var minZ = point1.Item3;

        var maxX = point1.Item1;
        var maxY = point1.Item2;
        var maxZ = point1.Item3;

        if (minX > point2.Item1) minX = point2.Item1;
        if (minX > point3.Item1) minX = point3.Item1;

        if (minY > point2.Item2) minY = point2.Item2;
        if (minY > point3.Item2) minY = point3.Item2;

        if (minZ > point2.Item3) minZ = point2.Item3;
        if (minZ > point3.Item3) minZ = point3.Item3;

        if (maxX < point2.Item1) maxX = point2.Item1;
        if (maxX < point3.Item1) maxX = point3.Item1;

        if (maxY < point2.Item2) maxY = point2.Item2;
        if (maxY < point3.Item2) maxY = point3.Item2;

        if (maxZ < point2.Item3) maxZ = point2.Item3;
        if (maxZ < point3.Item3) maxZ = point3.Item3;

        return new Float64BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static Float64BoundingBox3D Containing(params LinFloat64Vector3D[] pointsList)
    {
        var point1 = pointsList[0];

        var minX = point1.Item1;
        var minY = point1.Item2;
        var minZ = point1.Item3;

        var maxX = point1.Item1;
        var maxY = point1.Item2;
        var maxZ = point1.Item3;

        foreach (var point in pointsList.Skip(1))
        {
            if (minX > point.Item1) minX = point.Item1;
            if (minY > point.Item2) minY = point.Item2;
            if (minZ > point.Item3) minZ = point.Item3;

            if (maxX < point.Item1) maxX = point.Item1;
            if (maxY < point.Item2) maxY = point.Item2;
            if (maxZ < point.Item3) maxZ = point.Item3;
        }

        return new Float64BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static Float64BoundingBox3D Containing(IEnumerable<LinFloat64Vector3D> pointsList)
    {
        double minX = 0, minY = 0, minZ = 0;
        double maxX = 0, maxY = 0, maxZ = 0;

        var flag = false;
        foreach (var point in pointsList)
        {
            if (!flag)
            {
                minX = point.Item1;
                minY = point.Item2;
                minZ = point.Item3;

                maxX = point.Item1;
                maxY = point.Item2;
                maxZ = point.Item3;

                flag = true;
                continue;
            }

            if (minX > point.Item1) minX = point.Item1;
            if (minY > point.Item2) minY = point.Item2;
            if (minZ > point.Item3) minZ = point.Item3;

            if (maxX < point.Item1) maxX = point.Item1;
            if (maxY < point.Item2) maxY = point.Item2;
            if (maxZ < point.Item3) maxZ = point.Item3;
        }

        return new Float64BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static Float64BoundingBox3D Containing(IEnumerable<LinFloat64Vector3D> pointsList, double scalingFactor)
    {
        double minX = 0, minY = 0, minZ = 0;
        double maxX = 0, maxY = 0, maxZ = 0;

        var flag = false;
        foreach (var point in pointsList)
        {
            if (!flag)
            {
                minX = point.Item1;
                minY = point.Item2;
                minZ = point.Item3;

                maxX = point.Item1;
                maxY = point.Item2;
                maxZ = point.Item3;

                flag = true;
                continue;
            }

            if (minX > point.Item1) minX = point.Item1;
            if (minY > point.Item2) minY = point.Item2;
            if (minZ > point.Item3) minZ = point.Item3;

            if (maxX < point.Item1) maxX = point.Item1;
            if (maxY < point.Item2) maxY = point.Item2;
            if (maxZ < point.Item3) maxZ = point.Item3;
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

        return new Float64BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }


    public static Float64BoundingBox3D Containing(Float64BoundingBox3D b1, Float64BoundingBox3D b2)
    {
        return new Float64BoundingBox3D(
            Math.Min(b1.MinX, b2.MinX),
            Math.Min(b1.MinY, b2.MinY),
            Math.Min(b1.MinZ, b2.MinZ),
            Math.Max(b1.MaxX, b2.MaxX),
            Math.Max(b1.MaxY, b2.MaxY),
            Math.Max(b1.MaxZ, b2.MaxZ)
        );
    }

    public static Float64BoundingBox3D Containing(params Float64BoundingBox3D[] boundingBoxesList)
    {
        var result = new Float64BoundingBoxComposer3D();

        var flag = false;
        foreach (var boundingBox in boundingBoxesList)
        {
            if (!flag)
            {
                result = boundingBox.GetBoundingBoxComposer();

                flag = true;
                continue;
            }

            result.ExpandToInclude(boundingBox);
        }

        return result.GetBoundingBox();
    }

    public static Float64BoundingBox3D Containing(IEnumerable<Float64BoundingBox3D> boundingBoxesList)
    {
        var result = new Float64BoundingBoxComposer3D();

        var flag = false;
        foreach (var boundingBox in boundingBoxesList)
        {
            if (!flag)
            {
                result = boundingBox.GetBoundingBoxComposer();

                flag = true;
                continue;
            }

            result.ExpandToInclude(boundingBox);
        }

        return result.GetBoundingBox();
    }

    public static Float64BoundingBox3D ContainingIntersection(Float64BoundingBox3D b1, Float64BoundingBox3D b2)
    {
        return new Float64BoundingBox3D(
            Math.Max(b1.MinX, b2.MinX),
            Math.Max(b1.MinY, b2.MinY),
            Math.Max(b1.MinZ, b2.MinZ),
            Math.Min(b1.MaxX, b2.MaxX),
            Math.Min(b1.MaxY, b2.MaxY),
            Math.Min(b1.MaxZ, b2.MaxZ)
        );
    }


    public static Float64BoundingBox3D Containing(IFloat64BoundedShape3D geometricObject)
    {
        return geometricObject.GetBoundingBox();
    }

    public static Float64BoundingBox3D Containing(params IFloat64BoundedShape3D[] geometricObjectsList)
    {
        var result = new Float64BoundingBoxComposer3D();

        var flag = false;
        foreach (var geometricObject in geometricObjectsList)
        {
            if (!flag)
            {
                result = geometricObject.GetBoundingBoxComposer();

                flag = true;
                continue;
            }

            result.ExpandToInclude(geometricObject.GetBoundingBox());
        }

        return result.GetBoundingBox();
    }

    public static Float64BoundingBox3D Containing<T>(IEnumerable<T> geometricObjectsList)
        where T : IFloat64BoundedShape3D
    {
        var result = new Float64BoundingBoxComposer3D();

        var flag = false;
        foreach (var geometricObject in geometricObjectsList)
        {
            if (!flag)
            {
                result = geometricObject.GetBoundingBoxComposer();

                flag = true;
                continue;
            }

            result.ExpandToInclude(geometricObject.GetBoundingBox());
        }

        return result.GetBoundingBox();
    }


    public int VSpaceDimensions
        => 3;

    public bool IsEmpty { get; }

    public LinFloat64Vector3D MinPoint { get; }

    public LinFloat64Vector3D MaxPoint { get; }

    public LinFloat64Vector3D Center { get; }

    public LinFloat64Vector3D Size { get; }

    public LinFloat64Vector3D HalfSize
        => LinFloat64Vector3D.Create(
            (MaxX - MinX) / 2,
            (MaxY - MinY) / 2,
            (MaxZ - MinZ) / 2
        );

    public LinFloat64Vector3D DiagonalVector
        => Size;


    public double MinX
        => MinPoint.X;

    public double MinY
        => MinPoint.Y;

    public double MinZ
        => MinPoint.Z;


    public double MaxX
        => MaxPoint.X;

    public double MaxY
        => MaxPoint.Y;

    public double MaxZ
        => MaxPoint.Z;


    public double CenterX
        => Center.X;

    public double CenterY
        => Center.Y;

    public double CenterZ
        => Center.Z;


    public double SizeX
        => Size.X;

    public double SizeY
        => Size.Y;

    public double SizeZ
        => Size.Z;


    public Float64ScalarRange RangeX
        => Float64ScalarRange.Create(MinX, MaxX);

    public Float64ScalarRange RangeY
        => Float64ScalarRange.Create(MinY, MaxY);

    public Float64ScalarRange RangeZ
        => Float64ScalarRange.Create(MinZ, MaxZ);


    public Float64BoundingBox2D RangeXy
        => Float64BoundingBox2D.Containing(MinX, MinY, MaxX, MaxY);

    public Float64BoundingBox2D RangeYz
        => Float64BoundingBox2D.Containing(MinY, MinZ, MaxY, MaxZ);

    public Float64BoundingBox2D RangeZx
        => Float64BoundingBox2D.Containing(MinZ, MinX, MaxZ, MaxX);


    public bool IntersectionTestsEnabled { get; set; } = true;

    public double DiagonalLength
    {
        get
        {
            var x = MaxX - MinX;
            var y = MaxY - MinY;
            var z = MaxZ - MinZ;

            return Math.Sqrt(x * x + y * y + z * z);
        }
    }

    public double SurfaceArea
    {
        get
        {
            var dx = MaxX - MinX;
            var dy = MaxY - MinY;
            var dz = MaxZ - MinZ;

            return 2.0d * (dx * dy + dy * dz + dz * dx);
        }
    }

    public double Volume =>
        (MaxX - MinX) *
        (MaxY - MinY) *
        (MaxZ - MinZ);


    private Float64BoundingBox3D()
    {
        IsEmpty = true;

        MinPoint = LinFloat64Vector3D.Zero;
        MaxPoint = LinFloat64Vector3D.Zero;
        Center = LinFloat64Vector3D.Zero;
        Size = LinFloat64Vector3D.Zero;

        Debug.Assert(IsValid());
    }

    private Float64BoundingBox3D(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
    {
        IsEmpty = false;

        MinPoint = LinFloat64Vector3D.Create(minX, minY, minZ);
        MaxPoint = LinFloat64Vector3D.Create(maxX, maxY, maxZ);
        Center = LinFloat64Vector3D.Create((maxX + minX) / 2, (maxY + minY) / 2, (maxZ + minZ) / 2);
        Size = LinFloat64Vector3D.Create(maxX - minX, maxY - minY, maxZ - minZ);

        Debug.Assert(IsValid());
    }


    public bool IsValid()
    {
        return MinPoint.IsValid() &&
               MinPoint.IsValid() &&
               MinX <= MaxX &&
               MinY <= MaxY &&
               MinZ <= MaxZ;
    }

    public Float64BoundingBox3D GetBoundingBox()
    {
        return this;
    }

    public Float64BoundingBoxComposer3D GetBoundingBoxComposer()
    {
        return new Float64BoundingBoxComposer3D(this);
    }

    public Float64BoundingSphere3D GetBoundingSphere()
    {
        var center = Center;
        var radius = DiagonalLength / 2;

        return new Float64BoundingSphere3D(
            center.X,
            center.Y,
            center.Z,
            radius
        );
    }

    public IFloat64BoundingSurface3D MapBoundingSurfaceUsing(IFloat64AffineMap3D affineMap)
    {
        var oldSide001 = LinFloat64Vector3D.Create(MaxX - MinX, 0, 0);
        var oldSide010 = LinFloat64Vector3D.Create(0, MaxY - MinY, 0);
        var oldSide100 = LinFloat64Vector3D.Create(0, 0, MaxZ - MinZ);

        var newSide001 = affineMap.MapVector(oldSide001);
        var newSide010 = affineMap.MapVector(oldSide010);
        var newSide100 = affineMap.MapVector(oldSide100);

        var newCorner000 = affineMap.MapPoint(MinPoint);
        var newCorner001 = newCorner000 + newSide001;
        var newCorner010 = newCorner000 + newSide010;
        var newCorner011 = newCorner001 + newSide010;
        var newCorner100 = newCorner000 + newSide100;
        var newCorner101 = newCorner100 + newSide001;
        var newCorner110 = newCorner100 + newSide010;
        var newCorner111 = newCorner110 + newSide001;

        var newBox = Containing(
            newCorner000,
            newCorner001,
            newCorner010,
            newCorner011,
            newCorner100,
            newCorner101,
            newCorner110,
            newCorner111
        );

        return newBox;
    }

    public Float64BoundingBox3D[,,] GetSubdivisions(int xDivisions, int yDivisions, int zDivisions)
    {
        var xLength = SizeX / xDivisions;
        var yLength = SizeY / yDivisions;
        var zLength = SizeZ / zDivisions;

        var minXValues =
            Enumerable
                .Range(0, xDivisions)
                .Select(i => i * xLength + MinX)
                .ToArray();

        var maxXValues = minXValues.Select(v => v + xLength).ToArray();

        var minYValues =
            Enumerable
                .Range(0, yDivisions)
                .Select(i => i * yLength + MinY)
                .ToArray();

        var maxYValues = minYValues.Select(v => v + yLength).ToArray();

        var minZValues =
            Enumerable
                .Range(0, zDivisions)
                .Select(i => i * zLength + MinZ)
                .ToArray();

        var maxZValues = minZValues.Select(v => v + zLength).ToArray();

        var divisions = new Float64BoundingBox3D[xDivisions, yDivisions, zDivisions];

        for (var ix = 0; ix < xDivisions; ix++)
            for (var iy = 0; iy < yDivisions; iy++)
                for (var iz = 0; iz < zDivisions; iz++)
                    divisions[ix, iy, iz] =
                        Containing(
                            minXValues[ix],
                            minYValues[iy],
                            minZValues[iz],
                            maxXValues[ix],
                            maxYValues[iy],
                            maxZValues[iz]
                        );

        return divisions;
    }


    public double DistanceToPoint(LinFloat64Vector3D point)
    {
        return Math.Sqrt(DistanceSquaredToPoint(point));
    }

    public double DistanceSquaredToPoint(LinFloat64Vector3D point)
    {
        var dx = Math.Max(0, Math.Max(MinX - point.X, point.X - MaxX));
        var dy = Math.Max(0, Math.Max(MinY - point.Y, point.Y - MaxY));
        var dz = Math.Max(0, Math.Max(MinZ - point.Z, point.Z - MaxZ));

        return dx * dx + dy * dy + dz * dz;
    }


    public bool ContainsPoint(LinFloat64Vector3D point)
    {
        return point.X >= MinX && point.X <= MaxX &&
               point.Y >= MinY && point.Y <= MaxY &&
               point.Z >= MinZ && point.Z <= MaxZ;
    }

    public bool ContainsPoint(LinFloat64Vector3D point, double tolerance)
    {
        return
            point.Item1 >= MinX - tolerance &&
            point.Item1 <= MaxX + tolerance &&
            point.Item2 >= MinY - tolerance &&
            point.Item2 <= MaxY + tolerance &&
            point.Item3 >= MinZ - tolerance &&
            point.Item3 <= MaxZ + tolerance;
    }

    public bool ContainsPoint(double pointX, double pointY, double pointZ, bool useMargins = false, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (useMargins)
            return
                pointX >= MinX - tolerance &&
                pointX <= MaxX + tolerance &&
                pointY >= MinY - tolerance &&
                pointY <= MaxY + tolerance &&
                pointZ >= MinZ - tolerance &&
                pointZ <= MaxZ + tolerance;

        return
            pointX >= MinX &&
            pointX <= MaxX &&
            pointY >= MinY &&
            pointY <= MaxY &&
            pointZ >= MinZ &&
            pointZ <= MaxZ;
    }

    public bool ContainsPointUpperExclusive(LinFloat64Vector3D point, bool useMargins = false, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (useMargins)
            return
                point.Item1 >= MinX - tolerance &&
                point.Item1 < MaxX + tolerance &&
                point.Item2 >= MinY - tolerance &&
                point.Item2 < MaxY + tolerance &&
                point.Item2 >= MinZ - tolerance &&
                point.Item2 < MaxZ + tolerance;

        return
            point.Item1 >= MinX &&
            point.Item1 < MaxX &&
            point.Item2 >= MinY &&
            point.Item2 < MaxY &&
            point.Item3 >= MinZ &&
            point.Item3 < MaxZ;
    }

    public bool ContainsBox(Float64BoundingBox3D box, bool useMargins = false, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (useMargins)
            return
                box.MinX >= MinX - tolerance &&
                box.MaxX <= MaxX + tolerance &&
                box.MinY >= MinY - tolerance &&
                box.MaxY <= MaxY + tolerance &&
                box.MinZ >= MinZ - tolerance &&
                box.MaxZ <= MaxZ + tolerance;

        return
            box.MinX >= MinX &&
            box.MaxX <= MaxX &&
            box.MinY >= MinY &&
            box.MaxY <= MaxY &&
            box.MinZ >= MinZ &&
            box.MaxZ <= MaxZ;
    }


    public bool IntersectsBox(Float64BoundingBox3D box)
    {
        return !(MaxX < box.MinX || MinX > box.MaxX ||
                 MaxY < box.MinY || MinY > box.MaxY ||
                 MaxZ < box.MinZ || MinZ > box.MaxZ);
    }

    public bool IntersectsBox(Float64BoundingBox3D box, double tolerance)
    {
        return
            box.MaxX >= MinX - tolerance &&
            box.MinX <= MaxX + tolerance &&
            box.MaxY >= MinY - tolerance &&
            box.MinY <= MaxY + tolerance &&
            box.MaxZ >= MinZ - tolerance &&
            box.MinZ <= MaxZ + tolerance;
    }

    public bool IntersectsRay(IFloat64Ray<LinFloat64Vector3D> ray)
    {
        var tMin = (MinX - ray.Origin.X) / ray.Direction.X;
        var tMax = (MaxX - ray.Origin.X) / ray.Direction.X;
        if (tMin > tMax) (tMin, tMax) = (tMax, tMin);

        var tyMin = (MinY - ray.Origin.Y) / ray.Direction.Y;
        var tyMax = (MaxY - ray.Origin.Y) / ray.Direction.Y;
        if (tyMin > tyMax) (tyMin, tyMax) = (tyMax, tyMin);

        if (tMin > tyMax || tyMin > tMax) return false;
        if (tyMin > tMin) tMin = tyMin;
        if (tyMax < tMax) tMax = tyMax;

        var tzMin = (MinZ - ray.Origin.Z) / ray.Direction.Z;
        var tzMax = (MaxZ - ray.Origin.Z) / ray.Direction.Z;
        if (tzMin > tzMax) (tzMin, tzMax) = (tzMax, tzMin);

        if (tMin > tzMax || tzMin > tMax) return false;
        return true;
    }

    public bool TryIntersectRay(IFloat64Ray<LinFloat64Vector3D> ray, out double tMin, out double tMax, double tolerance = Float64Utils.DefaultTolerance, double geometricTolerance = 1e-9)
    {
        tMin = double.NegativeInfinity;
        tMax = double.PositiveInfinity;

        var ox = ray.Origin.X;
        var oy = ray.Origin.Y;
        var oz = ray.Origin.Z;
        var dx = ray.Direction.X;
        var dy = ray.Direction.Y;
        var dz = ray.Direction.Z;

        // X axis
        if (Math.Abs(dx) < tolerance)
        {
            if (ox < MinX - geometricTolerance || ox > MaxX + geometricTolerance)
                return false;
        }
        else
        {
            var invDx = 1.0 / dx;
            var t0 = (MinX - ox) * invDx;
            var t1 = (MaxX - ox) * invDx;

            if (t0 > t1) (t0, t1) = (t1, t0);

            tMin = Math.Max(tMin, t0);
            tMax = Math.Min(tMax, t1);

            if (tMax < tMin)
                return false;
        }

        // Y axis
        if (Math.Abs(dy) < tolerance)
        {
            if (oy < MinY - geometricTolerance || oy > MaxY + geometricTolerance)
                return false;
        }
        else
        {
            var invDy = 1.0 / dy;
            var t0 = (MinY - oy) * invDy;
            var t1 = (MaxY - oy) * invDy;

            if (t0 > t1) (t0, t1) = (t1, t0);

            tMin = Math.Max(tMin, t0);
            tMax = Math.Min(tMax, t1);

            if (tMax < tMin)
                return false;
        }

        // Z axis
        if (Math.Abs(dz) < tolerance)
        {
            if (oz < MinZ - geometricTolerance || oz > MaxZ + geometricTolerance)
                return false;
        }
        else
        {
            var invDz = 1.0 / dz;
            var t0 = (MinZ - oz) * invDz;
            var t1 = (MaxZ - oz) * invDz;

            if (t0 > t1) (t0, t1) = (t1, t0);

            tMin = Math.Max(tMin, t0);
            tMax = Math.Min(tMax, t1);

            if (tMax < tMin)
                return false;
        }

        return true;
    }


    /// <summary>
    /// Tests if a line segment intersects with this bounding box.
    /// </summary>
    /// <param name="startEndPair"></param>
    /// <returns></returns>
    public bool IntersectsLineSegment(IPair<LinFloat64Vector3D> startEndPair)
    {
        return IntersectsLineSegment(startEndPair.Item1, startEndPair.Item2);
    }

    /// <summary>
    /// Tests if a line segment intersects with this 3D bounding box.
    /// </summary>
    /// <param name="start">Start point of the line segment</param>
    /// <param name="end">End point of the line segment</param>
    /// <returns>True if the line segment intersects with the bounding box, false otherwise</returns>
    public bool IntersectsLineSegment(LinFloat64Vector3D start, LinFloat64Vector3D end)
    {
        // Quick rejection: if both points are on the same side of any face, no intersection
        if (start.Item1 < MinX && end.Item1 < MinX || start.Item1 > MaxX && end.Item1 > MaxX ||
            start.Item2 < MinY && end.Item2 < MinY || start.Item2 > MaxY && end.Item2 > MaxY ||
            start.Item3 < MinZ && end.Item3 < MinZ || start.Item3 > MaxZ && end.Item3 > MaxZ)
            return false;

        // If either point is inside the box, there's an intersection
        if (ContainsPoint(start) || ContainsPoint(end))
            return true;

        // Use the slab method (Liang-Barsky algorithm adapted for AABB)
        // This is more efficient than checking all 6 faces individually
        var tMin = 0.0;
        var tMax = 1.0;

        var direction = LinFloat64Vector3D.Create(end.Item1 - start.Item1, end.Item2 - start.Item2, end.Item3 - start.Item3);

        // Check intersection with each pair of parallel planes
        if (!UpdateSlabIntersection(start.Item1, direction.Item1, MinX, MaxX, ref tMin, ref tMax))
            return false;

        if (!UpdateSlabIntersection(start.Item2, direction.Item2, MinY, MaxY, ref tMin, ref tMax))
            return false;

        if (!UpdateSlabIntersection(start.Item3, direction.Item3, MinZ, MaxZ, ref tMin, ref tMax))
            return false;

        // If we get here, there's an intersection
        return true;

        // Helper method for the slab method - updates intersection parameters for one axis.
        // origin: Origin coordinate on the axis
        // direction: Direction component on the axis
        // minBound: Minimum bound of the slab
        // maxBound: Maximum bound of the slab
        // tMin: Current minimum parameter (updated)
        // tMax: Current maximum parameter (updated)
        // returns true if intersection is possible, false if no intersection
        bool UpdateSlabIntersection(double axisOrigin, double axisDirection, double minBound, double maxBound, ref double tMin, ref double tMax)
        {
            if (Math.Abs(axisDirection) < 1e-10)
            {
                // Ray is parallel to the slab
                if (axisOrigin < minBound || axisOrigin > maxBound)
                    return false; // Outside the slab

                // Otherwise, it's inside or on the boundary - no change to tMin/tMax needed
                return true;
            }

            // Calculate intersection parameters with the two planes
            var t1 = (minBound - axisOrigin) / axisDirection;
            var t2 = (maxBound - axisOrigin) / axisDirection;

            // Ensure t1 is the "entering" intersection and t2 is the "exiting" intersection
            if (t1 > t2)
                (t1, t2) = (t2, t1);

            // Update the overall intersection range
            if (t1 > tMin) tMin = t1;
            if (t2 < tMax) tMax = t2;

            // If the range becomes invalid, no intersection
            return tMin <= tMax;
        }
    }


    public Float64LineSegment3D ClipLine(IFloat64Line3D line)
    {
        var tMin = double.NegativeInfinity;
        var tMax = double.PositiveInfinity;

        //Compute intersection parameters of ray with Y-Z slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionX;
            var tSlap1 = (MinX - line.OriginX) * invRayDir;
            var tSlap2 = (MaxX - line.OriginX) * invRayDir;

            // Update parametric interval from slab intersection t values
            if (tSlap1 > tSlap2)
            {
                (tSlap1, tSlap2) = (tSlap2, tSlap1);
            }

            // Update tFar to ensure robust ray-bounds intersection
            tSlap2 *= 1 + 2 * Float64Utils.Geomma3;
            tMin = tSlap1 > tMin ? tSlap1 : tMin;
            tMax = tSlap2 < tMax ? tSlap2 : tMax;

            if (tMin > tMax)
                return null!;
        }

        //Compute intersection parameters of ray with X-Z slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionY;
            var tSlap1 = (MinY - line.OriginY) * invRayDir;
            var tSlap2 = (MaxY - line.OriginY) * invRayDir;

            // Update parametric interval from slab intersection t values
            if (tSlap1 > tSlap2)
            {
                (tSlap1, tSlap2) = (tSlap2, tSlap1);
            }

            // Update tFar to ensure robust ray-bounds intersection
            tSlap2 *= 1 + 2 * Float64Utils.Geomma3;
            tMin = tSlap1 > tMin ? tSlap1 : tMin;
            tMax = tSlap2 < tMax ? tSlap2 : tMax;

            if (tMin > tMax)
                return null!;
        }

        //Compute intersection parameters of ray with X-Y slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionZ;
            var tSlap1 = (MinZ - line.OriginZ) * invRayDir;
            var tSlap2 = (MaxZ - line.OriginZ) * invRayDir;

            // Update parametric interval from slab intersection t values
            if (tSlap1 > tSlap2)
            {
                (tSlap1, tSlap2) = (tSlap2, tSlap1);
            }

            // Update tFar to ensure robust ray-bounds intersection
            tSlap2 *= 1 + 2 * Float64Utils.Geomma3;
            tMin = tSlap1 > tMin ? tSlap1 : tMin;
            tMax = tSlap2 < tMax ? tSlap2 : tMax;

            if (tMin > tMax)
                return null!;
        }

        return new Float64LineSegment3D(
            line.OriginX + tMin * line.DirectionX,
            line.OriginY + tMin * line.DirectionY,
            line.OriginZ + tMin * line.DirectionZ,
            line.OriginX + tMax * line.DirectionX,
            line.OriginY + tMax * line.DirectionY,
            line.OriginZ + tMax * line.DirectionZ
        );
    }

    public Float64LineSegment3D ClipLine(IFloat64Line3D line, double lineParamMinValue, double lineParamMaxValue = double.PositiveInfinity)
    {
        var tMin = lineParamMinValue;
        var tMax = lineParamMaxValue;

        //Compute intersection parameters of ray with Y-Z slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionX;
            var tSlap1 = (MinX - line.OriginX) * invRayDir;
            var tSlap2 = (MaxX - line.OriginX) * invRayDir;

            // Update parametric interval from slab intersection t values
            if (tSlap1 > tSlap2)
            {
                (tSlap1, tSlap2) = (tSlap2, tSlap1);
            }

            // Update tFar to ensure robust ray-bounds intersection
            tSlap2 *= 1 + 2 * Float64Utils.Geomma3;
            tMin = tSlap1 > tMin ? tSlap1 : tMin;
            tMax = tSlap2 < tMax ? tSlap2 : tMax;

            if (tMin > tMax)
                return null!;
        }

        //Compute intersection parameters of ray with X-Z slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionY;
            var tSlap1 = (MinY - line.OriginY) * invRayDir;
            var tSlap2 = (MaxY - line.OriginY) * invRayDir;

            // Update parametric interval from slab intersection t values
            if (tSlap1 > tSlap2)
            {
                (tSlap1, tSlap2) = (tSlap2, tSlap1);
            }

            // Update tFar to ensure robust ray-bounds intersection
            tSlap2 *= 1 + 2 * Float64Utils.Geomma3;
            tMin = tSlap1 > tMin ? tSlap1 : tMin;
            tMax = tSlap2 < tMax ? tSlap2 : tMax;

            if (tMin > tMax)
                return null!;
        }

        //Compute intersection parameters of ray with X-Y slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionZ;
            var tSlap1 = (MinZ - line.OriginZ) * invRayDir;
            var tSlap2 = (MaxZ - line.OriginZ) * invRayDir;

            // Update parametric interval from slab intersection t values
            if (tSlap1 > tSlap2)
            {
                (tSlap1, tSlap2) = (tSlap2, tSlap1);
            }

            // Update tFar to ensure robust ray-bounds intersection
            tSlap2 *= 1 + 2 * Float64Utils.Geomma3;
            tMin = tSlap1 > tMin ? tSlap1 : tMin;
            tMax = tSlap2 < tMax ? tSlap2 : tMax;

            if (tMin > tMax)
                return null!;
        }

        return new Float64LineSegment3D(
            line.OriginX + tMin * line.DirectionX,
            line.OriginY + tMin * line.DirectionY,
            line.OriginZ + tMin * line.DirectionZ,
            line.OriginX + tMax * line.DirectionX,
            line.OriginY + tMax * line.DirectionY,
            line.OriginZ + tMax * line.DirectionZ
        );
    }


    public LinFloat64Vector3D GetPointAt(double tx, double ty, double tz)
    {
        return LinFloat64Vector3D.Create(
            (1.0d - tx) * MinX + tx * MaxX,
            (1.0d - ty) * MinY + ty * MaxY,
            (1.0d - tz) * MinZ + tz * MaxZ
        );
    }

    public LinFloat64Vector3D GetPointAt(LinFloat64Vector3D tVector)
    {
        return LinFloat64Vector3D.Create(
            (1.0d - tVector.Item1) * MinX + tVector.Item1 * MaxX,
            (1.0d - tVector.Item2) * MinY + tVector.Item2 * MaxY,
            (1.0d - tVector.Item3) * MinZ + tVector.Item3 * MaxZ
        );
    }

    public LinFloat64Vector3D GetPointOffset(LinFloat64Vector3D point)
    {
        var oX = point.Item1 - MinX;
        var oY = point.Item2 - MinY;
        var oZ = point.Item3 - MinZ;

        if (MaxX > MinX)
            oX /= MaxX - MinX;

        if (MaxY > MinY)
            oY /= MaxY - MinY;

        if (MaxZ > MinZ)
            oZ /= MaxZ - MinZ;

        return LinFloat64Vector3D.Create(oX, oY, oZ);
    }


    public int GetLongestSideIndex()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        return lengthX >= lengthY
            ? lengthX >= lengthZ ? 0 : 2
            : lengthY >= lengthZ ? 1 : 2;
    }

    public double GetLongestSideMinValue()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        return lengthX >= lengthY
            ? lengthX >= lengthZ ? MinX : MinZ
            : lengthY >= lengthZ ? MinY : MinZ;
    }

    public double GetLongestSideMaxValue()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        return lengthX >= lengthY
            ? lengthX >= lengthZ ? MaxX : MaxZ
            : lengthY >= lengthZ ? MaxY : MaxZ;
    }

    public double GetLongestSideMidValue()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        return lengthX >= lengthY
            ? lengthX >= lengthZ ? CenterX : CenterZ
            : lengthY >= lengthZ ? CenterY : CenterZ;
    }

    public double GetLongestSideLength()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        return lengthX >= lengthY
            ? lengthX >= lengthZ ? lengthX : lengthZ
            : lengthY >= lengthZ ? lengthY : lengthZ;
    }

    public Float64ScalarRange GetLongestSideRange()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        var axisIndex = lengthX >= lengthY
            ? lengthX >= lengthZ ? 0 : 2
            : lengthY >= lengthZ ? 1 : 2;

        if (axisIndex == 0)
            return Float64ScalarRange.Create(MinX, MaxX);

        if (axisIndex == 1)
            return Float64ScalarRange.Create(MinY, MaxY);

        return Float64ScalarRange.Create(MinZ, MaxZ);
    }


    public int GetShortestSideIndex()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        return lengthX <= lengthY
            ? lengthX <= lengthZ ? 0 : 2
            : lengthY <= lengthZ ? 1 : 2;
    }

    public double GetShortestSideMinValue()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        return lengthX <= lengthY
            ? lengthX <= lengthZ ? MinX : MinZ
            : lengthY <= lengthZ ? MinY : MinZ;
    }

    public double GetShortestSideMaxValue()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        return lengthX <= lengthY
            ? lengthX <= lengthZ ? MaxX : MaxZ
            : lengthY <= lengthZ ? MaxY : MaxZ;
    }

    public double GetShortestSideMidValue()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        return lengthX <= lengthY
            ? lengthX <= lengthZ ? CenterX : CenterZ
            : lengthY <= lengthZ ? CenterY : CenterZ;
    }

    public double GetShortestSideLength()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        return lengthX <= lengthY
            ? lengthX <= lengthZ ? lengthX : lengthZ
            : lengthY <= lengthZ ? lengthY : lengthZ;
    }

    public Float64ScalarRange GetShortestSideRange()
    {
        var lengthX = MaxX - MinX;
        var lengthY = MaxY - MinY;
        var lengthZ = MaxZ - MinZ;

        var axisIndex = lengthX <= lengthY
            ? lengthX <= lengthZ ? 0 : 2
            : lengthY <= lengthZ ? 1 : 2;

        if (axisIndex == 0)
            return Float64ScalarRange.Create(MinX, MaxX);

        if (axisIndex == 1)
            return Float64ScalarRange.Create(MinY, MaxY);

        return Float64ScalarRange.Create(MinZ, MaxZ);
    }


    /// <summary>
    /// Given an index between 0 and 7 this returns one corner of the box
    /// </summary>
    /// <param name="cornerIndex"></param>
    /// <returns></returns>
    public LinFloat64Vector3D GetCorner(int cornerIndex)
    {
        cornerIndex = cornerIndex.Mod(8);

        return cornerIndex switch
        {
            0 => LinFloat64Vector3D.Create(MinX, MinY, MinZ),
            1 => LinFloat64Vector3D.Create(MaxX, MinY, MinZ),
            2 => LinFloat64Vector3D.Create(MinX, MaxY, MinZ),
            3 => LinFloat64Vector3D.Create(MaxX, MaxY, MinZ),
            4 => LinFloat64Vector3D.Create(MinX, MinY, MaxZ),
            5 => LinFloat64Vector3D.Create(MaxX, MinY, MaxZ),
            6 => LinFloat64Vector3D.Create(MinX, MaxY, MaxZ),
            _ => LinFloat64Vector3D.Create(MaxX, MaxY, MaxZ)
        };
    }

    public double GetSideMinValue(int axisIndex)
    {
        axisIndex = axisIndex.Mod(3);

        return axisIndex switch
        {
            0 => MinX,
            1 => MinY,
            _ => MinZ
        };
    }

    public double GetSideMaxValue(int axisIndex)
    {
        axisIndex = axisIndex.Mod(3);

        return axisIndex switch
        {
            0 => MaxX,
            1 => MaxY,
            _ => MaxZ
        };
    }

    public double GetSideMidValue(int axisIndex)
    {
        axisIndex = axisIndex.Mod(3);

        return axisIndex switch
        {
            0 => 0.5 * (MinX + MaxX),
            1 => 0.5 * (MinY + MaxY),
            _ => 0.5 * (MinZ + MaxZ)
        };
    }

    public double GetSideLength(int axisIndex)
    {
        axisIndex = axisIndex.Mod(3);

        return axisIndex switch
        {
            0 => MaxX - MinX,
            1 => MaxY - MinY,
            _ => MaxZ - MinZ
        };
    }

    public Float64ScalarRange GetSideRange(int axisIndex)
    {
        axisIndex = axisIndex.Mod(3);

        return axisIndex switch
        {
            0 => Float64ScalarRange.Create(MinX, MaxX),
            1 => Float64ScalarRange.Create(MinY, MaxY),
            _ => Float64ScalarRange.Create(MinZ, MaxZ)
        };
    }

    public LinFloat64Vector3D GetSideDirection(int cornerIndex1, int cornerIndex2)
    {
        var point1 = GetCorner(cornerIndex1);
        var point2 = GetCorner(cornerIndex2);

        return LinFloat64Vector3D.Create(
            point2.Item1 - point1.Item1,
            point2.Item2 - point1.Item2,
            point2.Item3 - point1.Item3
        );
    }


    public IEnumerable<double> GetComponents(bool byCorner = true)
    {
        if (byCorner)
        {
            yield return MinX;
            yield return MinY;
            yield return MinZ;

            yield return MaxX;
            yield return MaxY;
            yield return MaxZ;
        }
        else
        {
            yield return MinX;
            yield return MaxX;

            yield return MinY;
            yield return MaxY;

            yield return MinZ;
            yield return MaxZ;
        }
    }


    public override string ToString()
    {
        return new StringBuilder()
            .Append("AABB: ")
            .Append(MinPoint.ToTupleString())
            .Append(" -> ")
            .Append(MaxPoint.ToTupleString())
            .ToString();
    }
}