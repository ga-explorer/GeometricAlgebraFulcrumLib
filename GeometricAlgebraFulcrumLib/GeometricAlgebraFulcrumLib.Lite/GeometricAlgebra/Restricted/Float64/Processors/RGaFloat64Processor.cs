﻿using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

public class RGaFloat64Processor :
    RGaMetric
{
    public static RGaFloat64EuclideanProcessor Euclidean
        => RGaFloat64EuclideanProcessor.Instance;

    public static RGaFloat64ProjectiveProcessor Projective
        => RGaFloat64ProjectiveProcessor.Instance;

    public static RGaFloat64ConformalProcessor Conformal
        => RGaFloat64ConformalProcessor.Instance;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Processor Create(int negativeCount, int zeroCount)
    {
        return negativeCount switch
        {
            0 when zeroCount == 0 => Euclidean,
            0 when zeroCount == 1 => Projective,
            1 when zeroCount == 0 => Conformal,
            _ => new RGaFloat64Processor(negativeCount, zeroCount)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Processor Create(RGaMetric metric)
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


    protected RGaFloat64Processor(int negativeCount, int zeroCount)
        : base(negativeCount, zeroCount)
    {
    }
}