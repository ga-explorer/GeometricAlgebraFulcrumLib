using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

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