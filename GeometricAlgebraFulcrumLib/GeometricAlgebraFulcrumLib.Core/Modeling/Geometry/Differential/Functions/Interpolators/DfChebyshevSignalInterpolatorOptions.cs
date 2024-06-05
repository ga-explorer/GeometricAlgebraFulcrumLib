﻿namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Interpolators;

public sealed class DfChebyshevSignalInterpolatorOptions :
    DfSignalInterpolatorOptions
{
    public int PolynomialDegree { get; set; } = 39;
}