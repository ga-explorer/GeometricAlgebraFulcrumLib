using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text;

public sealed class LaTeXComposerERational
    : LaTeXComposer<ERational>
{
    public static LaTeXComposerERational DefaultComposer { get; }
        = new LaTeXComposerERational();


    private LaTeXComposerERational()
        : base(ScalarProcessorOfERational.Instance)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(LinFloat64Angle angle)
    {
        var angleText = GetScalarText(angle.DegreesValue);

        return $"{angleText}^{{\\circ}}";
    }

    public override string GetScalarText(ERational scalar)
    {
        return scalar.ToString();
    }
}