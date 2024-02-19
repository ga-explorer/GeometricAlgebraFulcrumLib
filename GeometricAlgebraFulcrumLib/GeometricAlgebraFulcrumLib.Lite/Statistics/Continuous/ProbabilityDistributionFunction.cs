﻿using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Statistics.Continuous;

public sealed class ProbabilityDistributionFunction
{
    public static ProbabilityDistributionFunction CreateNormal(double mean, double variance, double zeroEpsilon = 1e-12)
    {
        var sqrt2Pi = Math.Sqrt(2 * Math.PI);
        var s = Math.Sqrt(variance);

        var deltaX = s * Math.Sqrt(
            -2d * Math.Log(
                s * zeroEpsilon * sqrt2Pi
            )
        );

        var pwaFunction = PiecewiseAffineFunction.CreateContinuous(
            x => Math.Exp(-0.5d * x * x / variance) / (s * sqrt2Pi),
            0,
            deltaX,
            1025,
            0.1 * Math.PI / 180
        ).MakeEven().ShiftX(mean).MakeFinite();

        return new ProbabilityDistributionFunction(pwaFunction);
    }


    public PiecewiseAffineFunction PwaFunction { get; }


    private ProbabilityDistributionFunction(PiecewiseAffineFunction pwaFunction)
    {
        PwaFunction = pwaFunction;

        var area = PwaFunction.GetArea();

        if (!area.IsNearEqual(1))
            PwaFunction.ScaleY(1d / area);
    }
}