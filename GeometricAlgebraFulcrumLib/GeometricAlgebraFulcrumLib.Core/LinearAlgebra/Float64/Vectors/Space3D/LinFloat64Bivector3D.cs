using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;

public sealed record LinFloat64Bivector3D :
    ITriplet<double>,
    ILinFloat64KVector3D
{
    public static LinFloat64Bivector3D Zero { get; }
        = new LinFloat64Bivector3D(
            Float64Scalar.Zero,
            Float64Scalar.Zero,
            Float64Scalar.Zero
        );

    public static LinFloat64Bivector3D E12 { get; }
        = new LinFloat64Bivector3D(
            Float64Scalar.One,
            Float64Scalar.Zero,
            Float64Scalar.Zero
        );

    public static LinFloat64Bivector3D E21 { get; }
        = new LinFloat64Bivector3D(
            Float64Scalar.NegativeOne,
            Float64Scalar.Zero,
            Float64Scalar.Zero
        );

    public static LinFloat64Bivector3D E13 { get; }
        = new LinFloat64Bivector3D(
            Float64Scalar.Zero,
            Float64Scalar.One,
            Float64Scalar.Zero
        );

    public static LinFloat64Bivector3D E31 { get; }
        = new LinFloat64Bivector3D(
            Float64Scalar.Zero,
            Float64Scalar.NegativeOne,
            Float64Scalar.Zero
        );

    public static LinFloat64Bivector3D E23 { get; }
        = new LinFloat64Bivector3D(
            Float64Scalar.Zero,
            Float64Scalar.Zero,
            Float64Scalar.One
        );

    public static LinFloat64Bivector3D E32 { get; }
        = new LinFloat64Bivector3D(
            Float64Scalar.Zero,
            Float64Scalar.Zero,
            Float64Scalar.NegativeOne
        );

    public static LinFloat64Bivector3D Symmetric { get; }
        = new LinFloat64Bivector3D(
            Float64Scalar.One,
            Float64Scalar.One,
            Float64Scalar.One
        );

    public static LinFloat64Bivector3D UnitSymmetric { get; }
        = new LinFloat64Bivector3D(
            Float64Scalar.InvSqrt3,
            Float64Scalar.InvSqrt3,
            Float64Scalar.InvSqrt3
        );

    public static IReadOnlyList<LinFloat64Bivector3D> BasisBivectors { get; }
        = new[] { E12, E13, E23 };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D Create(double scalar12, double scalar13, double scalar23)
    {
        return new LinFloat64Bivector3D(scalar12, scalar13, scalar23);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D operator +(LinFloat64Bivector3D mv1)
    {
        return mv1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D operator -(LinFloat64Bivector3D mv1)
    {
        return new LinFloat64Bivector3D(
            -mv1.Scalar12,
            -mv1.Scalar13,
            -mv1.Scalar23
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D operator +(LinFloat64Bivector3D mv1, LinFloat64Bivector3D mv2)
    {
        return new LinFloat64Bivector3D(
            mv1.Scalar12 + mv2.Scalar12,
            mv1.Scalar13 + mv2.Scalar13,
            mv1.Scalar23 + mv2.Scalar23
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D operator -(LinFloat64Bivector3D mv1, LinFloat64Bivector3D mv2)
    {
        return new LinFloat64Bivector3D(
            mv1.Scalar12 - mv2.Scalar12,
            mv1.Scalar13 - mv2.Scalar13,
            mv1.Scalar23 - mv2.Scalar23
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D operator *(LinFloat64Bivector3D mv1, double mv2)
    {
        return new LinFloat64Bivector3D(
            mv1.Scalar12 * mv2,
            mv1.Scalar13 * mv2,
            mv1.Scalar23 * mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D operator *(double mv1, LinFloat64Bivector3D mv2)
    {
        return new LinFloat64Bivector3D(
            mv1 * mv2.Scalar12,
            mv1 * mv2.Scalar13,
            mv1 * mv2.Scalar23
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D operator /(LinFloat64Bivector3D mv1, double mv2)
    {
        mv2 = 1d / mv2;

        return new LinFloat64Bivector3D(
            mv1.Scalar12 * mv2,
            mv1.Scalar13 * mv2,
            mv1.Scalar23 * mv2
        );
    }


    public int VSpaceDimensions
        => 3;

    public int Grade
        => 2;

    public double Item1
        => Scalar12;

    public double Item2
        => Scalar13;

    public double Item3
        => Scalar23;

    public Float64Scalar Xy
        => Scalar12;

    public Float64Scalar Yx
        => -Scalar12;

    public Float64Scalar Xz
        => Scalar13;

    public Float64Scalar Zx
        => -Scalar13;

    public Float64Scalar Yz
        => Scalar23;

    public Float64Scalar Zy
        => -Scalar23;

    public Float64Scalar Scalar
        => Float64Scalar.Zero;

    public Float64Scalar Scalar1
        => Float64Scalar.Zero;

    public Float64Scalar Scalar2
        => Float64Scalar.Zero;

    public Float64Scalar Scalar3
        => Float64Scalar.Zero;

    public Float64Scalar Scalar12 { get; }

    public Float64Scalar Scalar13 { get; }

    public Float64Scalar Scalar23 { get; }

    public Float64Scalar Scalar123
        => Float64Scalar.Zero;

    public int Count
        => 8;

    /// <summary>
    /// Get or set the ith component of this multivector
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Float64Scalar this[int index]
    {
        get
        {
            if (index is < 0 or > 7)
                throw new IndexOutOfRangeException();

            return index switch
            {
                3 => Scalar12,
                5 => Scalar13,
                6 => Scalar23,
                _ => Float64Scalar.Zero
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Bivector3D(Float64Scalar scalar12, Float64Scalar scalar13, Float64Scalar scalar23)
    {
        Scalar12 = scalar12;
        Scalar13 = scalar13;
        Scalar23 = scalar23;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar12.IsValid() &&
               Scalar13.IsValid() &&
               Scalar23.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Scalar12.IsZero() &&
               Scalar13.IsZero() &&
               Scalar23.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double zeroEpsilon = 1E-12)
    {
        return Norm().IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Norm()
    {
        return NormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar NormSquared()
    {
        return Scalar12.Square() +
               Scalar13.Square() +
               Scalar23.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D ToUnitBivector(bool zeroAsSymmetric = true)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroAsSymmetric ? UnitSymmetric : Zero;

        return this / normSquared.Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Multivector3D ToMultivector3D()
    {
        return LinFloat64Multivector3D.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D Negative()
    {
        return new LinFloat64Bivector3D(
            -Scalar12,
            -Scalar13,
            -Scalar23
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D Negative(double scalingFactor)
    {
        return new LinFloat64Bivector3D(
            -Scalar12 * scalingFactor,
            -Scalar13 * scalingFactor,
            -Scalar23 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D Inverse()
    {
        return Negative(1d / NormSquared().ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D DirectionToUnitNormal3D(LinFloat64Vector3D? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / norm.ScalarValue;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D DirectionToNormal3D(LinFloat64Vector3D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / normSquared.ScalarValue;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D DirectionToNormal3D(double scalingFactor, LinFloat64Vector3D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = scalingFactor / normSquared.ScalarValue;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D NormalToUnitDirection3D(LinFloat64Vector3D? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / norm.ScalarValue;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D NormalToDirection3D(LinFloat64Vector3D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / normSquared.ScalarValue;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D NormalToDirection3D(double scalingFactor, LinFloat64Vector3D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = scalingFactor / normSquared.ScalarValue;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D Dual3D()
    {
        return LinFloat64Vector3D.Create(
            Scalar23,
            -Scalar13,
            Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D Dual3D(double scalingFactor)
    {
        return LinFloat64Vector3D.Create(
            Scalar23 * scalingFactor,
            -Scalar13 * scalingFactor,
            Scalar12 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D UnDual3D()
    {
        return LinFloat64Vector3D.Create(
            -Scalar23,
            Scalar13,
            -Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D UnDual3D(double scalingFactor)
    {
        return LinFloat64Vector3D.Create(
            -Scalar23 * scalingFactor,
            Scalar13 * scalingFactor,
            -Scalar12 * scalingFactor
        );
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
        return $"({Scalar12:G10})<1,2> + ({Scalar13:G10})<1,3> + ({Scalar23:G10})<2,3>";
    }

}