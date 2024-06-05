using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public sealed record LinFloat64Trivector3D :
    ILinFloat64KVector3D
{
    public static LinFloat64Trivector3D Zero { get; }
        = new LinFloat64Trivector3D(Float64Scalar.Zero);

    public static LinFloat64Trivector3D E123 { get; }
        = new LinFloat64Trivector3D(Float64Scalar.One);

    public static LinFloat64Trivector3D NegativeE123 { get; }
        = new LinFloat64Trivector3D(Float64Scalar.NegativeOne);

    public static LinFloat64Trivector3D InverseE123 { get; }
        = new LinFloat64Trivector3D(Float64Scalar.NegativeOne);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D Create(double scalar123)
    {
        return new LinFloat64Trivector3D(scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D Create(Float64Scalar scalar123)
    {
        return new LinFloat64Trivector3D(scalar123);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D operator +(LinFloat64Trivector3D mv)
    {
        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D operator -(LinFloat64Trivector3D mv)
    {
        return new LinFloat64Trivector3D(
            -mv.Scalar123
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D operator +(LinFloat64Trivector3D mv1, LinFloat64Trivector3D mv2)
    {
        return new LinFloat64Trivector3D(
            mv1.Scalar123 + mv2.Scalar123
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D operator -(LinFloat64Trivector3D mv1, LinFloat64Trivector3D mv2)
    {
        return new LinFloat64Trivector3D(
            mv1.Scalar123 - mv2.Scalar123
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D operator *(LinFloat64Trivector3D mv1, double mv2)
    {
        return new LinFloat64Trivector3D(
            mv1.Scalar123 * mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D operator *(double mv1, LinFloat64Trivector3D mv2)
    {
        return new LinFloat64Trivector3D(
            mv1 * mv2.Scalar123
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D operator /(LinFloat64Trivector3D mv1, double mv2)
    {
        return new LinFloat64Trivector3D(
            mv1.Scalar123 / mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D operator /(double mv1, LinFloat64Trivector3D mv2)
    {
        return new LinFloat64Trivector3D(
            mv1 / -mv2.Scalar123
        );
    }


    public int VSpaceDimensions
        => 3;

    public int Grade
        => 3;

    public Float64Scalar Scalar
        => Float64Scalar.Zero;

    public Float64Scalar Scalar1
        => Float64Scalar.Zero;

    public Float64Scalar Scalar2
        => Float64Scalar.Zero;

    public Float64Scalar Scalar3
        => Float64Scalar.Zero;

    public Float64Scalar Scalar12
        => Float64Scalar.Zero;

    public Float64Scalar Scalar13
        => Float64Scalar.Zero;

    public Float64Scalar Scalar23
        => Float64Scalar.Zero;

    public Float64Scalar Scalar123 { get; }

    public int Count
        => 8;

    public Float64Scalar this[int index]
    {
        get
        {
            if (index is < 0 or > 7)
                throw new IndexOutOfRangeException();

            return index == 7
                ? Scalar123
                : Float64Scalar.Zero;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Trivector3D(Float64Scalar scalar123)
    {
        Scalar123 = scalar123;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar123.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Scalar123.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double epsilon = 1e-12d)
    {
        return Scalar123.IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Norm()
    {
        return Scalar123.Abs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar NormSquared()
    {
        return Scalar123.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Multivector3D ToMultivector3D()
    {
        return LinFloat64Multivector3D.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D Negative()
    {
        return new LinFloat64Trivector3D(-Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D GradeInvolution()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D CliffordConjugate()
    {
        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D DirectionToUnitNormal3D()
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return Scalar123.IsPositive()
            ? LinFloat64Scalar3D.E0
            : LinFloat64Scalar3D.NegativeE0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D DirectionToNormal3D()
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return LinFloat64Scalar3D.Create(1d / Scalar123.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D DirectionToNormal3D(double scalingFactor)
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return LinFloat64Scalar3D.Create(scalingFactor / Scalar123.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D NormalToUnitDirection3D()
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return Scalar123.IsPositive()
            ? LinFloat64Scalar3D.E0
            : LinFloat64Scalar3D.NegativeE0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D NormalToDirection3D()
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return LinFloat64Scalar3D.Create(1d / Scalar123.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D NormalToDirection3D(double scalingFactor)
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return LinFloat64Scalar3D.Create(scalingFactor / Scalar123.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D Dual3D()
    {
        return LinFloat64Scalar3D.Create(-Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D Dual3D(double scalingFactor)
    {
        return LinFloat64Scalar3D.Create(-Scalar123 * scalingFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D UnDual3D()
    {
        return LinFloat64Scalar3D.Create(Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D UnDual3D(double scalingFactor)
    {
        return LinFloat64Scalar3D.Create(Scalar123 * scalingFactor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Scalar> GetEnumerator()
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
        return $"({Scalar123})<1,2,3>";
    }

}