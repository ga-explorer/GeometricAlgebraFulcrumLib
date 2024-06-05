using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

public sealed record LinFloat64Vector4D :
    ILinFloat64Vector4D,
    ILinFloat64Multivector4D
{
    public static LinFloat64Vector4D Zero { get; }
        = Create(0, 0, 0, 0);

    public static LinFloat64Vector4D E1 { get; }
        = Create(1, 0, 0, 0);

    public static LinFloat64Vector4D E2 { get; }
        = Create(0, 1, 0, 0);

    public static LinFloat64Vector4D E3 { get; }
        = Create(0, 0, 1, 0);

    public static LinFloat64Vector4D E4 { get; }
        = Create(0, 0, 0, 1);

    public static LinFloat64Vector4D NegativeE1 { get; }
        = Create(-1, 0, 0, 0);

    public static LinFloat64Vector4D NegativeE2 { get; }
        = Create(0, -1, 0, 0);

    public static LinFloat64Vector4D NegativeE3 { get; }
        = Create(0, 0, -1, 0);

    public static LinFloat64Vector4D NegativeE4 { get; }
        = Create(0, 0, 0, -1);

    public static LinFloat64Vector4D Symmetric { get; }
        = Create(1, 1, 1, 1);

    public static LinFloat64Vector4D UnitSymmetric { get; }
        = Create(0.5d, 0.5d, 0.5d, 0.5d);

    public static IReadOnlyList<LinFloat64Vector4D> BasisVectors { get; }
        = new[] { E1, E2, E3, E4 };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Create(int x, int y, int z, int w)
    {
        return new LinFloat64Vector4D(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Create(double x, double y, double z, double w)
    {
        return new LinFloat64Vector4D(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Create(Float64Scalar x, Float64Scalar y, Float64Scalar z, Float64Scalar w)
    {
        return new LinFloat64Vector4D(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Create(ITriplet<double> v, double s)
    {
        return Create(v.Item1, v.Item2, v.Item3, s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Create(IQuad<double> v)
    {
        return Create(v.Item1, v.Item2, v.Item3, v.Item4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D CreateAffineVector(double x, double y, double z)
    {
        return Create(x, y, z, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D CreateAffinePoint(double x, double y, double z)
    {
        return Create(x, y, z, 1);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator -(LinFloat64Vector4D v1)
    {
        return Create(-v1.X, -v1.Y, -v1.Z, -v1.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator +(LinFloat64Vector4D v1, ILinFloat64Vector4D v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator +(ILinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator +(LinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator -(LinFloat64Vector4D v1, ILinFloat64Vector4D v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator -(ILinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator -(LinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator *(LinFloat64Vector4D v1, double s)
    {
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator *(double s, LinFloat64Vector4D v1)
    {
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator /(LinFloat64Vector4D v1, double s)
    {
        Debug.Assert(!s.IsNearZero());

        s = 1.0d / s;
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }


    public Float64Scalar X
        => Scalar1;

    public Float64Scalar Y
        => Scalar2;

    public Float64Scalar Z
        => Scalar3;

    public Float64Scalar W
        => Scalar4;

    public int VSpaceDimensions
        => 4;

    public Float64Scalar Item1
        => X;

    public Float64Scalar Item2
        => Y;

    public Float64Scalar Item3
        => Z;

    public Float64Scalar Item4
        => W;

    public Float64Scalar Scalar
        => Float64Scalar.Zero;

    public Float64Scalar Scalar1 { get; }

    public Float64Scalar Scalar2 { get; }

    public Float64Scalar Scalar3 { get; }

    public Float64Scalar Scalar4 { get; }

    public Float64Scalar Scalar12
        => Float64Scalar.Zero;

    public Float64Scalar Scalar13
        => Float64Scalar.Zero;

    public Float64Scalar Scalar23
        => Float64Scalar.Zero;

    public Float64Scalar Scalar14
        => Float64Scalar.Zero;

    public Float64Scalar Scalar24
        => Float64Scalar.Zero;

    public Float64Scalar Scalar34
        => Float64Scalar.Zero;

    public Float64Scalar Scalar123
        => Float64Scalar.Zero;

    public Float64Scalar Scalar124
        => Float64Scalar.Zero;

    public Float64Scalar Scalar134
        => Float64Scalar.Zero;

    public Float64Scalar Scalar234
        => Float64Scalar.Zero;

    public Float64Scalar Scalar1234
        => Float64Scalar.Zero;

    public int Count
        => 16;

    /// <summary>
    /// Get or set the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Float64Scalar this[int index]
    {
        get
        {
            if (index is < 0 or > 15)
                throw new IndexOutOfRangeException();

            return index switch
            {
                1 => X,
                2 => Y,
                4 => Z,
                8 => W,
                _ => Float64Scalar.Zero
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Vector4D(Float64Scalar x, Float64Scalar y, Float64Scalar z, Float64Scalar w)
    {
        Scalar1 = x;
        Scalar2 = y;
        Scalar3 = z;
        Scalar4 = w;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return X.IsValid() &&
               Y.IsValid() &&
               Z.IsValid() &&
               W.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return X.IsZero() &&
               Y.IsZero() &&
               Z.IsZero() &&
               W.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double epsilon = 1E-12)
    {
        return Norm().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Norm()
    {
        return (X.Square() + Y.Square() + Z.Square() + W.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar NormSquared()
    {
        return X.Square() + Y.Square() + Z.Square() + W.Square();
    }

    //public Float64Multivector4D ToMultivector4D()
    //{
    //    throw new NotImplementedException();
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Scalar> GetEnumerator()
    {
        yield return Scalar;     //0000
        yield return Scalar1;    //0001
        yield return Scalar2;    //0010
        yield return Scalar12;   //0011
        yield return Scalar3;    //0100
        yield return Scalar13;   //0101
        yield return Scalar23;   //0110
        yield return Scalar123;  //0111
        yield return Scalar4;    //1000
        yield return Scalar14;   //1001
        yield return Scalar24;   //1010
        yield return Scalar124;  //1011
        yield return Scalar34;   //1100
        yield return Scalar134;  //1101
        yield return Scalar234;  //1110
        yield return Scalar1234; //1111
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({X:G})<1> + ({Y:G})<2> + ({Z:G})<4> + ({W:G})<8>";
    }
}