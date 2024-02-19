﻿using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Text;

public sealed class LaTeXAngouriMathComposer
    : MathBase.Text.LaTeXComposer<Entity>
{
    public static LaTeXAngouriMathComposer DefaultComposer { get; }
        = new LaTeXAngouriMathComposer();
        

    private LaTeXAngouriMathComposer()
        : base(ScalarProcessorOfAngouriMathEntity.DefaultProcessor)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(Float64PlanarAngle angle)
    {
        return $"{angle.Degrees}^{{\\circ}}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetScalarText(Entity scalar)
    {
        return scalar.Latexise();
    }
}