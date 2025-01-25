using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Int32;

public sealed class Int32BoundingBox2D
{
    /// <summary>
    /// The union of two AABBs
    /// </summary>
    /// <param name="b1"></param>
    /// <param name="b2"></param>
    /// <returns></returns>
    public static Int32BoundingBox2D operator |(Int32BoundingBox2D b1, Int32BoundingBox2D b2)
    {
        return new Int32BoundingBox2D(
            Math.Min(b1.MinX, b2.MinX),
            Math.Min(b1.MinY, b2.MinY),
            Math.Max(b1.MaxX, b2.MaxX),
            Math.Max(b1.MaxY, b2.MaxY)
        );
    }

    /// <summary>
    /// The intersection of two AABBs
    /// </summary>
    /// <param name="b1"></param>
    /// <param name="b2"></param>
    /// <returns></returns>
    public static Int32BoundingBox2D operator &(Int32BoundingBox2D b1, Int32BoundingBox2D b2)
    {
        return new Int32BoundingBox2D(
            Math.Max(b1.MinX, b2.MinX),
            Math.Max(b1.MinY, b2.MinY),
            Math.Min(b1.MaxX, b2.MaxX),
            Math.Min(b1.MaxY, b2.MaxY)
        );
    }


    public int MinX { get; private set; }

    public int MinY { get; private set; }

    public int MaxX { get; private set; }

    public int MaxY { get; private set; }

    public IntTuple2D this[int index]
    {
        get
        {
            Debug.Assert(index is 0 or 1);

            return (index & 1) == 0 ? MinCorner : MaxCorner;
        }
    }

    public IntTuple2D MinCorner
    {
        get { return new IntTuple2D(MinX, MinY); }
    }

    public IntTuple2D MaxCorner
    {
        get { return new IntTuple2D(MaxX, MaxY); }
    }

    public IntTuple2D Diagonal
    {
        get { return new IntTuple2D(MaxX - MinX, MaxY - MinY); }
    }

    public int Area
    {
        get
        {
            var dx = MaxX - MinX;
            var dy = MaxY - MinY;

            return 2 * dx * dy;
        }
    }

    public int MaxExtentIndex
    {
        get
        {
            var dx = MaxX - MinX;
            var dy = MaxY - MinY;

            if (dx > dy) return 0;

            return 1;
        }
    }

    public Float64BoundingCircle2D BoundingSphere
    {
        get
        {
            var cX = 0.5 * (MinX + MaxX);
            var cY = 0.5 * (MinY + MaxY);
            var r = 0.0d;

            if (
                cX >= MinX && cX <= MaxX &&
                cY >= MinY && cY <= MaxY
            )
                r = Math.Sqrt(
                    (MaxX - cX) * (MaxX - cX) +
                    (MaxY - cY) * (MaxY - cY)
                );

            return Float64BoundingCircle2D.Create(cX, cY, r);
        }
    }

    public IEnumerable<IntTuple2D> RasterPoints
    {
        get
        {
            for (var y = MinY; y <= MaxY; y++)
                for (var x = MinX; x <= MaxX; x++)
                    yield return new IntTuple2D(x, y);
        }
    }


    public Int32BoundingBox2D()
    {
        MinX = int.MinValue;
        MinY = int.MinValue;

        MaxX = int.MaxValue;
        MaxY = int.MaxValue;
    }

    public Int32BoundingBox2D(IntTuple2D point)
    {
        MinX = point.X;
        MinY = point.Y;

        MaxX = point.X;
        MaxY = point.Y;
    }

    public Int32BoundingBox2D(IntTuple2D point1, IntTuple2D point2)
    {
        if (point1.X < point2.X)
        {
            MinX = point1.X;
            MaxX = point2.X;
        }
        else
        {
            MinX = point2.X;
            MaxX = point1.X;
        }

        if (point1.Y < point2.Y)
        {
            MinY = point1.Y;
            MaxY = point2.Y;
        }
        else
        {
            MinY = point2.Y;
            MaxY = point1.Y;
        }
    }

    internal Int32BoundingBox2D(int minX, int minY, int maxX, int maxY)
    {
        MinX = minX;
        MinY = minY;

        MaxX = maxX;
        MaxY = maxY;
    }



    /// <summary>
    /// Given an index between 0 and 7 this returnes one corner of the box
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IntTuple2D GetCorner(int index)
    {
        Debug.Assert(index is >= 0 or <= 3);

        if (index == 0) return new IntTuple2D(MinX, MinY);
        if (index == 1) return new IntTuple2D(MaxX, MinY);
        if (index == 2) return new IntTuple2D(MinX, MaxY);
        if (index == 3) return new IntTuple2D(MaxX, MaxY);

        return new IntTuple2D(0, 0);
    }

    public Int32BoundingBox2D ExpandToMax()
    {
        MinX = int.MinValue;
        MinY = int.MinValue;

        MaxX = int.MaxValue;
        MaxY = int.MaxValue;

        return this;
    }

    public Int32BoundingBox2D ExpandBy(int delta)
    {
        MinX = MinX - delta;
        MinY = MinY - delta;

        MaxX = MaxX + delta;
        MaxY = MaxY + delta;

        return this;
    }

    public Int32BoundingBox2D ExpandBy(int deltaX, int deltaY)
    {
        MinX = MinX - deltaX;
        MinY = MinY - deltaY;

        MaxX = MaxX + deltaX;
        MaxY = MaxY + deltaY;

        return this;
    }

    public Int32BoundingBox2D ExpandToInclude(IntTuple2D point)
    {
        MinX = Math.Min(MinX, point.X);
        MinY = Math.Min(MinY, point.Y);

        MaxX = Math.Max(MaxX, point.X);
        MaxY = Math.Max(MaxY, point.Y);

        return this;
    }

    public Int32BoundingBox2D ExpandToInclude(Int32BoundingBox2D box)
    {
        MinX = Math.Min(MinX, box.MinX);
        MinY = Math.Min(MinY, box.MinY);

        MaxX = Math.Max(MaxX, box.MaxX);
        MaxY = Math.Max(MaxY, box.MaxY);

        return this;
    }

    public bool Contains(IntTuple2D point)
    {
        return
            point.X >= MinX &&
            point.X <= MaxX &&
            point.Y >= MinY &&
            point.Y <= MaxY;
    }

    public bool Contains(Int32BoundingBox2D box)
    {
        return
            box.MinX >= MinX &&
            box.MaxX <= MaxX &&
            box.MinY >= MinY &&
            box.MaxY <= MaxY;
    }

    public bool ContainsUpperExclusive(IntTuple2D point)
    {
        return
            point.X >= MinX &&
            point.X < MaxX &&
            point.Y >= MinY &&
            point.Y < MaxY;
    }

    public bool Overlaps(Int32BoundingBox2D box)
    {
        return
            box.MaxX >= MinX &&
            box.MinX <= MaxX &&
            box.MaxY >= MinY &&
            box.MinY <= MaxY;
    }

    public IntTuple2D Offset(IntTuple2D point)
    {
        var oX = point.X - MinX;
        var oY = point.Y - MinY;

        if (MaxX > MinX) oX = oX / (MaxX - MinX);
        if (MaxY > MinY) oY = oY / (MaxY - MinY);

        return new IntTuple2D(oX, oY);
    }
}