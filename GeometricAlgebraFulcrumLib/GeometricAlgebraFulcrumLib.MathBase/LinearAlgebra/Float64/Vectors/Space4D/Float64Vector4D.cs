using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;

public sealed record Float64Vector4D :
    IFloat64Tuple4D,
    IReadOnlyList<double>
{
    public static Float64Vector4D Zero { get; } 
        = new Float64Vector4D(0, 0, 0, 0);

    public static Float64Vector4D E1 { get; } 
        = new Float64Vector4D(1, 0, 0, 0);

    public static Float64Vector4D E2 { get; } 
        = new Float64Vector4D(0, 1, 0, 0);

    public static Float64Vector4D E3 { get; } 
        = new Float64Vector4D(0, 0, 1, 0);

    public static Float64Vector4D E4 { get; } 
        = new Float64Vector4D(0, 0, 0, 1);

    public static Float64Vector4D NegativeE1 { get; } 
        = new Float64Vector4D(-1, 0, 0, 0);

    public static Float64Vector4D NegativeE2 { get; } 
        = new Float64Vector4D(0, -1, 0, 0);

    public static Float64Vector4D NegativeE3 { get; } 
        = new Float64Vector4D(0, 0, -1, 0);

    public static Float64Vector4D NegativeE4 { get; } 
        = new Float64Vector4D(0, 0, 0, -1);

    public static Float64Vector4D Symmetric { get; } 
        = new Float64Vector4D(1, 1, 1, 1);

    public static Float64Vector4D UnitSymmetric { get; } 
        = new Float64Vector4D(0.5d, 0.5d, 0.5d, 0.5d);
        
    public static IReadOnlyList<Float64Vector4D> BasisVectors { get; }
        = new[] { E1, E2, E3, E4 };


        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D CreateAffineVector(double x, double y, double z)
    {
        return new Float64Vector4D(x, y, z, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D CreateAffinePoint(double x, double y, double z)
    {
        return new Float64Vector4D(x, y, z, 1);
    }


        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D operator -(Float64Vector4D v1)
    {
        return new Float64Vector4D(-v1.X, -v1.Y, -v1.Z, -v1.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D operator +(Float64Vector4D v1, IFloat64Tuple4D v2)
    {
        return new Float64Vector4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D operator +(IFloat64Tuple4D v1, Float64Vector4D v2)
    {
        return new Float64Vector4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D operator +(Float64Vector4D v1, Float64Vector4D v2)
    {
        return new Float64Vector4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D operator -(Float64Vector4D v1, IFloat64Tuple4D v2)
    {
        return new Float64Vector4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D operator -(IFloat64Tuple4D v1, Float64Vector4D v2)
    {
        return new Float64Vector4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D operator -(Float64Vector4D v1, Float64Vector4D v2)
    {
        return new Float64Vector4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D operator *(Float64Vector4D v1, double s)
    {
        return new Float64Vector4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D operator *(double s, Float64Vector4D v1)
    {
        return new Float64Vector4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D operator /(Float64Vector4D v1, double s)
    {
        Debug.Assert(!s.IsAlmostZero());

        s = 1.0d / s;
        return new Float64Vector4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }
    
        
    /// <summary>
    /// The 1st component of this tuple. If this tuple holds a quaternion, this is the 1st component
    /// of its imaginary (i.e. vector) part
    /// </summary>
    public Float64Scalar X { get; }

    /// <summary>
    /// The 2nd component of this tuple. If this tuple holds a quaternion, this is the 2nd component
    /// of its imaginary (i.e. vector) part
    /// </summary>
    public Float64Scalar Y { get; }

    /// <summary>
    /// The 3rd component of this tuple. If this tuple holds a quaternion, this is the 3rd component
    /// of its imaginary (i.e. vector) part
    /// </summary>
    public Float64Scalar Z { get; }

    /// <summary>
    /// The 4th component of this tuple. If this tuple holds a quaternion, this is its scalar part
    /// </summary>
    public Float64Scalar W { get; }
        
    public int VSpaceDimensions 
        => 4;

    public double Item1 
        => X.Value;

    public double Item2 
        => Y.Value;

    public double Item3 
        => Z.Value;

    public double Item4 
        => W.Value;
        
    public int Count 
        => 4;

    /// <summary>
    /// Get or set the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public double this[int index]
    {
        get
        {
            return index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                3 => W,
                _ => throw new IndexOutOfRangeException()
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector4D(double x, double y, double z, double w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector4D(ITriplet<double> v, double s)
    {
        Debug.Assert(
            v.Item1.IsNotNaN() &&
            v.Item2.IsNotNaN() &&
            v.Item3.IsNotNaN() &&
            s.IsNotNaN()
        );

        X = v.Item1;
        Y = v.Item2;
        Z = v.Item3;
        W = s;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector4D(IQuad<double> v)
    {
        Debug.Assert(
            v.Item1.IsNotNaN() &&
            v.Item2.IsNotNaN() &&
            v.Item3.IsNotNaN() &&
            v.Item4.IsNotNaN()
        );

        X = v.Item1;
        Y = v.Item2;
        Z = v.Item3;
        W = v.Item4;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return !double.IsNaN(X) &&
               !double.IsNaN(Y) &&
               !double.IsNaN(Z) &&
               !double.IsNaN(W);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<double> GetEnumerator()
    {
        yield return X;
        yield return Y;
        yield return Z;
        yield return W;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({X:G}, {Y:G}, {Z:G}, {W:G})";
    }

}