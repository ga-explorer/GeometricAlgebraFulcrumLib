using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

public sealed class TextComposerERational
    : TextComposer<ERational>
{
    public static TextComposerERational DefaultComposer { get; }
        = new TextComposerERational();


    private TextComposerERational()
        : base(ScalarProcessorOfERational.Instance)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(LinFloat64Angle angle)
    {
        return $"{GetScalarText(ERational.FromDouble(angle.DegreesValue))} degrees";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetScalarText(ERational scalar)
    {
        return scalar.ToString();
    }
}