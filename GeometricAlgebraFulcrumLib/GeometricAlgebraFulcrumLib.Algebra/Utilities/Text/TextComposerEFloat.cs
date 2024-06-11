using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

public sealed class TextComposerEFloat
    : TextComposer<EFloat>
{
    public static TextComposerEFloat DefaultComposer { get; }
        = new TextComposerEFloat();


    private TextComposerEFloat()
        : base(ScalarProcessorOfEFloat.Instance)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(LinFloat64Angle angle)
    {
        return $"{GetScalarText(EFloat.FromDouble(angle.DegreesValue))} degrees";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetScalarText(EFloat scalar)
    {
        return scalar.ToString();
    }
}