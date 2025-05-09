using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;

public static class Float64BorderUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingBox2D GetBoundingBox(this IEnumerable<ILinFloat64Vector2D> pointsList)
    {
        return Float64BoundingBox2D.CreateFromPoints(pointsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingBox2D GetBoundingBox(this IEnumerable<ILinFloat64Vector2D> pointsList, double scalingFactor)
    {
        return Float64BoundingBox2D.CreateFromPoints(pointsList, scalingFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingCircle2D GetBoundingSphere(this IEnumerable<ILinFloat64Vector2D> pointsList)
    {
        return Float64BoundingCircle2D.CreateFromPoints(pointsList);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingBox3D GetBoundingBox(this IEnumerable<ILinFloat64Vector3D> pointsList)
    {
        return Float64BoundingBox3D.CreateFromPoints(pointsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64BoundingBox3D GetBoundingBox(this IEnumerable<ILinFloat64Vector3D> pointsList, double scalingFactor)
    {
        return Float64BoundingBox3D.CreateFromPoints(pointsList, scalingFactor);
    }



    public static LinFloat64Vector2D GetCorner(this IFloat64BoundingBox2D boundingBox, bool maxCorner)
    {
        return maxCorner
            ? LinFloat64Vector2D.Create((Float64Scalar)boundingBox.MaxX, (Float64Scalar)boundingBox.MaxY)
            : LinFloat64Vector2D.Create((Float64Scalar)boundingBox.MinX, (Float64Scalar)boundingBox.MinY);
    }

    public static double GetMidX(this IFloat64BoundingBox2D boundingBox)
    {
        return 0.5 * (boundingBox.MinX + boundingBox.MaxX);
    }

    public static double GetMidY(this IFloat64BoundingBox2D boundingBox)
    {
        return 0.5 * (boundingBox.MinY + boundingBox.MaxY);
    }

    public static double GetLengthX(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX;
    }

    public static double GetLengthY(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxY - boundingBox.MinY;
    }

    public static Float64ScalarRange GetRangeX(this IFloat64BoundingBox2D boundingBox)
    {
        return Float64ScalarRange.Create(boundingBox.MinX, boundingBox.MaxX);
    }

    public static Float64ScalarRange GetRangeY(this IFloat64BoundingBox2D boundingBox)
    {
        return Float64ScalarRange.Create(boundingBox.MinY, boundingBox.MaxY);
    }

    public static int GetLongestSideIndex(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX >= boundingBox.MaxY - boundingBox.MinY
            ? 0
            : 1;
    }

    public static double GetLongestSideMinValue(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX >= boundingBox.MaxY - boundingBox.MinY
            ? boundingBox.MinX
            : boundingBox.MinY;
    }

    public static double GetLongestSideMaxValue(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX >= boundingBox.MaxY - boundingBox.MinY
            ? boundingBox.MaxX
            : boundingBox.MaxY;
    }

    public static double GetLongestSideMidValue(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX >= boundingBox.MaxY - boundingBox.MinY
            ? 0.5 * (boundingBox.MinX + boundingBox.MaxX)
            : 0.5 * (boundingBox.MinY + boundingBox.MaxY);
    }

    public static double GetLongestSideLength(this IFloat64BoundingBox2D boundingBox)
    {
        return Math.Max(
            boundingBox.MaxX - boundingBox.MinX,
            boundingBox.MaxY - boundingBox.MinY
        );
    }

    public static Float64ScalarRange GetLongestSideRange(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX >= boundingBox.MaxY - boundingBox.MinY
            ? Float64ScalarRange.Create(boundingBox.MinX, boundingBox.MaxX)
            : Float64ScalarRange.Create(boundingBox.MinY, boundingBox.MaxY);
    }

    public static double GetShortestSideMinValue(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX <= boundingBox.MaxY - boundingBox.MinY
            ? boundingBox.MinX
            : boundingBox.MinY;
    }

    public static double GetShortestSideMaxValue(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX <= boundingBox.MaxY - boundingBox.MinY
            ? boundingBox.MaxX
            : boundingBox.MaxY;
    }

    public static double GetShortestSideMidValue(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX <= boundingBox.MaxY - boundingBox.MinY
            ? 0.5 * (boundingBox.MinX + boundingBox.MaxX)
            : 0.5 * (boundingBox.MinY + boundingBox.MaxY);
    }

    public static double GetShortestSideLength(this IFloat64BoundingBox2D boundingBox)
    {
        return Math.Min(
            boundingBox.MaxX - boundingBox.MinX,
            boundingBox.MaxY - boundingBox.MinY
        );
    }

    public static Float64ScalarRange GetShortestSideRange(this IFloat64BoundingBox2D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX <= boundingBox.MaxY - boundingBox.MinY
            ? Float64ScalarRange.Create(boundingBox.MinX, boundingBox.MaxX)
            : Float64ScalarRange.Create(boundingBox.MinY, boundingBox.MaxY);
    }

    public static LinFloat64Vector2D GetMinCorner(this IFloat64BoundingBox2D boundingBox)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)boundingBox.MinX, (Float64Scalar)boundingBox.MinY);
    }

    public static LinFloat64Vector2D GetMaxCorner(this IFloat64BoundingBox2D boundingBox)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)boundingBox.MaxX, (Float64Scalar)boundingBox.MaxY);
    }

    public static LinFloat64Vector2D GetMidPoint(this IFloat64BoundingBox2D boundingBox)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(0.5 * (boundingBox.MinX + boundingBox.MaxX)),
            (Float64Scalar)(0.5 * (boundingBox.MinY + boundingBox.MaxY)));
    }

    public static LinFloat64Vector2D GetSideLengths(this IFloat64BoundingBox2D boundingBox)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(boundingBox.MaxX - boundingBox.MinX),
            (Float64Scalar)(boundingBox.MaxY - boundingBox.MinY));
    }

    public static LinFloat64Vector2D GetDiagonalVector(this IFloat64BoundingBox2D boundingBox)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(boundingBox.MaxX - boundingBox.MinX),
            (Float64Scalar)(boundingBox.MaxY - boundingBox.MinY));
    }

    public static double GetDiagonalLength(this IFloat64BoundingBox2D boundingBox)
    {
        var x = boundingBox.MaxX - boundingBox.MinX;
        var y = boundingBox.MaxY - boundingBox.MinY;

        return Math.Sqrt(x * x + y * y);
    }

    public static double GetBorderLength(this IFloat64BoundingBox2D boundingBox)
    {
        var dx = boundingBox.MaxX - boundingBox.MinX;
        var dy = boundingBox.MaxY - boundingBox.MinY;

        return 2.0d * (dx + dy);
    }

    public static double GetArea(this IFloat64BoundingBox2D boundingBox)
    {
        return (boundingBox.MaxX - boundingBox.MinX) *
               (boundingBox.MaxY - boundingBox.MinY);
    }

    public static Float64BoundingCircle2D GetBoundingSphere(this IFloat64BoundingBox2D boundingBox)
    {
        var cX = 0.5 * (boundingBox.MinX + boundingBox.MaxX);
        var cY = 0.5 * (boundingBox.MinY + boundingBox.MaxY);
        var r = 0.0d;

        if (
            cX >= boundingBox.MinX && cX <= boundingBox.MaxX &&
            cY >= boundingBox.MinY && cY <= boundingBox.MaxY
        )
            r = Math.Sqrt(
                (boundingBox.MaxX - cX) * (boundingBox.MaxX - cX) +
                (boundingBox.MaxY - cY) * (boundingBox.MaxY - cY)
            );

        return Float64BoundingCircle2D.Create(cX, cY, r);
    }

    /// <summary>
    /// Given an index between 0 and 3 this returnes one corner of the box
    /// </summary>
    /// <param name="boundingBox"></param>
    /// <param name="cornerIndex"></param>
    /// <returns></returns>
    public static LinFloat64Vector2D GetCorner(this IFloat64BoundingBox2D boundingBox, int cornerIndex)
    {
        cornerIndex = cornerIndex.Mod(4);

        if (cornerIndex == 0)
            return LinFloat64Vector2D.Create((Float64Scalar)boundingBox.MinX, (Float64Scalar)boundingBox.MinY);

        if (cornerIndex == 1)
            return LinFloat64Vector2D.Create((Float64Scalar)boundingBox.MaxX, (Float64Scalar)boundingBox.MinY);

        if (cornerIndex == 2)
            return LinFloat64Vector2D.Create((Float64Scalar)boundingBox.MinX, (Float64Scalar)boundingBox.MaxY);

        return LinFloat64Vector2D.Create((Float64Scalar)boundingBox.MaxX, (Float64Scalar)boundingBox.MaxY);
    }

    public static double GetSideMinValue(this IFloat64BoundingBox2D boundingBox, int axisIndex)
    {
        axisIndex = axisIndex.Mod(2);

        if (axisIndex == 0)
            return boundingBox.MinX;

        return boundingBox.MinY;
    }

    public static double GetSideMaxValue(this IFloat64BoundingBox2D boundingBox, int axisIndex)
    {
        axisIndex = axisIndex.Mod(2);

        if (axisIndex == 0)
            return boundingBox.MaxX;

        return boundingBox.MaxY;
    }

    public static double GetSideMidValue(this IFloat64BoundingBox2D boundingBox, int axisIndex)
    {
        axisIndex = axisIndex.Mod(2);

        if (axisIndex == 0)
            return 0.5 * (boundingBox.MinX + boundingBox.MaxX);

        return 0.5 * (boundingBox.MinY + boundingBox.MaxY);
    }

    public static double GetSideLength(this IFloat64BoundingBox2D boundingBox, int axisIndex)
    {
        axisIndex = axisIndex.Mod(2);

        if (axisIndex == 0)
            return boundingBox.MaxX - boundingBox.MinX;

        return boundingBox.MaxY - boundingBox.MinY;
    }

    public static Float64ScalarRange GetSideRange(this IFloat64BoundingBox2D boundingBox, int axisIndex)
    {
        axisIndex = axisIndex.Mod(2);

        if (axisIndex == 0)
            return Float64ScalarRange.Create(boundingBox.MinX, boundingBox.MaxX);

        return Float64ScalarRange.Create(boundingBox.MinY, boundingBox.MaxY);
    }

    public static LinFloat64Vector2D GetSideDirection(this Float64BoundingBoxComposer2D boundingBox, int cornerIndex1, int cornerIndex2)
    {
        var point1 = boundingBox.GetCorner(cornerIndex1);
        var point2 = boundingBox.GetCorner(cornerIndex2);

        return LinFloat64Vector2D.Create(point2.X - point1.X,
            point2.Y - point1.Y);
    }

    public static LinFloat64Vector2D GetPointOffset(this IFloat64BoundingBox2D boundingBox, ILinFloat64Vector2D point)
    {
        var oX = point.X - boundingBox.MinX;
        var oY = point.Y - boundingBox.MinY;

        if (boundingBox.MaxX > boundingBox.MinX)
            oX = oX / (boundingBox.MaxX - boundingBox.MinX);

        if (boundingBox.MaxY > boundingBox.MinY)
            oY = oY / (boundingBox.MaxY - boundingBox.MinY);

        return LinFloat64Vector2D.Create(oX, oY);
    }

    public static bool Contains(this IFloat64BoundingBox2D boundingBox, ILinFloat64Vector2D point, bool useMargins = false, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (useMargins)
            return
                point.X >= boundingBox.MinX - zeroEpsilon &&
                point.X <= boundingBox.MaxX + zeroEpsilon &&
                point.Y >= boundingBox.MinY - zeroEpsilon &&
                point.Y <= boundingBox.MaxY + zeroEpsilon;

        return
            point.X >= boundingBox.MinX &&
            point.X <= boundingBox.MaxX &&
            point.Y >= boundingBox.MinY &&
            point.Y <= boundingBox.MaxY;
    }

    public static bool Contains(this IFloat64BoundingBox2D boundingBox, double pointX, double pointY, bool useMargins = false, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (useMargins)
            return
                pointX >= boundingBox.MinX - zeroEpsilon &&
                pointX <= boundingBox.MaxX + zeroEpsilon &&
                pointY >= boundingBox.MinY - zeroEpsilon &&
                pointY <= boundingBox.MaxY + zeroEpsilon;

        return
            pointX >= boundingBox.MinX &&
            pointX <= boundingBox.MaxX &&
            pointY >= boundingBox.MinY &&
            pointY <= boundingBox.MaxY;
    }

    public static bool Contains(this IFloat64BoundingBox2D boundingBox, IFloat64BoundingBox2D box, bool useMargins = false, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (useMargins)
            return
                box.MinX >= boundingBox.MinX - zeroEpsilon &&
                box.MaxX <= boundingBox.MaxX + zeroEpsilon &&
                box.MinY >= boundingBox.MinY - zeroEpsilon &&
                box.MaxY <= boundingBox.MaxY + zeroEpsilon;

        return
            box.MinX >= boundingBox.MinX &&
            box.MaxX <= boundingBox.MaxX &&
            box.MinY >= boundingBox.MinY &&
            box.MaxY <= boundingBox.MaxY;
    }

    public static bool ContainsUpperExclusive(this IFloat64BoundingBox2D boundingBox, ILinFloat64Vector2D point, bool useMargins = false, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (useMargins)
            return
                point.X >= boundingBox.MinX - zeroEpsilon &&
                point.X < boundingBox.MaxX + zeroEpsilon &&
                point.Y >= boundingBox.MinY - zeroEpsilon &&
                point.Y < boundingBox.MaxY + zeroEpsilon;

        return
            point.X >= boundingBox.MinX &&
            point.X < boundingBox.MaxX &&
            point.Y >= boundingBox.MinY &&
            point.Y < boundingBox.MaxY;
    }

    public static bool Overlaps(this IFloat64BoundingBox2D boundingBox, IFloat64BoundingBox2D box, bool useMargins = false, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (useMargins)
            return
                box.MaxX >= boundingBox.MinX - zeroEpsilon &&
                box.MinX <= boundingBox.MaxX + zeroEpsilon &&
                box.MaxY >= boundingBox.MinY - zeroEpsilon &&
                box.MinY <= boundingBox.MaxY + zeroEpsilon;

        return
            box.MaxX >= boundingBox.MinX &&
            box.MinX <= boundingBox.MaxX &&
            box.MaxY >= boundingBox.MinY &&
            box.MinY <= boundingBox.MaxY;
    }

    public static Float64BoundingBox2D[,] GetSubdivisions(this IFloat64BoundingBox2D boundingBox, int xDivisions, int yDivisions)
    {
        var xLength = boundingBox.GetLengthX() / xDivisions;
        var yLength = boundingBox.GetLengthY() / yDivisions;

        var minXValues =
            Enumerable
                .Range(0, xDivisions)
                .Select(i => i * xLength + boundingBox.MinX)
                .ToArray();

        var maxXValues =
            minXValues
                .Select(v => v + xLength)
                .ToArray();

        var minYValues =
            Enumerable
                .Range(0, yDivisions)
                .Select(i => i * yLength + boundingBox.MinY)
                .ToArray();

        var maxYValues =
            minYValues
                .Select(v => v + yLength)
                .ToArray();

        var divisions = new Float64BoundingBox2D[xDivisions, yDivisions];

        for (var ix = 0; ix < xDivisions; ix++)
            for (var iy = 0; iy < yDivisions; iy++)
                divisions[ix, iy] =
                    Float64BoundingBox2D.Create(
                        minXValues[ix],
                        minYValues[iy],
                        maxXValues[ix],
                        maxYValues[iy]
                    );

        return divisions;

    }

    public static Tuple<bool, Float64LineSegment2D> ClipLine(this IFloat64BoundingBox2D boundingBox, IFloat64Line2D line)
    {
        var tMin = double.NegativeInfinity;
        var tMax = double.PositiveInfinity;

        //Compute intersection parameters of ray with Y slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionX;
            var tSlap1 = (boundingBox.MinX - line.OriginX) * invRayDir;
            var tSlap2 = (boundingBox.MaxX - line.OriginX) * invRayDir;

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
                return new Tuple<bool, Float64LineSegment2D>(
                    false, 
                    Float64LineSegment2D.ZeroSegment
                );
        }

        //Compute intersection parameters of ray with X slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionY;
            var tSlap1 = (boundingBox.MinY - line.OriginY) * invRayDir;
            var tSlap2 = (boundingBox.MaxY - line.OriginY) * invRayDir;

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
                return new Tuple<bool, Float64LineSegment2D>(
                    false, 
                    Float64LineSegment2D.ZeroSegment
                );
        }

        return new Tuple<bool, Float64LineSegment2D>(
            true, 
            new Float64LineSegment2D(
                line.OriginX + tMin * line.DirectionX,
                line.OriginY + tMin * line.DirectionY,
                line.OriginX + tMax * line.DirectionX,
                line.OriginY + tMax * line.DirectionY
            )
        );
    }

    public static Tuple<bool, Float64LineSegment2D> ClipLine(this IFloat64BoundingBox2D boundingBox, IFloat64Line2D line, double lineParamMinValue, double lineParamMaxValue = double.PositiveInfinity)
    {
        var tMin = lineParamMinValue;
        var tMax = lineParamMaxValue;

        //Compute intersection parameters of ray with Y slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionX;
            var tSlap1 = (boundingBox.MinX - line.OriginX) * invRayDir;
            var tSlap2 = (boundingBox.MaxX - line.OriginX) * invRayDir;

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
                return new Tuple<bool, Float64LineSegment2D>(
                    false, 
                    Float64LineSegment2D.ZeroSegment
                );
        }

        //Compute intersection parameters of ray with X slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionY;
            var tSlap1 = (boundingBox.MinY - line.OriginY) * invRayDir;
            var tSlap2 = (boundingBox.MaxY - line.OriginY) * invRayDir;

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
                return new Tuple<bool, Float64LineSegment2D>(
                    false, 
                    Float64LineSegment2D.ZeroSegment
                );
        }

        return new Tuple<bool, Float64LineSegment2D>(
            true, 
            new Float64LineSegment2D(
                line.OriginX + tMin * line.DirectionX,
                line.OriginY + tMin * line.DirectionY,
                line.OriginX + tMax * line.DirectionX,
                line.OriginY + tMax * line.DirectionY
            )
        );
    }


    public static LinFloat64Vector3D GetCorner(this IFloat64BoundingBox3D boundingBox, bool maxCorner)
    {
        return maxCorner
            ? LinFloat64Vector3D.Create(boundingBox.MaxX, boundingBox.MaxY, boundingBox.MaxZ)
            : LinFloat64Vector3D.Create(boundingBox.MinX, boundingBox.MinY, boundingBox.MinZ);
    }

    public static double GetMidX(this IFloat64BoundingBox3D boundingBox)
    {
        return 0.5 * (boundingBox.MinX + boundingBox.MaxX);
    }

    public static double GetMidY(this IFloat64BoundingBox3D boundingBox)
    {
        return 0.5 * (boundingBox.MinY + boundingBox.MaxY);
    }

    public static double GetMidZ(this IFloat64BoundingBox3D boundingBox)
    {
        return 0.5 * (boundingBox.MinZ + boundingBox.MaxZ);
    }

    public static double GetLengthX(this IFloat64BoundingBox3D boundingBox)
    {
        return boundingBox.MaxX - boundingBox.MinX;
    }

    public static double GetLengthY(this IFloat64BoundingBox3D boundingBox)
    {
        return boundingBox.MaxY - boundingBox.MinY;
    }

    public static double GetLengthZ(this IFloat64BoundingBox3D boundingBox)
    {
        return boundingBox.MaxZ - boundingBox.MinZ;
    }

    public static Float64ScalarRange GetRangeX(this IFloat64BoundingBox3D boundingBox)
    {
        return Float64ScalarRange.Create(boundingBox.MinX, boundingBox.MaxX);
    }

    public static Float64ScalarRange GetRangeY(this IFloat64BoundingBox3D boundingBox)
    {
        return Float64ScalarRange.Create(boundingBox.MinY, boundingBox.MaxY);
    }

    public static Float64ScalarRange GetRangeZ(this IFloat64BoundingBox3D boundingBox)
    {
        return Float64ScalarRange.Create(boundingBox.MinZ, boundingBox.MaxZ);
    }

    public static Float64BoundingBox2D GetRangeXy(this IFloat64BoundingBox3D boundingBox)
    {
        return new Float64BoundingBox2D(
            boundingBox.MinX,
            boundingBox.MinY,
            boundingBox.MaxX,
            boundingBox.MaxY
        );
    }

    public static Float64BoundingBox2D GetRangeYz(this IFloat64BoundingBox3D boundingBox)
    {
        return new Float64BoundingBox2D(
            boundingBox.MinY,
            boundingBox.MinZ,
            boundingBox.MaxY,
            boundingBox.MaxZ
        );
    }

    public static Float64BoundingBox2D GetRangeZx(this IFloat64BoundingBox3D boundingBox)
    {
        return new Float64BoundingBox2D(
            boundingBox.MinZ,
            boundingBox.MinX,
            boundingBox.MaxZ,
            boundingBox.MaxX
        );
    }

    public static Float64BoundingBox2D GetBoundingBox2D<T>(this IEnumerable<T> objectsList)
        where T : IFloat64FiniteGeometricShape2D
    {
        var result = new Float64BoundingBoxComposer2D();

        var flag = false;
        foreach (var geometricObject in objectsList)
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

    public static Float64BoundingBox3D GetBoundingBox3D<T>(this IEnumerable<T> objectsList)
        where T : IFloat64FiniteGeometricShape3D
    {
        var result = new Float64BoundingBoxComposer3D();

        var flag = false;
        foreach (var geometricObject in objectsList)
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

    public static LinFloat64Vector2D GetPointAt(this IFloat64BoundingBox2D boundingBox, double tx, double ty)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)((1.0d - tx) * boundingBox.MinX + tx * boundingBox.MaxX),
            (Float64Scalar)((1.0d - ty) * boundingBox.MinY + ty * boundingBox.MaxY));
    }

    public static LinFloat64Vector2D GetPointAt(this IFloat64BoundingBox2D boundingBox, ILinFloat64Vector2D tVector)
    {
        return LinFloat64Vector2D.Create((1.0d - tVector.X) * boundingBox.MinX + tVector.X * boundingBox.MaxX,
            (1.0d - tVector.Y) * boundingBox.MinY + tVector.Y * boundingBox.MaxY);
    }


    public static int GetLongestSideIndex(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        return lengthX >= lengthY
            ? lengthX >= lengthZ ? 0 : 2
            : lengthY >= lengthZ ? 1 : 2;
    }

    public static double GetLongestSideMinValue(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        return lengthX >= lengthY
            ? lengthX >= lengthZ ? boundingBox.MinX : boundingBox.MinZ
            : lengthY >= lengthZ ? boundingBox.MinY : boundingBox.MinZ;
    }

    public static double GetLongestSideMaxValue(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        return lengthX >= lengthY
            ? lengthX >= lengthZ ? boundingBox.MaxX : boundingBox.MaxZ
            : lengthY >= lengthZ ? boundingBox.MaxY : boundingBox.MaxZ;
    }

    public static double GetLongestSideMidValue(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        return lengthX >= lengthY
            ? lengthX >= lengthZ ? boundingBox.GetMidX() : boundingBox.GetMidZ()
            : lengthY >= lengthZ ? boundingBox.GetMidY() : boundingBox.GetMidZ();
    }

    public static double GetLongestSideLength(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        return lengthX >= lengthY
            ? lengthX >= lengthZ ? lengthX : lengthZ
            : lengthY >= lengthZ ? lengthY : lengthZ;
    }

    public static Float64ScalarRange GetLongestSideRange(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        var axisIndex = lengthX >= lengthY
            ? lengthX >= lengthZ ? 0 : 2
            : lengthY >= lengthZ ? 1 : 2;

        if (axisIndex == 0)
            return Float64ScalarRange.Create(boundingBox.MinX, boundingBox.MaxX);

        if (axisIndex == 1)
            return Float64ScalarRange.Create(boundingBox.MinY, boundingBox.MaxY);

        return Float64ScalarRange.Create(boundingBox.MinZ, boundingBox.MaxZ);
    }

    public static int GetShortestSideIndex(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        return lengthX <= lengthY
            ? lengthX <= lengthZ ? 0 : 2
            : lengthY <= lengthZ ? 1 : 2;
    }

    public static double GetShortestSideMinValue(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        return lengthX <= lengthY
            ? lengthX <= lengthZ ? boundingBox.MinX : boundingBox.MinZ
            : lengthY <= lengthZ ? boundingBox.MinY : boundingBox.MinZ;
    }

    public static double GetShortestSideMaxValue(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        return lengthX <= lengthY
            ? lengthX <= lengthZ ? boundingBox.MaxX : boundingBox.MaxZ
            : lengthY <= lengthZ ? boundingBox.MaxY : boundingBox.MaxZ;
    }

    public static double GetShortestSideMidValue(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        return lengthX <= lengthY
            ? lengthX <= lengthZ ? boundingBox.GetMidX() : boundingBox.GetMidZ()
            : lengthY <= lengthZ ? boundingBox.GetMidY() : boundingBox.GetMidZ();
    }

    public static double GetShortestSideLength(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        return lengthX <= lengthY
            ? lengthX <= lengthZ ? lengthX : lengthZ
            : lengthY <= lengthZ ? lengthY : lengthZ;
    }

    public static Float64ScalarRange GetShortestSideRange(this IFloat64BoundingBox3D boundingBox)
    {
        var lengthX = boundingBox.MaxX - boundingBox.MinX;
        var lengthY = boundingBox.MaxY - boundingBox.MinY;
        var lengthZ = boundingBox.MaxZ - boundingBox.MinZ;

        var axisIndex = lengthX <= lengthY
            ? lengthX <= lengthZ ? 0 : 2
            : lengthY <= lengthZ ? 1 : 2;

        if (axisIndex == 0)
            return Float64ScalarRange.Create(boundingBox.MinX, boundingBox.MaxX);

        if (axisIndex == 1)
            return Float64ScalarRange.Create(boundingBox.MinY, boundingBox.MaxY);

        return Float64ScalarRange.Create(boundingBox.MinZ, boundingBox.MaxZ);
    }

    public static LinFloat64Vector3D GetMinCorner(this IFloat64BoundingBox3D boundingBox)
    {
        return LinFloat64Vector3D.Create(boundingBox.MinX,
            boundingBox.MinY,
            boundingBox.MinZ);
    }

    public static LinFloat64Vector3D GetMaxCorner(this IFloat64BoundingBox3D boundingBox)
    {
        return LinFloat64Vector3D.Create(boundingBox.MaxX,
            boundingBox.MaxY,
            boundingBox.MaxZ);
    }

    public static LinFloat64Vector3D GetMidPoint(this IFloat64BoundingBox3D boundingBox)
    {
        return LinFloat64Vector3D.Create(0.5 * (boundingBox.MinX + boundingBox.MaxX),
            0.5 * (boundingBox.MinY + boundingBox.MaxY),
            0.5 * (boundingBox.MinZ + boundingBox.MaxZ));
    }

    public static LinFloat64Vector3D GetSideLengths(this IFloat64BoundingBox3D boundingBox)
    {
        return LinFloat64Vector3D.Create(boundingBox.MaxX - boundingBox.MinX,
            boundingBox.MaxY - boundingBox.MinY,
            boundingBox.MaxZ - boundingBox.MinZ);
    }

    public static LinFloat64Vector3D GetSideHalfLengths(this IFloat64BoundingBox3D boundingBox)
    {
        return LinFloat64Vector3D.Create(0.5 * (boundingBox.MaxX - boundingBox.MinX),
            0.5 * (boundingBox.MaxY - boundingBox.MinY),
            0.5 * (boundingBox.MaxZ - boundingBox.MinZ));
    }

    public static LinFloat64Vector3D GetDiagonalVector(this IFloat64BoundingBox3D boundingBox)
    {
        return LinFloat64Vector3D.Create(boundingBox.MaxX - boundingBox.MinX,
            boundingBox.MaxY - boundingBox.MinY,
            boundingBox.MaxZ - boundingBox.MinZ);
    }

    public static double GetDiagonalLength(this IFloat64BoundingBox3D boundingBox)
    {
        var x = boundingBox.MaxX - boundingBox.MinX;
        var y = boundingBox.MaxY - boundingBox.MinY;
        var z = boundingBox.MaxZ - boundingBox.MinZ;

        return Math.Sqrt(x * x + y * y + z * z);
    }

    public static double GetSurfaceArea(this IFloat64BoundingBox3D boundingBox)
    {
        var dx = boundingBox.MaxX - boundingBox.MinX;
        var dy = boundingBox.MaxY - boundingBox.MinY;
        var dz = boundingBox.MaxZ - boundingBox.MinZ;

        return 2.0d * (dx * dy + dy * dz + dz * dx);
    }

    public static double GetVolume(this IFloat64BoundingBox3D boundingBox)
    {
        return (boundingBox.MaxX - boundingBox.MinX) *
               (boundingBox.MaxY - boundingBox.MinY) *
               (boundingBox.MaxZ - boundingBox.MinZ);
    }

    public static Float64BoundingSphere3D GetBoundingSphere(this IFloat64BoundingBox3D boundingBox)
    {
        var cX = 0.5 * (boundingBox.MinX + boundingBox.MaxX);
        var cY = 0.5 * (boundingBox.MinY + boundingBox.MaxY);
        var cZ = 0.5 * (boundingBox.MinZ + boundingBox.MaxZ);
        var r = 0.0d;

        if (
            cX >= boundingBox.MinX && cX <= boundingBox.MaxX &&
            cY >= boundingBox.MinY && cY <= boundingBox.MaxY &&
            cZ >= boundingBox.MinZ && cZ <= boundingBox.MaxZ
        )
            r = Math.Sqrt(
                (boundingBox.MaxX - cX) * (boundingBox.MaxX - cX) +
                (boundingBox.MaxY - cY) * (boundingBox.MaxY - cY) +
                (boundingBox.MaxZ - cZ) * (boundingBox.MaxZ - cZ)
            );

        return new Float64BoundingSphere3D(cX, cY, cZ, r);
    }

    /// <summary>
    /// Given an index between 0 and 7 this returnes one corner of the box
    /// </summary>
    /// <param name="boundingBox"></param>
    /// <param name="cornerIndex"></param>
    /// <returns></returns>
    public static LinFloat64Vector3D GetCorner(this IFloat64BoundingBox3D boundingBox, int cornerIndex)
    {
        cornerIndex = cornerIndex.Mod(8);

        if (cornerIndex == 0)
            return LinFloat64Vector3D.Create(boundingBox.MinX, boundingBox.MinY, boundingBox.MinZ);

        if (cornerIndex == 1)
            return LinFloat64Vector3D.Create(boundingBox.MaxX, boundingBox.MinY, boundingBox.MinZ);

        if (cornerIndex == 2)
            return LinFloat64Vector3D.Create(boundingBox.MinX, boundingBox.MaxY, boundingBox.MinZ);

        if (cornerIndex == 3)
            return LinFloat64Vector3D.Create(boundingBox.MaxX, boundingBox.MaxY, boundingBox.MinZ);

        if (cornerIndex == 4)
            return LinFloat64Vector3D.Create(boundingBox.MinX, boundingBox.MinY, boundingBox.MaxZ);

        if (cornerIndex == 5)
            return LinFloat64Vector3D.Create(boundingBox.MaxX, boundingBox.MinY, boundingBox.MaxZ);

        if (cornerIndex == 6)
            return LinFloat64Vector3D.Create(boundingBox.MinX, boundingBox.MaxY, boundingBox.MaxZ);

        return LinFloat64Vector3D.Create(boundingBox.MaxX, boundingBox.MaxY, boundingBox.MaxZ);
    }

    public static double GetSideMinValue(this IFloat64BoundingBox3D boundingBox, int axisIndex)
    {
        axisIndex = axisIndex.Mod(3);

        if (axisIndex == 0)
            return boundingBox.MinX;

        if (axisIndex == 1)
            return boundingBox.MinY;

        return boundingBox.MinZ;
    }

    public static double GetSideMaxValue(this IFloat64BoundingBox3D boundingBox, int axisIndex)
    {
        axisIndex = axisIndex.Mod(3);

        if (axisIndex == 0)
            return boundingBox.MaxX;

        if (axisIndex == 1)
            return boundingBox.MaxY;

        return boundingBox.MaxZ;
    }

    public static double GetSideMidValue(this IFloat64BoundingBox3D boundingBox, int axisIndex)
    {
        axisIndex = axisIndex.Mod(3);

        if (axisIndex == 0)
            return 0.5 * (boundingBox.MinX + boundingBox.MaxX);

        if (axisIndex == 1)
            return 0.5 * (boundingBox.MinY + boundingBox.MaxY);

        return 0.5 * (boundingBox.MinZ + boundingBox.MaxZ);
    }

    public static double GetSideLength(this IFloat64BoundingBox3D boundingBox, int axisIndex)
    {
        axisIndex = axisIndex.Mod(3);

        if (axisIndex == 0)
            return boundingBox.MaxX - boundingBox.MinX;

        if (axisIndex == 1)
            return boundingBox.MaxY - boundingBox.MinY;

        return boundingBox.MaxZ - boundingBox.MinZ;
    }

    public static Float64ScalarRange GetSideRange(this IFloat64BoundingBox3D boundingBox, int axisIndex)
    {
        axisIndex = axisIndex.Mod(3);

        if (axisIndex == 0)
            return Float64ScalarRange.Create(boundingBox.MinX, boundingBox.MaxX);

        if (axisIndex == 1)
            return Float64ScalarRange.Create(boundingBox.MinY, boundingBox.MaxY);

        return Float64ScalarRange.Create(boundingBox.MinZ, boundingBox.MaxZ);
    }

    public static LinFloat64Vector3D GetSideDirection(this IFloat64BoundingBox3D boundingBox, int cornerIndex1, int cornerIndex2)
    {
        var point1 = boundingBox.GetCorner(cornerIndex1);
        var point2 = boundingBox.GetCorner(cornerIndex2);

        return LinFloat64Vector3D.Create(point2.X - point1.X,
            point2.Y - point1.Y,
            point2.Z - point1.Z);
    }

    public static LinFloat64Vector3D GetPointOffset(this IFloat64BoundingBox3D boundingBox, ILinFloat64Vector3D point)
    {
        var oX = point.X - boundingBox.MinX;
        var oY = point.Y - boundingBox.MinY;
        var oZ = point.Z - boundingBox.MinZ;

        if (boundingBox.MaxX > boundingBox.MinX)
            oX = oX / (boundingBox.MaxX - boundingBox.MinX);

        if (boundingBox.MaxY > boundingBox.MinY)
            oY = oY / (boundingBox.MaxY - boundingBox.MinY);

        if (boundingBox.MaxZ > boundingBox.MinZ)
            oZ = oZ / (boundingBox.MaxZ - boundingBox.MinZ);

        return LinFloat64Vector3D.Create(oX, oY, oZ);
    }

    public static bool Contains(this IFloat64BoundingBox3D boundingBox, ILinFloat64Vector3D point, bool useMargins = false, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (useMargins)
            return
                point.X >= boundingBox.MinX - zeroEpsilon &&
                point.X <= boundingBox.MaxX + zeroEpsilon &&
                point.Y >= boundingBox.MinY - zeroEpsilon &&
                point.Y <= boundingBox.MaxY + zeroEpsilon &&
                point.Z >= boundingBox.MinZ - zeroEpsilon &&
                point.Z <= boundingBox.MaxZ + zeroEpsilon;

        return
            point.X >= boundingBox.MinX &&
            point.X <= boundingBox.MaxX &&
            point.Y >= boundingBox.MinY &&
            point.Y <= boundingBox.MaxY &&
            point.Z >= boundingBox.MinY &&
            point.Z <= boundingBox.MaxY;
    }

    public static bool Contains(this IFloat64BoundingBox3D boundingBox, double pointX, double pointY, double pointZ, bool useMargins = false, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (useMargins)
            return
                pointX >= boundingBox.MinX - zeroEpsilon &&
                pointX <= boundingBox.MaxX + zeroEpsilon &&
                pointY >= boundingBox.MinY - zeroEpsilon &&
                pointY <= boundingBox.MaxY + zeroEpsilon &&
                pointZ >= boundingBox.MinZ - zeroEpsilon &&
                pointZ <= boundingBox.MaxZ + zeroEpsilon;

        return
            pointX >= boundingBox.MinX &&
            pointX <= boundingBox.MaxX &&
            pointY >= boundingBox.MinY &&
            pointY <= boundingBox.MaxY &&
            pointZ >= boundingBox.MinZ &&
            pointZ <= boundingBox.MaxZ;
    }

    public static bool Contains(this IFloat64BoundingBox3D boundingBox, IFloat64BoundingBox3D box, bool useMargins = false, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (useMargins)
            return
                box.MinX >= boundingBox.MinX - zeroEpsilon &&
                box.MaxX <= boundingBox.MaxX + zeroEpsilon &&
                box.MinY >= boundingBox.MinY - zeroEpsilon &&
                box.MaxY <= boundingBox.MaxY + zeroEpsilon &&
                box.MinZ >= boundingBox.MinZ - zeroEpsilon &&
                box.MaxZ <= boundingBox.MaxZ + zeroEpsilon;

        return
            box.MinX >= boundingBox.MinX &&
            box.MaxX <= boundingBox.MaxX &&
            box.MinY >= boundingBox.MinY &&
            box.MaxY <= boundingBox.MaxY &&
            box.MinZ >= boundingBox.MinZ &&
            box.MaxZ <= boundingBox.MaxZ;
    }

    public static bool ContainsUpperExclusive(this IFloat64BoundingBox3D boundingBox, ILinFloat64Vector3D point, bool useMargins = false, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (useMargins)
            return
                point.X >= boundingBox.MinX - zeroEpsilon &&
                point.X < boundingBox.MaxX + zeroEpsilon &&
                point.Y >= boundingBox.MinY - zeroEpsilon &&
                point.Y < boundingBox.MaxY + zeroEpsilon &&
                point.Y >= boundingBox.MinZ - zeroEpsilon &&
                point.Y < boundingBox.MaxZ + zeroEpsilon;

        return
            point.X >= boundingBox.MinX &&
            point.X < boundingBox.MaxX &&
            point.Y >= boundingBox.MinY &&
            point.Y < boundingBox.MaxY &&
            point.Z >= boundingBox.MinZ &&
            point.Z < boundingBox.MaxZ;
    }

    public static bool Overlaps(this IFloat64BoundingBox3D boundingBox, IFloat64BoundingBox3D box, bool useMargins = false, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (useMargins)
            return
                box.MaxX >= boundingBox.MinX - zeroEpsilon &&
                box.MinX <= boundingBox.MaxX + zeroEpsilon &&
                box.MaxY >= boundingBox.MinY - zeroEpsilon &&
                box.MinY <= boundingBox.MaxY + zeroEpsilon &&
                box.MaxZ >= boundingBox.MinZ - zeroEpsilon &&
                box.MinZ <= boundingBox.MaxZ + zeroEpsilon;

        return
            box.MaxX >= boundingBox.MinX &&
            box.MinX <= boundingBox.MaxX &&
            box.MaxY >= boundingBox.MinY &&
            box.MinY <= boundingBox.MaxY &&
            box.MaxZ >= boundingBox.MinZ &&
            box.MinZ <= boundingBox.MaxZ;
    }

    public static Float64BoundingBox3D[,,] GetSubdivisions(this IFloat64BoundingBox3D boundingBox, int xDivisions, int yDivisions, int zDivisions)
    {
        var xLength = boundingBox.GetLengthX() / xDivisions;
        var yLength = boundingBox.GetLengthY() / yDivisions;
        var zLength = boundingBox.GetLengthZ() / zDivisions;

        var minXValues =
            Enumerable
                .Range(0, xDivisions)
                .Select(i => i * xLength + boundingBox.MinX)
                .ToArray();

        var maxXValues = minXValues.Select(v => v + xLength).ToArray();

        var minYValues =
            Enumerable
                .Range(0, yDivisions)
                .Select(i => i * yLength + boundingBox.MinY)
                .ToArray();

        var maxYValues = minYValues.Select(v => v + yLength).ToArray();

        var minZValues =
            Enumerable
                .Range(0, zDivisions)
                .Select(i => i * zLength + boundingBox.MinZ)
                .ToArray();

        var maxZValues = minZValues.Select(v => v + zLength).ToArray();

        var divisions = new Float64BoundingBox3D[xDivisions, yDivisions, zDivisions];

        for (var ix = 0; ix < xDivisions; ix++)
            for (var iy = 0; iy < yDivisions; iy++)
                for (var iz = 0; iz < zDivisions; iz++)
                    divisions[ix, iy, iz] =
                        Float64BoundingBox3D.CreateFromPoints(
                            minXValues[ix],
                            minYValues[iy],
                            minZValues[iz],
                            maxXValues[ix],
                            maxYValues[iy],
                            maxZValues[iz]
                        );

        return divisions;
    }

    public static Float64LineSegment3D ClipLine(this IFloat64BoundingBox3D boundingBox, IFloat64Line3D line)
    {
        var tMin = double.NegativeInfinity;
        var tMax = double.PositiveInfinity;

        //Compute intersection parameters of ray with Y-Z slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionX;
            var tSlap1 = (boundingBox.MinX - line.OriginX) * invRayDir;
            var tSlap2 = (boundingBox.MaxX - line.OriginX) * invRayDir;

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
                return null;
        }

        //Compute intersection parameters of ray with X-Z slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionY;
            var tSlap1 = (boundingBox.MinY - line.OriginY) * invRayDir;
            var tSlap2 = (boundingBox.MaxY - line.OriginY) * invRayDir;

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
                return null;
        }

        //Compute intersection parameters of ray with X-Y slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionZ;
            var tSlap1 = (boundingBox.MinZ - line.OriginZ) * invRayDir;
            var tSlap2 = (boundingBox.MaxZ - line.OriginZ) * invRayDir;

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
                return null;
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

    public static Float64LineSegment3D ClipLine(this IFloat64BoundingBox3D boundingBox, IFloat64Line3D line, double lineParamMinValue, double lineParamMaxValue = double.PositiveInfinity)
    {
        var tMin = lineParamMinValue;
        var tMax = lineParamMaxValue;

        //Compute intersection parameters of ray with Y-Z slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionX;
            var tSlap1 = (boundingBox.MinX - line.OriginX) * invRayDir;
            var tSlap2 = (boundingBox.MaxX - line.OriginX) * invRayDir;

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
                return null;
        }

        //Compute intersection parameters of ray with X-Z slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionY;
            var tSlap1 = (boundingBox.MinY - line.OriginY) * invRayDir;
            var tSlap2 = (boundingBox.MaxY - line.OriginY) * invRayDir;

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
                return null;
        }

        //Compute intersection parameters of ray with X-Y slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / line.DirectionZ;
            var tSlap1 = (boundingBox.MinZ - line.OriginZ) * invRayDir;
            var tSlap2 = (boundingBox.MaxZ - line.OriginZ) * invRayDir;

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
                return null;
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

    public static Float64BoundingBoxComposer2D GetMutableBoundingBox2D<T>(this IEnumerable<T> objectsList)
        where T : IFloat64FiniteGeometricShape2D
    {
        var result = new Float64BoundingBoxComposer2D();

        var flag = false;
        foreach (var geometricObject in objectsList)
        {
            if (!flag)
            {
                result = geometricObject.GetBoundingBoxComposer();

                flag = true;
                continue;
            }

            result.ExpandToInclude(geometricObject.GetBoundingBox());
        }

        return result;
    }

    public static Float64BoundingBoxComposer3D GetMutableBoundingBox3D<T>(this IEnumerable<T> objectsList)
        where T : IFloat64FiniteGeometricShape3D
    {
        var result = new Float64BoundingBoxComposer3D();

        var flag = false;
        foreach (var geometricObject in objectsList)
        {
            if (!flag)
            {
                result = geometricObject.GetBoundingBoxComposer();

                flag = true;
                continue;
            }

            result.ExpandToInclude(geometricObject.GetBoundingBox());
        }

        return result;
    }

    public static LinFloat64Vector3D GetPointAt(this IFloat64BoundingBox3D boundingBox, double tx, double ty, double tz)
    {
        return LinFloat64Vector3D.Create((1.0d - tx) * boundingBox.MinX + tx * boundingBox.MaxX,
            (1.0d - ty) * boundingBox.MinY + ty * boundingBox.MaxY,
            (1.0d - tz) * boundingBox.MinZ + tz * boundingBox.MaxZ);
    }

    public static LinFloat64Vector3D GetPointAt(this IFloat64BoundingBox3D boundingBox, ILinFloat64Vector3D tVector)
    {
        return LinFloat64Vector3D.Create((1.0d - tVector.X) * boundingBox.MinX + tVector.X * boundingBox.MaxX,
            (1.0d - tVector.Y) * boundingBox.MinY + tVector.Y * boundingBox.MaxY,
            (1.0d - tVector.Z) * boundingBox.MinZ + tVector.Z * boundingBox.MaxZ);
    }

    public static IEnumerable<double> GetComponents(this Float64ScalarRange boundingBox)
    {
        yield return boundingBox.MinValue;
        yield return boundingBox.MaxValue;
    }

    public static IEnumerable<double> GetComponents(this IFloat64BoundingBox2D boundingBox, bool byCorner = true)
    {
        if (byCorner)
        {
            yield return boundingBox.MinX;
            yield return boundingBox.MinY;

            yield return boundingBox.MaxX;
            yield return boundingBox.MaxY;
        }
        else
        {
            yield return boundingBox.MinX;
            yield return boundingBox.MaxX;

            yield return boundingBox.MinY;
            yield return boundingBox.MaxY;
        }
    }

    public static IEnumerable<double> GetComponents(this IFloat64BoundingBox3D boundingBox, bool byCorner = true)
    {
        if (byCorner)
        {
            yield return boundingBox.MinX;
            yield return boundingBox.MinY;
            yield return boundingBox.MinZ;

            yield return boundingBox.MaxX;
            yield return boundingBox.MaxY;
            yield return boundingBox.MaxZ;
        }
        else
        {
            yield return boundingBox.MinX;
            yield return boundingBox.MaxX;

            yield return boundingBox.MinY;
            yield return boundingBox.MaxY;

            yield return boundingBox.MinZ;
            yield return boundingBox.MaxZ;
        }
    }
}