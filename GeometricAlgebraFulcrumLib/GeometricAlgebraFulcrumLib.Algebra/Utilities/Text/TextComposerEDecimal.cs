using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

public sealed class TextComposerEDecimal
    : TextComposer<EDecimal>
{
    public static TextComposerEDecimal DefaultComposer { get; }
        = new TextComposerEDecimal();


    private TextComposerEDecimal()
        : base(ScalarProcessorOfEDecimal.Instance)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(LinFloat64Angle angle)
    {
        return $"{GetScalarText(EDecimal.FromDouble(angle.DegreesValue))} degrees";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetScalarText(EDecimal scalar)
    {
        return scalar.ToString();
    }
}