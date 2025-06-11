using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Frames.Space2D;

public sealed class LinFloat64Normal2D :
    ILinFloat64Vector2D
{
    public double X { get; private set; }

    public double Y { get; private set; }

    public int VSpaceDimensions
        => 2;

    public double Item1
        => X;

    public double Item2
        => Y;


    
    public LinFloat64Normal2D()
    {
        X = 0;
        Y = 0;
    }

    
    public LinFloat64Normal2D(double x, double y)
    {
        X = x;
        Y = y;

        Debug.Assert(IsValid());
    }

    
    public LinFloat64Normal2D(ILinFloat64Vector2D normal)
    {
        X = normal.X;
        Y = normal.Y;

        Debug.Assert(IsValid());
    }


    
    public bool IsValid()
    {
        return !double.IsNaN(X) &&
               !double.IsNaN(Y);
    }

    /// <summary>
    /// Reset the normal to zero
    /// </summary>
    
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
    /// <param name="z"></param>
    
    public void Set(double x, double y, double z)
    {
        X = x;
        Y = y;

        Debug.Assert(IsValid());
    }

    /// <summary>
    /// Set the normal to the given value
    /// </summary>
    /// <param name="normalPair"></param>
    
    public void Set(IPair<double> normalPair)
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
    
    public void Update(IPair<double> normalPair)
    {
        X += normalPair.Item1;
        Y += normalPair.Item2;

        Debug.Assert(IsValid());
    }

    /// <summary>
    /// Make the normal vector of this vertex a unit vector if not near zero
    /// </summary>
    
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
    
    public void MakeNegative()
    {
        X = -X;
        Y = -Y;

        Debug.Assert(IsValid());
    }

    
    public LinFloat64Normal2D GetNegative()
    {
        return new LinFloat64Normal2D(-X, -Y);
    }

    
    public override string ToString()
    {
        return $"({X:G}, {Y:G})";
    }
}