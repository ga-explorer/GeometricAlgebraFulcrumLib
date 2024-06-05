using System.Collections;
using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Tuples;

/// <summary>
/// This structure is a integer 2-tuple that can hold the components of:
/// 1- 2D Position Vectors (Points)
/// 2- 2D Direction Vectors (Free Vectors)
/// 3- 2D Normals
/// 4- Complex numbers
/// </summary>
public sealed record IntTuple2D :
    IPair<int>,
    IEnumerable<int>
{
    public static IntTuple2D operator -(IntTuple2D v1)
    {
        return new IntTuple2D(-v1.X, -v1.Y);
    }

    public static IntTuple2D operator +(IntTuple2D v1, IntTuple2D v2)
    {
        return new IntTuple2D(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static IntTuple2D operator -(IntTuple2D v1, IntTuple2D v2)
    {
        return new IntTuple2D(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static IntTuple2D operator *(IntTuple2D v1, int s)
    {
        return new IntTuple2D(v1.X * s, v1.Y * s);
    }

    public static IntTuple2D operator *(int s, IntTuple2D v1)
    {
        return new IntTuple2D(v1.X * s, v1.Y * s);
    }


    /// <summary>
    /// First Component of tuple
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Second Component of tuple
    /// </summary>
    public int Y { get; }
    
    public int Item1 
        => X;

    public int Item2 
        => Y;

    /// <summary>
    /// The squared Euclidean length of this tuple if it represents a vector
    /// </summary>
    public int VectorLengthSquared
    {
        get { return X * X + Y * Y; }
    }

    /// <summary>
    /// Get or set the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int this[int index]
    {
        get
        {
            Debug.Assert(index is 0 or 1);

            if (index == 0) return X;
            if (index == 1) return Y;

            return 0;
        }
    }

    /// <summary>
    /// True of the Euclidean squared length of this vector is zero
    /// </summary>
    public bool IsZeroVector
    {
        get { return VectorLengthSquared == 0; }
    }

    /// <summary>
    /// The value of the smallest component in this tuple
    /// </summary>
    public int MinComponent
    {
        get { return X < Y ? X : Y; }
    }

    /// <summary>
    /// The value of the largest component in this tuple
    /// </summary>
    public int MaxComponent
    {
        get { return X > Y ? X : Y; }
    }

    /// <summary>
    /// The index of the smallest component in this tuple
    /// </summary>
    public int MinComponentIndex
    {
        get { return X < Y ? 0 : 1; }
    }

    /// <summary>
    /// The index of the largest component in this tuple
    /// </summary>
    public int MaxComponentIndex
    {
        get { return X > Y ? 0 : 1; }
    }


    public IntTuple2D(int x, int y)
    {
        X = x;
        Y = y;
    }

    public IntTuple2D(IntTuple2D tuple)
    {
        X = tuple.X;
        Y = tuple.Y;
    }


    public IEnumerator<int> GetEnumerator()
    {
        yield return X;
        yield return Y;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return X;
        yield return Y;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("(")
            .Append(X.ToString())
            .Append(", ")
            .Append(Y.ToString())
            .Append(")")
            .ToString();
    }

}