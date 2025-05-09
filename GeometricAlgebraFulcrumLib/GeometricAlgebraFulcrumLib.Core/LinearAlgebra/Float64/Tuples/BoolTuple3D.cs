using System.Collections;
using System.Diagnostics;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Tuples;

public sealed record BoolTuple3D :
    IEnumerable<bool>
{
    public static BoolTuple3D operator !(BoolTuple3D v1)
    {
        return new BoolTuple3D(!v1.ItemX, !v1.ItemY, !v1.ItemZ);
    }

    public static BoolTuple3D operator |(BoolTuple3D v1, BoolTuple3D v2)
    {
        return new BoolTuple3D(v1.ItemX | v2.ItemX, v1.ItemY | v2.ItemY, v1.ItemZ | v2.ItemZ);
    }

    public static BoolTuple3D operator &(BoolTuple3D v1, BoolTuple3D v2)
    {
        return new BoolTuple3D(v1.ItemX & v2.ItemX, v1.ItemY & v2.ItemY, v1.ItemZ & v2.ItemZ);
    }

    public static BoolTuple3D operator ^(BoolTuple3D v1, BoolTuple3D v2)
    {
        return new BoolTuple3D(v1.ItemX ^ v2.ItemX, v1.ItemY ^ v2.ItemY, v1.ItemZ ^ v2.ItemZ);
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
    /// Third Component of tuple
    /// </summary>
    public bool ItemZ { get; }


    /// <summary>
    /// Get or set the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool this[int index]
    {
        get
        {
            Debug.Assert(index is 0 or 1 or 2);

            if (index == 0) return ItemX;
            if (index == 1) return ItemY;
            if (index == 2) return ItemZ;

            return false;
        }
    }


    public BoolTuple3D(bool x, bool y, bool z)
    {
        ItemX = x;
        ItemY = y;
        ItemZ = z;
    }

    public BoolTuple3D(BoolTuple3D tuple)
    {
        ItemX = tuple.ItemX;
        ItemY = tuple.ItemY;
        ItemZ = tuple.ItemZ;
    }


    public IEnumerator<bool> GetEnumerator()
    {
        yield return ItemX;
        yield return ItemY;
        yield return ItemZ;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return ItemX;
        yield return ItemY;
        yield return ItemZ;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("(")
            .Append(ItemX.ToString())
            .Append(", ")
            .Append(ItemY.ToString())
            .Append(", ")
            .Append(ItemZ.ToString())
            .Append(")")
            .ToString();
    }
}