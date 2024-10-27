using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

/// <summary>
/// A 3-tuple of double precision coordinates
/// </summary>
public sealed record LinVector3D<T> :
    ILinVector3D<T>,
    ILinKVector3D<T>
{
    public static LinVector3D<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.Zero,
            scalarProcessor.Zero,
            scalarProcessor.Zero
        );
    }

    public static LinVector3D<T> E1(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.One,
            scalarProcessor.Zero,
            scalarProcessor.Zero
        );
    }

    public static LinVector3D<T> E2(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.Zero,
            scalarProcessor.One,
            scalarProcessor.Zero
        );
    }

    public static LinVector3D<T> E3(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.Zero,
            scalarProcessor.Zero,
            scalarProcessor.One
        );
    }

    public static LinVector3D<T> NegativeE1(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.MinusOne,
            scalarProcessor.Zero,
            scalarProcessor.Zero
        );
    }

    public static LinVector3D<T> NegativeE2(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.Zero,
            scalarProcessor.MinusOne,
            scalarProcessor.Zero
        );
    }

    public static LinVector3D<T> NegativeE3(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.Zero,
            scalarProcessor.Zero,
            scalarProcessor.MinusOne
        );
    }

    public static LinVector3D<T> Symmetric(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.One,
            scalarProcessor.One,
            scalarProcessor.One
        );
    }

    public static LinVector3D<T> SymmetricNpp(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.MinusOne,
            scalarProcessor.One,
            scalarProcessor.One
        );
    }

    public static LinVector3D<T> SymmetricPnp(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.One,
            scalarProcessor.MinusOne,
            scalarProcessor.One
        );
    }

    public static LinVector3D<T> SymmetricPpn(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.One,
            scalarProcessor.One,
            scalarProcessor.MinusOne
        );
    }

    public static LinVector3D<T> SymmetricPnn(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.One,
            scalarProcessor.MinusOne,
            scalarProcessor.MinusOne
        );
    }

    public static LinVector3D<T> SymmetricNpn(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.MinusOne,
            scalarProcessor.One,
            scalarProcessor.MinusOne
        );
    }

    public static LinVector3D<T> SymmetricNnp(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.MinusOne,
            scalarProcessor.MinusOne,
            scalarProcessor.One
        );
    }

    public static LinVector3D<T> NegativeSymmetric(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3D<T>(
            scalarProcessor.MinusOne,
            scalarProcessor.MinusOne,
            scalarProcessor.MinusOne
        );
    }

    public static LinVector3D<T> UnitSymmetric(IScalarProcessor<T> scalarProcessor)
    {
        var invSqrt3 = 
            scalarProcessor.One.Divide(scalarProcessor.Sqrt(3));

        return new LinVector3D<T>(
            invSqrt3,
            invSqrt3,
            invSqrt3
        );
    }

    public static LinVector3D<T> NegativeUnitSymmetric(IScalarProcessor<T> scalarProcessor)
    {
        var invSqrt3 = 
            scalarProcessor.MinusOne.Divide(scalarProcessor.Sqrt(3));

        return new LinVector3D<T>(
            invSqrt3,
            invSqrt3,
            invSqrt3
        );
    }

    public static IReadOnlyList<LinVector3D<T>> BasisVectors(IScalarProcessor<T> scalarProcessor)
    {
        return new[]
        {
            E1(scalarProcessor),
            E2(scalarProcessor),
            E3(scalarProcessor)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Create(IScalarProcessor<T> scalarProcessor, int x, int y, int z)
    {
        return new LinVector3D<T>(
            scalarProcessor.ScalarFromNumber(x),
            scalarProcessor.ScalarFromNumber(y),
            scalarProcessor.ScalarFromNumber(z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Create(IScalarProcessor<T> scalarProcessor, float x, float y, float z)
    {
        return new LinVector3D<T>(
            scalarProcessor.ScalarFromNumber(x),
            scalarProcessor.ScalarFromNumber(y),
            scalarProcessor.ScalarFromNumber(z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Create(IScalarProcessor<T> scalarProcessor, double x, double y, double z)
    {
        return new LinVector3D<T>(
            scalarProcessor.ScalarFromNumber(x),
            scalarProcessor.ScalarFromNumber(y),
            scalarProcessor.ScalarFromNumber(z)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Create(IScalarProcessor<T> scalarProcessor, T x, T y, T z)
    {
        return new LinVector3D<T>(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Create(IScalar<T> x, IScalar<T> y, IScalar<T> z)
    {
        return new LinVector3D<T>(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Create(ITriplet<Scalar<T>> tuple)
    {
        return new LinVector3D<T>(
            tuple.Item1,
            tuple.Item2,
            tuple.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> CreateAffineVector(Scalar<T> x, Scalar<T> y)
    {
        return new LinVector3D<T>(
            x, 
            y, 
            x.ScalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> CreateAffinePoint(Scalar<T> x, Scalar<T> y)
    {
        return new LinVector3D<T>(x, y, x.ScalarProcessor.One);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> CreateEqualXyz(Scalar<T> x)
    {
        return new LinVector3D<T>(x, x, x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> CreateUnitVector(Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        var s = x * x + y * y + z * z;

        if (s.IsZero()) return UnitSymmetric(x.ScalarProcessor);

        s = 1d / s.Sqrt();

        return new LinVector3D<T>(x * s, y * s, z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> VectorSymmetric(Scalar<T> vectorLength)
    {
        var scalar = vectorLength.Divide(vectorLength.ScalarProcessor.Sqrt(3));

        return new LinVector3D<T>(scalar, scalar, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator -(LinVector3D<T> v1)
    {
        return new LinVector3D<T>(-v1.X, -v1.Y, -v1.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator +(LinVector3D<T> v1, ILinVector3D<T> v2)
    {
        return new LinVector3D<T>(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator +(ILinVector3D<T> v1, LinVector3D<T> v2)
    {
        return new LinVector3D<T>(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator +(LinVector3D<T> v1, LinVector3D<T> v2)
    {
        return new LinVector3D<T>(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator -(LinVector3D<T> v1, ILinVector3D<T> v2)
    {
        return new LinVector3D<T>(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator -(ILinVector3D<T> v1, LinVector3D<T> v2)
    {
        return new LinVector3D<T>(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator -(LinVector3D<T> v1, LinVector3D<T> v2)
    {
        return new LinVector3D<T>(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator *(LinVector3D<T> v1, int s)
    {
        var x = v1.X * s;
        var y = v1.Y * s;
        var z = v1.Z * s;

        return new LinVector3D<T>(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator *(int s, LinVector3D<T> v1)
    {
        var x = s * v1.X;
        var y = s * v1.Y;
        var z = s * v1.Z;

        return new LinVector3D<T>(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator *(LinVector3D<T> v1, double s)
    {
        var x = v1.X * s;
        var y = v1.Y * s;
        var z = v1.Z * s;

        return new LinVector3D<T>(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator *(double s, LinVector3D<T> v1)
    {
        var x = s * v1.X;
        var y = s * v1.Y;
        var z = s * v1.Z;

        return new LinVector3D<T>(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinComplexVector3D<T> operator *(LinVector3D<T> v1, ComplexNumber<T> s)
    {
        return new LinComplexVector3D<T>(v1.X * s, v1.Y * s, v1.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinComplexVector3D<T> operator *(ComplexNumber<T> s, LinVector3D<T> v1)
    {
        return new LinComplexVector3D<T>(v1.X * s, v1.Y * s, v1.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator *(LinVector3D<T> v1, Scalar<T> s)
    {
        var x = v1.X * s;
        var y = v1.Y * s;
        var z = v1.Z * s;

        return new LinVector3D<T>(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator *(Scalar<T> s, LinVector3D<T> v1)
    {
        var x = s * v1.X;
        var y = s * v1.Y;
        var z = s * v1.Z;

        return new LinVector3D<T>(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator /(LinVector3D<T> v1, int s)
    {
        var d = 1.0d / s;

        return new LinVector3D<T>(v1.X * d, v1.Y * d, v1.Z * d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator /(LinVector3D<T> v1, double s)
    {
        s = 1.0d / s;

        return new LinVector3D<T>(v1.X * s, v1.Y * s, v1.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator /(LinVector3D<T> v1, Scalar<T> s)
    {
        var d = s.Inverse();

        return new LinVector3D<T>(v1.X * d, v1.Y * d, v1.Z * d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator /(int s, LinVector3D<T> v1)
    {
        return (double)s * v1.Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator /(double s, LinVector3D<T> v1)
    {
        return s * v1.Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> operator /(Scalar<T> s, LinVector3D<T> v1)
    {
        return s * v1.Inverse();
    }


    public IScalarProcessor<T> ScalarProcessor 
        => Scalar1.ScalarProcessor;

    public int VSpaceDimensions
        => 3;

    public int Grade
        => 1;

    public Scalar<T> X
        => Scalar1;

    public Scalar<T> Y
        => Scalar2;

    public Scalar<T> Z
        => Scalar3;

    public Scalar<T> Item1
        => X;

    public Scalar<T> Item2
        => Y;

    public Scalar<T> Item3
        => Z;

    public Scalar<T> Scalar
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar1 { get; }

    public Scalar<T> Scalar2 { get; }

    public Scalar<T> Scalar3 { get; }

    public Scalar<T> Scalar12
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar13
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar23
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar123
        => ScalarProcessor.Zero;

    public int Count
        => 8;

    /// <summary>
    /// Get or set the ith component of this multivector
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Scalar<T> this[int index]
    {
        get
        {
            if (index is < 0 or > 7)
                throw new IndexOutOfRangeException();

            return index switch
            {
                1 => Scalar1,
                2 => Scalar2,
                4 => Scalar3,
                _ => ScalarProcessor.Zero
            };
        }
    }

    public Scalar<T> this[LinUnitBasisVector3D axis]
    {
        get
        {
            return axis switch
            {
                LinUnitBasisVector3D.PositiveX => X,
                LinUnitBasisVector3D.PositiveY => Y,
                LinUnitBasisVector3D.PositiveZ => Z,
                LinUnitBasisVector3D.NegativeX => -X,
                LinUnitBasisVector3D.NegativeY => -Y,
                LinUnitBasisVector3D.NegativeZ => -Z,
                _ => ScalarProcessor.Zero
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinVector3D(IScalar<T> scalar1, IScalar<T> scalar2, IScalar<T> scalar3)
    {
        Scalar1 = scalar1.ToScalar();
        Scalar2 = scalar2.ToScalar();
        Scalar3 = scalar3.ToScalar();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinVector3D(IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        Scalar1 = scalarProcessor.ScalarFromValue(scalar1);
        Scalar2 = scalarProcessor.ScalarFromValue(scalar2);
        Scalar3 = scalarProcessor.ScalarFromValue(scalar3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D(ITriplet<Scalar<T>> v)
    {
        Scalar1 = v.Item1;
        Scalar2 = v.Item2;
        Scalar3 = v.Item3;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar1.IsValid() &&
               Scalar2.IsValid() &&
               Scalar3.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return X.IsZero() &&
               Y.IsZero() &&
               Z.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return Norm().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return NormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return X.Square() + Y.Square() + Z.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector3D<T> ToMultivector3D()
    {
        return LinMultivector3D<T>.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> Negative()
    {
        return new LinVector3D<T>(-Scalar1,
            -Scalar2,
            -Scalar3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> GradeInvolution()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> Inverse()
    {
        var normSquared =
            NormSquared();

        return normSquared.IsZero()
            ? throw new DivideByZeroException()
            : this / normSquared;
    }


    public LinBivector3D<T> DirectionToUnitNormal3D(LinBivector3D<T>? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = 1d / norm;

        return LinBivector3D<T>.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinBivector3D<T> DirectionToNormal3D(LinBivector3D<T>? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = 1d / normSquared;

        return LinBivector3D<T>.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinBivector3D<T> DirectionToNormal3D(Scalar<T> scalingFactor, LinBivector3D<T>? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = scalingFactor / normSquared;

        return LinBivector3D<T>.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinBivector3D<T> NormalToUnitDirection3D(LinBivector3D<T>? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = 1d / norm;

        return LinBivector3D<T>.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinBivector3D<T> NormalToDirection3D(LinBivector3D<T>? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = 1d / normSquared;

        return LinBivector3D<T>.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinBivector3D<T> NormalToDirection3D(Scalar<T> scalingFactor, LinBivector3D<T>? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = scalingFactor / normSquared;

        return LinBivector3D<T>.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> Dual3D()
    {
        return LinBivector3D<T>.Create(
            -Scalar3,
            Scalar2,
            -Scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> Dual3D(Scalar<T> scalingFactor)
    {
        return LinBivector3D<T>.Create(
            -Scalar3 * scalingFactor,
            Scalar2 * scalingFactor,
            -Scalar1 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> UnDual3D()
    {
        return LinBivector3D<T>.Create(
            Scalar3,
            -Scalar2,
            Scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> UnDual3D(Scalar<T> scalingFactor)
    {
        return LinBivector3D<T>.Create(
            Scalar3 * scalingFactor,
            -Scalar2 * scalingFactor,
            Scalar1 * scalingFactor
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> ToVector3D()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Scalar<T>> GetEnumerator()
    {
        yield return Scalar;
        yield return Scalar1;
        yield return Scalar2;
        yield return Scalar12;
        yield return Scalar3;
        yield return Scalar13;
        yield return Scalar23;
        yield return Scalar123;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({Scalar1})<1> + ({Scalar2})<2> + ({Scalar3})<3>";
    }

}