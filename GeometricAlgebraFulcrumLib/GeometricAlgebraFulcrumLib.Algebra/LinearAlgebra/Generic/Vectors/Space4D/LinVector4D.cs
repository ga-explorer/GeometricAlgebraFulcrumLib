using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;

public sealed record LinVector4D<T> :
    ILinVector4D<T>,
    ILinMultivector4D<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4D<T>(
            scalarProcessor,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> E1(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4D<T>(
            scalarProcessor,
            scalarProcessor.OneValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> E2(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4D<T>(
            scalarProcessor,
            scalarProcessor.ZeroValue,
            scalarProcessor.OneValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> E3(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4D<T>(
            scalarProcessor,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.OneValue,
            scalarProcessor.ZeroValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> E4(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4D<T>(
            scalarProcessor,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.OneValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> NegativeE1(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4D<T>(
            scalarProcessor,
            scalarProcessor.MinusOneValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> NegativeE2(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4D<T>(
            scalarProcessor,
            scalarProcessor.ZeroValue,
            scalarProcessor.MinusOneValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> NegativeE3(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4D<T>(
            scalarProcessor,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.MinusOneValue,
            scalarProcessor.ZeroValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> NegativeE4(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4D<T>(
            scalarProcessor,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue,
            scalarProcessor.MinusOneValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Symmetric(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4D<T>(
            scalarProcessor,
            scalarProcessor.OneValue,
            scalarProcessor.OneValue,
            scalarProcessor.OneValue,
            scalarProcessor.OneValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> UnitSymmetric(IScalarProcessor<T> scalarProcessor)
    {
        var oneOver2 = scalarProcessor.Divide(
            scalarProcessor.OneValue,
            scalarProcessor.TwoValue
        );

        return new LinVector4D<T>(
            oneOver2,
            oneOver2,
            oneOver2,
            oneOver2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinVector4D<T>> BasisVectors(IScalarProcessor<T> scalarProcessor)
    {
        return new[]
        {
            E1(scalarProcessor), 
            E2(scalarProcessor), 
            E3(scalarProcessor), 
            E4(scalarProcessor)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Create(IScalarProcessor<T> scalarProcessor, int x, int y, int z, int w)
    {
        return new LinVector4D<T>(
            scalarProcessor.ScalarFromNumber(x),
            scalarProcessor.ScalarFromNumber(y),
            scalarProcessor.ScalarFromNumber(z),
            scalarProcessor.ScalarFromNumber(w)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Create(IScalarProcessor<T> scalarProcessor, double x, double y, double z, double w)
    {
        return new LinVector4D<T>(
            scalarProcessor.ScalarFromNumber(x),
            scalarProcessor.ScalarFromNumber(y),
            scalarProcessor.ScalarFromNumber(z),
            scalarProcessor.ScalarFromNumber(w)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Create(IScalarProcessor<T> scalarProcessor, T x, T y, T z, T w)
    {
        return new LinVector4D<T>(scalarProcessor, x, y, z, w);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Create(IScalar<T> x, IScalar<T> y, IScalar<T> z, IScalar<T> w)
    {
        return new LinVector4D<T>(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Create(Scalar<T> x, Scalar<T> y, Scalar<T> z, Scalar<T> w)
    {
        return new LinVector4D<T>(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Create(ITriplet<Scalar<T>> v, Scalar<T> s)
    {
        return Create(v.Item1, v.Item2, v.Item3, s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> Create(IQuad<Scalar<T>> v)
    {
        return Create(v.Item1, v.Item2, v.Item3, v.Item4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> CreateAffineVector(Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        return Create(x, y, z, x.ScalarProcessor.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> CreateAffinePoint(Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        return Create(x, y, z, x.ScalarProcessor.One);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator -(LinVector4D<T> v1)
    {
        return Create(-v1.X, -v1.Y, -v1.Z, -v1.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator +(LinVector4D<T> v1, ILinVector4D<T> v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator +(ILinVector4D<T> v1, LinVector4D<T> v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator +(LinVector4D<T> v1, LinVector4D<T> v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator -(LinVector4D<T> v1, ILinVector4D<T> v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator -(ILinVector4D<T> v1, LinVector4D<T> v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator -(LinVector4D<T> v1, LinVector4D<T> v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator *(LinVector4D<T> v1, double s)
    {
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator *(double s, LinVector4D<T> v1)
    {
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> operator /(LinVector4D<T> v1, double s)
    {
        Debug.Assert(!s.IsNearZero());

        s = 1.0d / s;
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }


    public IScalarProcessor<T> ScalarProcessor 
        => X.ScalarProcessor;

    public Scalar<T> X
        => Scalar1;

    public Scalar<T> Y
        => Scalar2;

    public Scalar<T> Z
        => Scalar3;

    public Scalar<T> W
        => Scalar4;

    public int VSpaceDimensions
        => 4;

    public Scalar<T> Item1
        => X;

    public Scalar<T> Item2
        => Y;

    public Scalar<T> Item3
        => Z;

    public Scalar<T> Item4
        => W;

    public Scalar<T> Scalar
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar1 { get; }

    public Scalar<T> Scalar2 { get; }

    public Scalar<T> Scalar3 { get; }

    public Scalar<T> Scalar4 { get; }

    public Scalar<T> Scalar12
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar13
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar23
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar14
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar24
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar34
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar123
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar124
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar134
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar234
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar1234
        => ScalarProcessor.Zero;

    public int Count
        => 16;

    /// <summary>
    /// Get or set the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Scalar<T> this[int index]
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
                _ => ScalarProcessor.Zero
            };
        }
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinVector4D(IScalarProcessor<T> scalarProcessor, T x, T y, T z, T w)
    {
        Scalar1 = scalarProcessor.ScalarFromValue(x);
        Scalar2 = scalarProcessor.ScalarFromValue(y);
        Scalar3 = scalarProcessor.ScalarFromValue(z);
        Scalar4 = scalarProcessor.ScalarFromValue(w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinVector4D(IScalar<T> x, IScalar<T> y, IScalar<T> z, IScalar<T> w)
    {
        Scalar1 = x.ToScalar();
        Scalar2 = y.ToScalar();
        Scalar3 = z.ToScalar();
        Scalar4 = w.ToScalar();
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
    public bool IsNearZero()
    {
        return Norm().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return (X.Square() + Y.Square() + Z.Square() + W.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return X.Square() + Y.Square() + Z.Square() + W.Square();
    }

    //public Float64Multivector4D ToMultivector4D()
    //{
    //    throw new NotImplementedException();
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Scalar<T>> GetEnumerator()
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
        return $"({X})<1> + ({Y})<2> + ({Z})<4> + ({W})<8>";
    }
}