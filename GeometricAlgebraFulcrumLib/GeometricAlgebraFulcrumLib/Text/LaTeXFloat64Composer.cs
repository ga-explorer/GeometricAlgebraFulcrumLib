﻿using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Text;

public sealed class LaTeXFloat64Composer
    : LaTeXComposer<double>
{
    public static LaTeXFloat64Composer DefaultComposer { get; }
        = new LaTeXFloat64Composer();


    public int ScalarDecimals { get; set; }
        = 7;

    public double ZeroEpsilon { get; set; }
        = 1e-12d;


    private LaTeXFloat64Composer()
        : base(ScalarProcessorOfFloat64.DefaultProcessor)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(Float64PlanarAngle angle)
    {
        var angleText = GetScalarText(angle.Degrees);

        return $"{angleText}^{{\\circ}}";
    }

    public override string GetScalarText(double scalar)
    {
        if (scalar.IsNearZero(ZeroEpsilon))
            return "0";

        var valueText = scalar.ToString("G");

        if (!valueText.Contains("E")) 
            return Math.Round(scalar, ScalarDecimals).ToString("G");

        var ePosition = valueText.IndexOf('E');

        var magnitude = double.Parse(valueText[..ePosition]);
        var magnitudeText = Math.Round(magnitude, ScalarDecimals).ToString("G");
        var exponentText = valueText[(ePosition + 1)..];

        return $@"{magnitudeText} \times 10^{{{exponentText}}}";
    }
}