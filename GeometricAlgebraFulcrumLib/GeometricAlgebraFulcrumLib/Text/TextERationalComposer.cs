﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text;

public sealed class TextERationalComposer
    : TextComposer<ERational>
{
    public static TextERationalComposer DefaultComposer { get; }
        = new TextERationalComposer();


    private TextERationalComposer() 
        : base(ScalarProcessorOfERational.DefaultProcessor)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(Float64PlanarAngle angle)
    {
        return $"{GetScalarText(ERational.FromDouble(angle.Degrees))} degrees";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetScalarText(ERational scalar)
    {
        return scalar.ToString();
    }
}