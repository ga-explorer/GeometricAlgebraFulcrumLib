using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

public sealed record Float64BoundingBox2D :
    IFloat64BoundingBox<Float64BoundingBox2D, LinFloat64Vector2D>,
    IFloat64BoundingSurface2D
{
    public static Float64BoundingBox2D EmptyInstance { get; }
        = new Float64BoundingBox2D();

    public static Float64BoundingBox2D Empty(int vSpaceDimensions = 2)
        => EmptyInstance;

    public static Float64BoundingBox2D Infinite { get; }
        = new Float64BoundingBox2D(
            double.NegativeInfinity,
            double.NegativeInfinity,
            double.PositiveInfinity,
            double.PositiveInfinity
        );


    public static Float64BoundingBox2D InfiniteX(double y1, double y2)
    {
        return y1 >= y2
            ? new Float64BoundingBox2D(double.NegativeInfinity, y1, double.PositiveInfinity, y2)
            : new Float64BoundingBox2D(double.NegativeInfinity, y2, double.PositiveInfinity, y1);
    }

    public static Float64BoundingBox2D InfiniteY(double x1, double x2)
    {
        return x1 >= x2
            ? new Float64BoundingBox2D(x1, double.NegativeInfinity, x2, double.PositiveInfinity)
            : new Float64BoundingBox2D(x2, double.NegativeInfinity, x1, double.PositiveInfinity);
    }


    public static Float64BoundingBox2D Around(double centerX, double centerY, double deltaX, double deltaY)
    {
        var minX = centerX - deltaX;
        var maxX = centerX + deltaX;
        var minY = centerY - deltaY;
        var maxY = centerY + deltaY;

        if (deltaX < 0)
            (maxX, minX) = (minX, maxX);

        if (deltaY < 0)
            (maxY, minY) = (minY, maxY);

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }

    public static Float64BoundingBox2D Around(LinFloat64Vector2D center, double deltaX, double deltaY)
    {
        var minX = center.Item1 - deltaX;
        var maxX = center.Item1 + deltaX;
        var minY = center.Item2 - deltaY;
        var maxY = center.Item2 + deltaY;

        if (deltaX < 0)
            (maxX, minX) = (minX, maxX);

        if (deltaY < 0)
            (maxY, minY) = (minY, maxY);

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }


    public static Float64BoundingBox2D Containing(LinFloat64Vector2D point)
    {
        return new Float64BoundingBox2D(
            point.Item1,
            point.Item2,
            point.Item1,
            point.Item2
        );
    }

    public static Float64BoundingBox2D Containing(double point1X, double point1Y, double point2X, double point2Y)
    {
        double minX, minY, maxX, maxY;

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

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }

    public static Float64BoundingBox2D Containing(LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        double minX, minY, maxX, maxY;

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

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }

    public static Float64BoundingBox2D Containing(LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var minX = point1.Item1;
        var minY = point1.Item2;

        var maxX = point1.Item1;
        var maxY = point1.Item2;

        if (minX > point2.Item1) minX = point2.Item1;
        if (minX > point3.Item1) minX = point3.Item1;

        if (minY > point2.Item2) minY = point2.Item2;
        if (minY > point3.Item2) minY = point3.Item2;

        if (maxX < point2.Item1) maxX = point2.Item1;
        if (maxX < point3.Item1) maxX = point3.Item1;

        if (maxY < point2.Item2) maxY = point2.Item2;
        if (maxY < point3.Item2) maxY = point3.Item2;

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }

    public static Float64BoundingBox2D Containing(params LinFloat64Vector2D[] pointsList)
    {
        var point1 = pointsList[0];

        var minX = point1.Item1;
        var minY = point1.Item2;

        var maxX = point1.Item1;
        var maxY = point1.Item2;

        foreach (var point in pointsList.Skip(1))
        {
            if (minX > point.Item1) minX = point.Item1;
            if (minY > point.Item2) minY = point.Item2;
            if (maxX < point.Item1) maxX = point.Item1;
            if (maxY < point.Item2) maxY = point.Item2;
        }

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }

    public static Float64BoundingBox2D Containing(IEnumerable<LinFloat64Vector2D> pointsList)
    {
        double minX = 0, minY = 0, maxX = 0, maxY = 0;

        var flag = false;
        foreach (var point in pointsList)
        {
            if (!flag)
            {
                minX = point.Item1;
                minY = point.Item2;

                maxX = point.Item1;
                maxY = point.Item2;

                flag = true;
                continue;
            }

            if (minX > point.Item1) minX = point.Item1;
            if (minY > point.Item2) minY = point.Item2;
            if (maxX < point.Item1) maxX = point.Item1;
            if (maxY < point.Item2) maxY = point.Item2;
        }

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }

    public static Float64BoundingBox2D Containing(IEnumerable<LinFloat64Vector2D> pointsList, double updateFactor)
    {
        double minX = 0, minY = 0, maxX = 0, maxY = 0;

        var flag = false;
        foreach (var point in pointsList)
        {
            if (!flag)
            {
                minX = point.Item1;
                minY = point.Item2;

                maxX = point.Item1;
                maxY = point.Item2;

                flag = true;
                continue;
            }

            if (minX > point.Item1) minX = point.Item1;
            if (minY > point.Item2) minY = point.Item2;
            if (maxX < point.Item1) maxX = point.Item1;
            if (maxY < point.Item2) maxY = point.Item2;
        }

        var midX = 0.5d * (maxX + minX);
        var midY = 0.5d * (maxY + minY);

        minX = (minX - midX) * updateFactor + midX;
        minY = (minY - midY) * updateFactor + midY;

        maxX = (maxX - midX) * updateFactor + midX;
        maxY = (maxY - midY) * updateFactor + midY;

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }

    public static Float64BoundingBox2D ContainingPoints(IPair<LinFloat64Vector2D> s)
    {
        var minX = Math.Min(s.Item1.Item1, s.Item2.Item1);
        var maxX = Math.Max(s.Item1.Item1, s.Item2.Item1);

        var minY = Math.Min(s.Item1.Item2, s.Item2.Item2);
        var maxY = Math.Max(s.Item1.Item2, s.Item2.Item2);

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }

    public static Float64BoundingBox2D ContainingPoints(ITriplet<LinFloat64Vector2D> t)
    {
        var minX = Math.Min(t.Item1.Item1, Math.Min(t.Item2.Item1, t.Item3.Item1));
        var maxX = Math.Max(t.Item1.Item1, Math.Max(t.Item2.Item1, t.Item3.Item1));

        var minY = Math.Min(t.Item1.Item2, Math.Min(t.Item2.Item2, t.Item3.Item2));
        var maxY = Math.Max(t.Item1.Item2, Math.Max(t.Item2.Item2, t.Item3.Item2));

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }


    public static Float64BoundingBox2D Containing(Float64BoundingBox2D boundingBox)
    {
        return new Float64BoundingBox2D(boundingBox);
    }

    public static Float64BoundingBox2D Containing(Float64BoundingBox2D b1, Float64BoundingBox2D b2)
    {
        return new Float64BoundingBox2D(
            Math.Min(b1.MinX, b2.MinX),
            Math.Min(b1.MinY, b2.MinY),
            Math.Max(b1.MaxX, b2.MaxX),
            Math.Max(b1.MaxY, b2.MaxY)
        );
    }

    public static Float64BoundingBox2D Containing(params Float64BoundingBox2D[] boundingBoxesList)
    {
        var result = new Float64BoundingBoxComposer2D();

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

    public static Float64BoundingBox2D Containing(IEnumerable<Float64BoundingBox2D> boundingBoxesList)
    {
        var result = new Float64BoundingBoxComposer2D();

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

    public static Float64BoundingBox2D ContainingIntersection(Float64BoundingBox2D b1, Float64BoundingBox2D b2)
    {
        return new Float64BoundingBox2D(
            Math.Max(b1.MinX, b2.MinX),
            Math.Max(b1.MinY, b2.MinY),
            Math.Min(b1.MaxX, b2.MaxX),
            Math.Min(b1.MaxY, b2.MaxY)
        );
    }


    public static Float64BoundingBox2D Containing(IFloat64BoundedShape2D geometricObject)
    {
        return geometricObject.GetBoundingBox();
    }

    public static Float64BoundingBox2D Containing(params IFloat64BoundedShape2D[] geometricObjectsList)
    {
        var result = new Float64BoundingBoxComposer2D();

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

    public static Float64BoundingBox2D Containing<T>(IEnumerable<T> geometricObjectsList)
        where T : IFloat64BoundedShape2D
    {
        var result = new Float64BoundingBoxComposer2D();

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
        => 2;

    public bool IsEmpty { get; }

    public LinFloat64Vector2D MinPoint { get; }

    public LinFloat64Vector2D MaxPoint { get; }

    public LinFloat64Vector2D Center { get; }

    public LinFloat64Vector2D Size { get; }

    public LinFloat64Vector2D HalfSize
        => LinFloat64Vector2D.Create(
            (MaxX - MinX) / 2,
            (MaxY - MinY) / 2
        );

    public LinFloat64Vector2D DiagonalVector
        => Size;


    public double MinX
        => MinPoint.X;

    public double MinY
        => MinPoint.Y;


    public double MaxX
        => MaxPoint.X;

    public double MaxY
        => MaxPoint.Y;


    public double CenterX
        => Center.X;

    public double CenterY
        => Center.Y;


    public double SizeX
        => Size.X;

    public double SizeY
        => Size.Y;

    public double WidthToHeightRatio
        => Math.Abs(SizeX / SizeY);

    public double HeightToWidthRatio
        => Math.Abs(SizeY / SizeX);

    public Float64ScalarRange RangeX
        => Float64ScalarRange.Create(MinX, MaxX);

    public Float64ScalarRange RangeY
        => Float64ScalarRange.Create(MinY, MaxY);


    public bool IntersectionTestsEnabled { get; set; } = true;

    public double DiagonalLength
    {
        get
        {
            var x = MaxX - MinX;
            var y = MaxY - MinY;

            return Math.Sqrt(x * x + y * y);
        }
    }

    public double Perimeter
        => 2 * (SizeX + SizeY);

    public double Area
    {
        get
        {
            var dx = MaxX - MinX;
            var dy = MaxY - MinY;

            return dx * dy;
        }
    }


    private Float64BoundingBox2D()
    {
        IsEmpty = true;

        MinPoint = LinFloat64Vector2D.Zero;
        MaxPoint = LinFloat64Vector2D.Zero;
        Center = LinFloat64Vector2D.Zero;
        Size = LinFloat64Vector2D.Zero;

        Debug.Assert(IsValid());
    }

    private Float64BoundingBox2D(double minX, double minY, double maxX, double maxY)
    {
        IsEmpty = false;

        MinPoint = LinFloat64Vector2D.Create(minX, minY);
        MaxPoint = LinFloat64Vector2D.Create(maxX, maxY);
        Center = LinFloat64Vector2D.Create((maxX + minX) / 2, (maxY + minY) / 2);
        Size = LinFloat64Vector2D.Create(maxX - minX, maxY - minY);

        Debug.Assert(IsValid());
    }


    public bool IsValid()
    {
        return MinPoint.IsValid() &&
               MinPoint.IsValid() &&
               MinX <= MaxX &&
               MinY <= MaxY;
    }

    public Float64BoundingBox2D GetBoundingBox()
    {
        return this;
    }

    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        return new Float64BoundingBoxComposer2D(this);
    }

    public Float64BoundingCircle2D GetBoundingCircle()
    {
        var radius = DiagonalLength / 2;

        return new Float64BoundingCircle2D(
            Center.X,
            Center.Y,
            radius
        );
    }

    public IFloat64BoundingSurface2D MapUsing(IFloat64AffineMap2D affineMap)
    {
        var oldSide001 = LinFloat64Vector2D.Create(MaxX - MinX, 0);
        var oldSide010 = LinFloat64Vector2D.Create(0, MaxY - MinY);

        var newSide001 = affineMap.MapVector(oldSide001);
        var newSide010 = affineMap.MapVector(oldSide010);

        var newCorner000 = affineMap.MapPoint(MinPoint);
        var newCorner001 = newCorner000 + newSide001;
        var newCorner010 = newCorner000 + newSide010;
        var newCorner011 = newCorner001 + newSide010;

        var newBox = Containing(
            newCorner000,
            newCorner001,
            newCorner010,
            newCorner011
        );

        return newBox;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(LinFloat64Vector2D point)
    {
        return point.Item1 >= MinX && point.Item1 <= MaxX &&
               point.Item2 >= MinY && point.Item2 <= MaxY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double DistanceToPoint(LinFloat64Vector2D point)
    {
        return Math.Sqrt(
            DistanceToPointSquared(point.Item1, point.Item2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double DistanceToPoint(double x, double y)
    {
        return Math.Sqrt(
            DistanceToPointSquared(x, y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double DistanceToPointSquared(LinFloat64Vector2D point)
    {
        return DistanceToPointSquared(point.Item1, point.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double DistanceToPointSquared(double x, double y)
    {
        var dx = 0.0;

        if (x < MinX)
            dx = MinX - x;
        else if (x > MaxX)
            dx = x - MaxX;

        var dy = 0.0;
        if (y < MinY)
            dy = MinY - y;
        else if (y > MaxY)
            dy = y - MaxY;

        return dx * dx + dy * dy;
    }

    public bool IntersectsBox(Float64BoundingBox2D other)
    {
        if (MaxX < other.MinX || other.MaxX < MinX) return false;
        if (MaxY < other.MinY || other.MaxY < MinY) return false;

        return true;
    }

    /// <summary>
    /// Tests if a line segment intersects with this bounding box.
    /// </summary>
    /// <param name="startEndPair"></param>
    /// <returns></returns>
    public bool IntersectsLineSegment(IPair<LinFloat64Vector2D> startEndPair)
    {
        return IntersectsLineSegment(startEndPair.Item1, startEndPair.Item2);
    }

    /// <summary>
    /// Tests if a line segment intersects with this bounding box.
    /// </summary>
    /// <param name="start">Start point of the line segment</param>
    /// <param name="end">End point of the line segment</param>
    /// <returns>True if the line segment intersects with the bounding box, false otherwise</returns>
    public bool IntersectsLineSegment(LinFloat64Vector2D start, LinFloat64Vector2D end)
    {
        // Quick rejection: if both points are on the same side of any edge, no intersection
        if ((start.Item1 < MinX && end.Item1 < MinX) || (start.Item1 > MaxX && end.Item1 > MaxX) ||
            (start.Item2 < MinY && end.Item2 < MinY) || (start.Item2 > MaxY && end.Item2 > MaxY))
            return false;

        // If either point is inside the box, there's an intersection
        if (ContainsPoint(start) || ContainsPoint(end))
            return true;

        // Check intersection with each edge of the bounding box
        // Top edge (y = MaxY)
        if (LineSegmentIntersectsHorizontalEdge(start, end, MaxY, MinX, MaxX))
            return true;

        // Bottom edge (y = MinY)  
        if (LineSegmentIntersectsHorizontalEdge(start, end, MinY, MinX, MaxX))
            return true;

        // Left edge (x = MinX)
        if (LineSegmentIntersectsVerticalEdge(start, end, MinX, MinY, MaxY))
            return true;

        // Right edge (x = MaxX)
        if (LineSegmentIntersectsVerticalEdge(start, end, MaxX, MinY, MaxY))
            return true;

        return false;

        static bool LineSegmentIntersectsHorizontalEdge(LinFloat64Vector2D start, LinFloat64Vector2D end, double yEdge, double xMin, double xMax)
        {
            // If both points are on the same side of the horizontal edge, no intersection
            if ((start.Item2 < yEdge && end.Item2 < yEdge) || (start.Item2 > yEdge && end.Item2 > yEdge))
                return false;

            // Calculate the x-coordinate where the line crosses the horizontal edge
            if (Math.Abs(end.Item2 - start.Item2) < 1e-10) // Horizontal line
                return false; // Already handled by quick rejection or contains check

            var t = (yEdge - start.Item2) / (end.Item2 - start.Item2);

            // Check if intersection is within the line segment (0 <= t <= 1)
            if (t is < 0 or > 1)
                return false;

            // Calculate x coordinate of intersection
            var x = start.Item1 + t * (end.Item1 - start.Item1);

            // Check if x is within the edge bounds
            return x >= xMin && x <= xMax;
        }

        static bool LineSegmentIntersectsVerticalEdge(LinFloat64Vector2D start, LinFloat64Vector2D end, double xEdge, double yMin, double yMax)
        {
            // If both points are on the same side of the vertical edge, no intersection
            if ((start.Item1 < xEdge && end.Item1 < xEdge) || (start.Item1 > xEdge && end.Item1 > xEdge))
                return false;

            // Calculate the y-coordinate where the line crosses the vertical edge
            if (Math.Abs(end.Item1 - start.Item1) < 1e-10) // Vertical line
                return false; // Already handled by quick rejection or contains check

            var t = (xEdge - start.Item1) / (end.Item1 - start.Item1);

            // Check if intersection is within the line segment (0 <= t <= 1)
            if (t is < 0 or > 1)
                return false;

            // Calculate y coordinate of intersection
            var y = start.Item2 + t * (end.Item2 - start.Item2);

            // Check if y is within the edge bounds
            return y >= yMin && y <= yMax;
        }
    }


    public bool IntersectsRay(IFloat64Ray<LinFloat64Vector2D> ray)
    {
        // slab test
        var tMin = (MinX - ray.Origin.X) / ray.Direction.X;
        var tMax = (MaxX - ray.Origin.X) / ray.Direction.X;
        if (tMin > tMax) (tMin, tMax) = (tMax, tMin);

        var tyMin = (MinY - ray.Origin.Y) / ray.Direction.Y;
        var tyMax = (MaxY - ray.Origin.Y) / ray.Direction.Y;
        if (tyMin > tyMax) (tyMin, tyMax) = (tyMax, tyMin);

        if ((tMin > tyMax) || (tyMin > tMax)) return false;

        return true;
    }

    public double DistanceSquaredToPoint(LinFloat64Vector2D point)
    {
        var dx = Math.Max(0, Math.Max(MinX - point.X, point.X - MaxX));
        var dy = Math.Max(0, Math.Max(MinY - point.Y, point.Y - MaxY));

        return dx * dx + dy * dy;
    }


    /// <summary>
    /// Given an index between 0 and 3 this returns one corner of the box
    /// </summary>
    /// <param name="cornerIndex"></param>
    /// <returns></returns>
    public LinFloat64Vector2D GetCorner(int cornerIndex)
    {
        cornerIndex = cornerIndex.Mod(4);

        return cornerIndex switch
        {
            0 => LinFloat64Vector2D.Create(MinX, MinY),
            1 => LinFloat64Vector2D.Create(MaxX, MinY),
            2 => LinFloat64Vector2D.Create(MinX, MaxY),
            _ => LinFloat64Vector2D.Create(MaxX, MaxY)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetLongestSideIndex()
    {
        return MaxX - MinX >= MaxY - MinY
            ? 0 : 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetLongestSideMinValue()
    {
        return MaxX - MinX >= MaxY - MinY
            ? MinX
            : MinY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetLongestSideMaxValue()
    {
        return MaxX - MinX >= MaxY - MinY
            ? MaxX
            : MaxY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetLongestSideMidValue()
    {
        return MaxX - MinX >= MaxY - MinY
            ? 0.5 * (MinX + MaxX)
            : 0.5 * (MinY + MaxY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetLongestSideLength()
    {
        return Math.Max(
            MaxX - MinX,
            MaxY - MinY
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange GetLongestSideRange()
    {
        return MaxX - MinX >= MaxY - MinY
            ? Float64ScalarRange.Create(MinX, MaxX)
            : Float64ScalarRange.Create(MinY, MaxY);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetShortestSideMinValue()
    {
        return MaxX - MinX <= MaxY - MinY
            ? MinX
            : MinY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetShortestSideMaxValue()
    {
        return MaxX - MinX <= MaxY - MinY
            ? MaxX
            : MaxY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetShortestSideMidValue()
    {
        return MaxX - MinX <= MaxY - MinY
            ? 0.5 * (MinX + MaxX)
            : 0.5 * (MinY + MaxY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetShortestSideLength()
    {
        return Math.Min(
            MaxX - MinX,
            MaxY - MinY
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange GetShortestSideRange()
    {
        return MaxX - MinX <= MaxY - MinY
            ? Float64ScalarRange.Create(MinX, MaxX)
            : Float64ScalarRange.Create(MinY, MaxY);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSideMinValue(int axisIndex)
    {
        axisIndex = axisIndex.Mod(2);

        return axisIndex == 0 ? MinX : MinY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSideMaxValue(int axisIndex)
    {
        axisIndex = axisIndex.Mod(2);

        return axisIndex == 0 ? MaxX : MaxY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSideMidValue(int axisIndex)
    {
        axisIndex = axisIndex.Mod(2);

        return axisIndex == 0
            ? 0.5 * (MinX + MaxX)
            : 0.5 * (MinY + MaxY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSideLength(int axisIndex)
    {
        axisIndex = axisIndex.Mod(2);

        return axisIndex == 0
            ? MaxX - MinX
            : MaxY - MinY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange GetSideRange(int axisIndex)
    {
        axisIndex = axisIndex.Mod(2);

        return axisIndex == 0
            ? Float64ScalarRange.Create(MinX, MaxX)
            : Float64ScalarRange.Create(MinY, MaxY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetSideDirection(int cornerIndex1, int cornerIndex2)
    {
        var point1 = GetCorner(cornerIndex1);
        var point2 = GetCorner(cornerIndex2);

        return LinFloat64Vector2D.Create(
            point2.Item1 - point1.Item1,
            point2.Item2 - point1.Item2
        );
    }


    public LinFloat64Vector2D GetPointAt(double tx, double ty)
    {
        return LinFloat64Vector2D.Create(
            (Float64Scalar)((1.0d - tx) * MinX + tx * MaxX),
            (Float64Scalar)((1.0d - ty) * MinY + ty * MaxY)
        );
    }

    public LinFloat64Vector2D GetPointAt(LinFloat64Vector2D tVector)
    {
        return LinFloat64Vector2D.Create(
            (1.0d - tVector.Item1) * MinX + tVector.Item1 * MaxX,
            (1.0d - tVector.Item2) * MinY + tVector.Item2 * MaxY
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetPointOffset(LinFloat64Vector2D point)
    {
        var oX = point.Item1 - MinX;
        var oY = point.Item2 - MinY;

        if (MaxX > MinX)
            oX = oX / (MaxX - MinX);

        if (MaxY > MinY)
            oY = oY / (MaxY - MinY);

        return LinFloat64Vector2D.Create(oX, oY);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(LinFloat64Vector2D point, double tolerance)
    {
        return
            point.Item1 >= MinX - tolerance &&
            point.Item1 <= MaxX + tolerance &&
            point.Item2 >= MinY - tolerance &&
            point.Item2 <= MaxY + tolerance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(double pointX, double pointY, bool useMargins = false, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (useMargins)
            return
                pointX >= MinX - tolerance &&
                pointX <= MaxX + tolerance &&
                pointY >= MinY - tolerance &&
                pointY <= MaxY + tolerance;

        return
            pointX >= MinX &&
            pointX <= MaxX &&
            pointY >= MinY &&
            pointY <= MaxY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPointUpperExclusive(LinFloat64Vector2D point, bool useMargins = false, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (useMargins)
            return
                point.Item1 >= MinX - tolerance &&
                point.Item1 < MaxX + tolerance &&
                point.Item2 >= MinY - tolerance &&
                point.Item2 < MaxY + tolerance;

        return
            point.Item1 >= MinX &&
            point.Item1 < MaxX &&
            point.Item2 >= MinY &&
            point.Item2 < MaxY;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsBox(Float64BoundingBox2D box, bool useMargins = false, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (useMargins)
            return
                box.MinX >= MinX - tolerance &&
                box.MaxX <= MaxX + tolerance &&
                box.MinY >= MinY - tolerance &&
                box.MaxY <= MaxY + tolerance;

        return
            box.MinX >= MinX &&
            box.MaxX <= MaxX &&
            box.MinY >= MinY &&
            box.MaxY <= MaxY;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IntersectsBox(Float64BoundingBox2D box, double tolerance)
    {
        return
            box.MaxX >= MinX - tolerance &&
            box.MinX <= MaxX + tolerance &&
            box.MaxY >= MinY - tolerance &&
            box.MinY <= MaxY + tolerance;
    }


    public Float64BoundingBox2D[,] GetSubdivisions(int xDivisions, int yDivisions)
    {
        var xLength = SizeX / xDivisions;
        var yLength = SizeY / yDivisions;

        var minXValues =
            Enumerable
                .Range(0, xDivisions)
                .Select(i => i * xLength + MinX)
                .ToArray();

        var maxXValues =
            minXValues
                .Select(v => v + xLength)
                .ToArray();

        var minYValues =
            Enumerable
                .Range(0, yDivisions)
                .Select(i => i * yLength + MinY)
                .ToArray();

        var maxYValues =
            minYValues
                .Select(v => v + yLength)
                .ToArray();

        var divisions = new Float64BoundingBox2D[xDivisions, yDivisions];

        for (var ix = 0; ix < xDivisions; ix++)
            for (var iy = 0; iy < yDivisions; iy++)
                divisions[ix, iy] =
                    Containing(
                        minXValues[ix],
                        minYValues[iy],
                        maxXValues[ix],
                        maxYValues[iy]
                    );

        return divisions;

    }


    public Tuple<bool, Float64LineSegment2D> ClipLine(IFloat64Line2D line)
    {
        var tMin = double.NegativeInfinity;
        var tMax = double.PositiveInfinity;

        //Compute intersection parameters of ray with Y slaps
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
                return new Tuple<bool, Float64LineSegment2D>(
                    false,
                    Float64LineSegment2D.ZeroSegment
                );
        }

        //Compute intersection parameters of ray with X slaps
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

    public Tuple<bool, Float64LineSegment2D> ClipLine(IFloat64Line2D line, double lineParamMinValue, double lineParamMaxValue = double.PositiveInfinity)
    {
        var tMin = lineParamMinValue;
        var tMax = lineParamMaxValue;

        //Compute intersection parameters of ray with Y slaps
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
                return new Tuple<bool, Float64LineSegment2D>(
                    false,
                    Float64LineSegment2D.ZeroSegment
                );
        }

        //Compute intersection parameters of ray with X slaps
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


    public bool TryIntersectRay(IFloat64Ray<LinFloat64Vector2D> ray, out double tMin, out double tMax, double tolerance = Float64Utils.DefaultTolerance, double geometricTolerance = 1e-9)
    {
        tMin = double.NegativeInfinity;
        tMax = double.PositiveInfinity;

        var ox = ray.Origin.X;
        var oy = ray.Origin.Y;
        var dx = ray.Direction.X;
        var dy = ray.Direction.Y;

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

        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetComponents(bool byCorner = true)
    {
        if (byCorner)
        {
            yield return MinX;
            yield return MinY;

            yield return MaxX;
            yield return MaxY;
        }
        else
        {
            yield return MinX;
            yield return MaxX;

            yield return MinY;
            yield return MaxY;
        }
    }


    public override string ToString()
    {
        return new StringBuilder()
            .Append("AABB: ")
            .Append(MinPoint.ToTupleString())
            .Append(" -> ")
            .Append(MaxPoint.ToTupleString())
            .Append($" [Size: {Size.ToTupleString()}]")
            .ToString();
    }
}