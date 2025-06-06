﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text;

public sealed class LaTeXComposerEFloat
    : LaTeXComposer<EFloat>
{
    public static LaTeXComposerEFloat DefaultComposer { get; }
        = new LaTeXComposerEFloat();


    private LaTeXComposerEFloat()
        : base(ScalarProcessorOfEFloat.Instance)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(LinFloat64Angle angle)
    {
        var angleText = GetScalarText(angle.DegreesValue);

        return $"{angleText}^{{\\circ}}";
    }

    public override string GetScalarText(EFloat scalar)
    {
        return scalar.ToString();
    }
}