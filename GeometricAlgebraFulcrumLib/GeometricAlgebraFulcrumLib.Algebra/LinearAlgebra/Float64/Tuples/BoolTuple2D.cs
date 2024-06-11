using System.Collections;
using System.Diagnostics;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;

public sealed record BoolTuple2D :
    IEnumerable<bool>
{
    public static BoolTuple2D operator !(BoolTuple2D v1)
    {
        return new BoolTuple2D(!v1.ItemX, !v1.ItemY);
    }

    public static BoolTuple2D operator |(BoolTuple2D v1, BoolTuple2D v2)
    {
        return new BoolTuple2D(v1.ItemX | v2.ItemX, v1.ItemY | v2.ItemY);
    }

    public static BoolTuple2D operator &(BoolTuple2D v1, BoolTuple2D v2)
    {
        return new BoolTuple2D(v1.ItemX & v2.ItemX, v1.ItemY & v2.ItemY);
    }

    public static BoolTuple2D operator ^(BoolTuple2D v1, BoolTuple2D v2)
    {
        return new BoolTuple2D(v1.ItemX ^ v2.ItemX, v1.ItemY ^ v2.ItemY);
    }


    /// <summary>
    /// First Component of tuple
    /// </summary>
    public bool ItemX { get; }

    /// <summary>
    /// Second Component of tuple
    /// </summary>
    public bool ItemY { get; }


    /// <summary>
    /// Get or set the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool this[int index]
    {
        get
        {
            Debug.Assert(index is 0 or 1);

            if (index == 0) return ItemX;
            if (index == 1) return ItemY;

            return false;
        }
    }


    public BoolTuple2D(bool x, bool y)
    {
        ItemX = x;
        ItemY = y;
    }

    public BoolTuple2D(BoolTuple2D tuple)
    {
        ItemX = tuple.ItemX;
        ItemY = tuple.ItemY;
    }


    public IEnumerator<bool> GetEnumerator()
    {
        yield return ItemX;
        yield return ItemY;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return ItemX;
        yield return ItemY;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("(")
            .Append(ItemX.ToString())
            .Append(", ")
            .Append(ItemY.ToString())
            .Append(")")
            .ToString();
    }
}