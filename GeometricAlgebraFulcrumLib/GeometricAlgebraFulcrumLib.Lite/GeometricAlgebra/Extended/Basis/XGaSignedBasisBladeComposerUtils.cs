using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;

public static class XGaSignedBasisBladeComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateZeroBasisScalar(this XGaMetric metric)
    {
        return metric.ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBasisBlade CreatePositiveBasisScalar(this XGaMetric metric)
    {
        return metric.BasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateNegativeBasisScalar(this XGaMetric metric)
    {
        return metric.NegativeBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IXGaSignedBasisBlade CreateSignedBasisScalar(this XGaMetric metric, IntegerSign sign)
    {
        return sign.Value switch
        {
            0 => metric.ZeroBasisScalar,
            > 0 => metric.BasisScalar,
            _ => metric.NegativeBasisScalar
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateZeroBasisVector(this XGaMetric metric)
    {
        var basisBlade = metric.CreateBasisVector(0);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateZeroBasisVector(this XGaMetric metric, int index)
    {
        var basisBlade = metric.CreateBasisVector(index);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreatePositiveBasisVector(this XGaMetric metric, int index)
    {
        var basisBlade = metric.CreateBasisVector(index);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Positive);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateNegativeBasisVector(this XGaMetric metric, int index)
    {
        var basisBlade = metric.CreateBasisVector(index);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateSignedBasisVector(this XGaMetric metric, int index, IntegerSign sign)
    {
        return new XGaSignedBasisBlade(
            metric.CreateBasisVector(index),
            sign
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateZeroBasisBivector(this XGaMetric metric)
    {
        var basisBlade = metric.CreateBasisBivector(0, 1);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateZeroBasisBivector(this XGaMetric metric, int index1, int index2)
    {
        var basisBlade = index1 < index2
            ? metric.CreateBasisBivector(index1, index2)
            : metric.CreateBasisBivector(index2, index1);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreatePositiveBasisBivector(this XGaMetric metric, int index1, int index2)
    {
        var sign = index1 < index2 
            ? IntegerSign.Positive 
            : IntegerSign.Negative;

        var basisBlade = index1 < index2
            ? metric.CreateBasisBivector(index1, index2)
            : metric.CreateBasisBivector(index2, index1);

        return new XGaSignedBasisBlade(basisBlade, sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateNegativeBasisBivector(this XGaMetric metric, int index1, int index2)
    {
        var sign = index1 < index2 
            ? IntegerSign.Negative 
            : IntegerSign.Positive;

        var basisBlade = index1 < index2
            ? metric.CreateBasisBivector(index1, index2)
            : metric.CreateBasisBivector(index2, index1);

        return new XGaSignedBasisBlade(basisBlade, sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateSignedBasisBivector(this XGaMetric metric, int index1, int index2, IntegerSign sign)
    {
        if (index1 > index2) sign = -sign;

        var basisBlade = index1 < index2
            ? metric.CreateBasisBivector(index1, index2)
            : metric.CreateBasisBivector(index2, index1);

        return new XGaSignedBasisBlade(basisBlade, sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateZeroBasisKVector(this XGaMetric metric, ImmutableSortedSet<int> indexSet)
    {
        var basisBlade = metric.CreateBasisBlade(indexSet);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreatePositiveBasisKVector(this XGaMetric metric, ImmutableSortedSet<int> indexSet)
    {
        var basisBlade = metric.CreateBasisBlade(indexSet);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Positive);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateNegativeBasisKVector(this XGaMetric metric, ImmutableSortedSet<int> indexSet)
    {
        var basisBlade = metric.CreateBasisBlade(indexSet);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateSignedBasisKVector(this XGaMetric metric, ImmutableSortedSet<int> indexSet, IntegerSign sign)
    {
        var basisBlade = metric.CreateBasisBlade(indexSet);

        return new XGaSignedBasisBlade(basisBlade, sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateZeroBasisBlade(this XGaMetric metric, int grade)
    {
        return grade switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(grade)),
            0 => metric.ZeroBasisScalar,
            1 => metric.CreateZeroBasisVector(),
            2 => metric.CreateZeroBasisBivector(),
            _ => new XGaSignedBasisBlade(
                metric.CreateBasisBlade(grade.GetRange().ToImmutableSortedSet()), 
                IntegerSign.Zero
            )
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateZeroBasisBlade(this XGaBasisBlade basisBlade)
    {
        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreatePositiveBasisBlade(this XGaBasisBlade basisBlade)
    {
        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Positive);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateNegativeBasisBlade(this XGaBasisBlade basisBlade)
    {
        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade CreateSignedBasisBlade(this XGaBasisBlade basisBlade, IntegerSign sign)
    {
        return new XGaSignedBasisBlade(basisBlade, sign);
    }

}