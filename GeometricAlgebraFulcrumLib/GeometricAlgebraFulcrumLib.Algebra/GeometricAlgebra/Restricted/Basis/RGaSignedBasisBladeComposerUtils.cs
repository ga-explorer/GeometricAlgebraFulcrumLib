using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;

public static class RGaSignedBasisBladeComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateZeroBasisScalar(this RGaMetric metric)
    {
        return metric.ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBasisBlade CreatePositiveBasisScalar(this RGaMetric metric)
    {
        return metric.BasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateNegativeBasisScalar(this RGaMetric metric)
    {
        return metric.NegativeBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IRGaSignedBasisBlade CreateSignedBasisScalar(this RGaMetric metric, IntegerSign sign)
    {
        return sign.Value switch
        {
            0 => metric.ZeroBasisScalar,
            > 0 => metric.BasisScalar,
            _ => metric.NegativeBasisScalar
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateZeroBasisVector(this RGaMetric metric)
    {
        var basisBlade = metric.CreateBasisVector(0);

        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateZeroBasisVector(this RGaMetric metric, int index)
    {
        var basisBlade = metric.CreateBasisVector(index);

        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreatePositiveBasisVector(this RGaMetric metric, int index)
    {
        var basisBlade = metric.CreateBasisVector(index);

        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Positive);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateNegativeBasisVector(this RGaMetric metric, int index)
    {
        var basisBlade = metric.CreateBasisVector(index);

        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateSignedBasisVector(this RGaMetric metric, int index, IntegerSign sign)
    {
        return new RGaSignedBasisBlade(
            metric.CreateBasisVector(index),
            sign
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateZeroBasisBivector(this RGaMetric metric)
    {
        var basisBlade = metric.CreateBasisBivector(0, 1);

        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateZeroBasisBivector(this RGaMetric metric, int index1, int index2)
    {
        var basisBlade = index1 < index2
            ? metric.CreateBasisBivector(index1, index2)
            : metric.CreateBasisBivector(index2, index1);

        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreatePositiveBasisBivector(this RGaMetric metric, int index1, int index2)
    {
        var sign = index1 < index2 
            ? IntegerSign.Positive 
            : IntegerSign.Negative;

        var basisBlade = index1 < index2
            ? metric.CreateBasisBivector(index1, index2)
            : metric.CreateBasisBivector(index2, index1);

        return new RGaSignedBasisBlade(basisBlade, sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateNegativeBasisBivector(this RGaMetric metric, int index1, int index2)
    {
        var sign = index1 < index2 
            ? IntegerSign.Negative 
            : IntegerSign.Positive;

        var basisBlade = index1 < index2
            ? metric.CreateBasisBivector(index1, index2)
            : metric.CreateBasisBivector(index2, index1);

        return new RGaSignedBasisBlade(basisBlade, sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateSignedBasisBivector(this RGaMetric metric, int index1, int index2, IntegerSign sign)
    {
        if (index1 > index2) sign = -sign;

        var basisBlade = index1 < index2
            ? metric.CreateBasisBivector(index1, index2)
            : metric.CreateBasisBivector(index2, index1);

        return new RGaSignedBasisBlade(basisBlade, sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateZeroBasisKVector(this RGaMetric metric, ImmutableSortedSet<int> indexSet)
    {
        var basisBlade = metric.CreateBasisBlade(indexSet);

        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreatePositiveBasisKVector(this RGaMetric metric, ImmutableSortedSet<int> indexSet)
    {
        var basisBlade = metric.CreateBasisBlade(indexSet);

        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Positive);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateNegativeBasisKVector(this RGaMetric metric, ImmutableSortedSet<int> indexSet)
    {
        var basisBlade = metric.CreateBasisBlade(indexSet);

        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateSignedBasisKVector(this RGaMetric metric, ImmutableSortedSet<int> indexSet, IntegerSign sign)
    {
        var basisBlade = metric.CreateBasisBlade(indexSet);

        return new RGaSignedBasisBlade(basisBlade, sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateZeroBasisBlade(this RGaMetric metric, int grade)
    {
        return grade switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(grade)),
            0 => metric.ZeroBasisScalar,
            1 => metric.CreateZeroBasisVector(),
            2 => metric.CreateZeroBasisBivector(),
            _ => new RGaSignedBasisBlade(
                metric.CreateBasisBlade(grade.GetRange().ToImmutableSortedSet()), 
                IntegerSign.Zero
            )
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateZeroBasisBlade(this RGaBasisBlade basisBlade)
    {
        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreatePositiveBasisBlade(this RGaBasisBlade basisBlade)
    {
        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Positive);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateNegativeBasisBlade(this RGaBasisBlade basisBlade)
    {
        return new RGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade CreateSignedBasisBlade(this RGaBasisBlade basisBlade, IntegerSign sign)
    {
        return new RGaSignedBasisBlade(basisBlade, sign);
    }

}