using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;

public class XGaFloat64Processor :
    XGaMetric
{
    public static XGaFloat64EuclideanProcessor Euclidean
        => XGaFloat64EuclideanProcessor.Instance;

    public static XGaFloat64ProjectiveProcessor Projective
        => XGaFloat64ProjectiveProcessor.Instance;

    public static XGaFloat64ConformalProcessor Conformal
        => XGaFloat64ConformalProcessor.Instance;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Processor Create(int negativeCount, int zeroCount)
    {
        return negativeCount switch
        {
            0 when zeroCount == 0 => Euclidean,
            0 when zeroCount == 1 => Projective,
            1 when zeroCount == 0 => Conformal,
            _ => new XGaFloat64Processor(negativeCount, zeroCount)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Processor Create(XGaMetric metric)
    {
        if (metric.IsEuclidean)
            return Euclidean;

        if (metric.IsProjective)
            return Projective;

        if (metric.IsConformal)
            return Conformal;

        return Create(
            metric.NegativeSignatureBasisCount,
            metric.ZeroSignatureBasisCount
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64Processor(int negativeCount, int zeroCount)
        : base(negativeCount, zeroCount)
    {
    }
}