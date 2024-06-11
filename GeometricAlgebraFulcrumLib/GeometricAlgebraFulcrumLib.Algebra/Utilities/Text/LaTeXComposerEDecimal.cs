using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

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