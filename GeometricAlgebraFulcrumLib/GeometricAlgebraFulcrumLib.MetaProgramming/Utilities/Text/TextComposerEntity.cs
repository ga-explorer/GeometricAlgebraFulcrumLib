using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Processors;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Text;

public sealed class TextComposerEntity
    : TextComposer<Entity>
{
    public static TextComposerEntity DefaultComposer { get; }
        = new TextComposerEntity();


    private TextComposerEntity()
        : base(ScalarProcessorOfAngouriMathEntity.Instance)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(LinFloat64Angle angle)
    {
        return $"{angle.DegreesValue} degrees";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetScalarText(Entity scalar)
    {
        return scalar.ToString();
    }
}