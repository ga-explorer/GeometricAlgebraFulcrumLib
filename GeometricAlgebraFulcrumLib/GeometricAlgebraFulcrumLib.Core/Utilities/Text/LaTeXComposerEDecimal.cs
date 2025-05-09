using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text;

public sealed class LaTeXComposerEDecimal
    : LaTeXComposer<EDecimal>
{
    public static LaTeXComposerEDecimal DefaultComposer { get; }
        = new LaTeXComposerEDecimal();


    private LaTeXComposerEDecimal()
        : base(ScalarProcessorOfEDecimal.Instance)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(LinFloat64Angle angle)
    {
        var angleText = GetScalarText(EDecimal.FromDouble(angle.DegreesValue));

        return $"{angleText}^{{\\circ}}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetScalarText(EDecimal scalar)
    {
        return scalar.ToString();
    }
}