using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space2D;

public sealed class LinFloat64Normal2D :
    ILinFloat64Vector2D
{
    public double X { get; private set; }

    public double Y { get; private set; }
    
    public double this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            _ => Float64Scalar.Zero
        };

    public int VSpaceDimensions
        => 2;

    public double Item1
        => X;

    public double Item2
        => Y;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Normal2D()
    {
        X = 0;
        Y = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Normal2D(double x, double y)
    {
        X = x;
        Y = y;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Normal2D(ILinFloat64Vector2D normal)
    {
        X = normal.X;
        Y = normal.Y;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return X.IsValid() &&
               Y.IsValid();
    }

    /// <summary>
    /// Reset the normal to zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        X = 0;
        Y = 0;
    }

    /// <summary>
    /// Set the normal to the given value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(double x, double y)
    {
        X = x;
        Y = y;

        Debug.Assert(IsValid());
    }

    /// <summary>
    /// Set the normal to the given value
    /// </summary>
    /// <param name="normalPair"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(IPair<double> normalPair)
    {
        X = normalPair.Item1;
        Y = normalPair.Item2;

        Debug.Assert(IsValid());
    }
    
    /// <summary>
    /// Set the normal to the given value
    /// </summary>
    /// <param name="normalPair"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(LinFloat64Vector2D normalPair)
    {
        X = normalPair.Item1;
        Y = normalPair.Item2;

        Debug.Assert(IsValid());
    }

    /// <summary>
    /// Add the given vector to the normal of this vertex
    /// </summary>
    /// <param name="dx"></param>
    /// <param name="dy"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update(double dx, double dy)
    {
        X += dx;
        Y += dy;

        Debug.Assert(IsValid());
    }

    /// <summary>
    /// Add the given vector to this normal
    /// </summary>
    /// <param name="normalPair"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update(IPair<double> normalPair)
    {
        X += normalPair.Item1;
        Y += normalPair.Item2;

        Debug.Assert(IsValid());
    }
    
    /// <summary>
    /// Add the given vector to this normal
    /// </summary>
    /// <param name="normalPair"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update(LinFloat64Vector2D normalPair)
    {
        X += normalPair.Item1;
        Y += normalPair.Item2;

        Debug.Assert(IsValid());
    }

    /// <summary>
    /// Make the normal vector of this vertex a unit vector if not near zero
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MakeUnit()
    {
        var s = Math.Sqrt(X * X + Y * Y);
        if (s.IsNearZero())
            return;

        s = 1.0d / s;
        X *= s;
        Y *= s;

        Debug.Assert(IsValid());
    }

    /// <summary>
    /// Reverse the direction of the normal and make its length 1
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MakeNegativeUnit()
    {
        var s = Math.Sqrt(X * X + Y * Y);
        if (s.IsNearZero())
            return;

        s = -1.0d / s;
        X *= s;
        Y *= s;

        Debug.Assert(IsValid());
    }

    /// <summary>
    /// Reverse the direction of the normal
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MakeNegative()
    {
        X = -X;
        Y = -Y;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Normal2D GetNegative()
    {
        return new LinFloat64Normal2D(-X, -Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({X:G}, {Y:G})";
    }
}