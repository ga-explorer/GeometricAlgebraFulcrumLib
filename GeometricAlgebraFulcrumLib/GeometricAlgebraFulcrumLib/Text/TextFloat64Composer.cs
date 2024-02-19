﻿using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Text;

public sealed class TextFloat64Composer
    : TextComposer<double>
{
    public static TextFloat64Composer DefaultComposer { get; }
        = new TextFloat64Composer();


    public int RoundingDecimals { get; set; }
        = 15;


    private TextFloat64Composer() 
        : base(ScalarProcessorOfFloat64.DefaultProcessor)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(Float64PlanarAngle angle)
    {
        return $"{GetScalarText(angle.Degrees)} degrees";
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetScalarText(double scalar)
    {
        return Math.Round(scalar, RoundingDecimals).ToString("G");
    }
}