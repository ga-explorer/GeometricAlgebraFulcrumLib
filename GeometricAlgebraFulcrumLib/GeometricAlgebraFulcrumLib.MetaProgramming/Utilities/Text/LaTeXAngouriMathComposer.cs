using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Processors;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Text;

public sealed class LaTeXAngouriMathComposer
    : LaTeXComposer<Entity>
{
    public static LaTeXAngouriMathComposer DefaultComposer { get; }
        = new LaTeXAngouriMathComposer();


    private LaTeXAngouriMathComposer()
        : base(ScalarProcessorOfAngouriMathEntity.Instance)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(LinFloat64Angle angle)
    {
        return $"{angle.DegreesValue}^{{\\circ}}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetScalarText(Entity scalar)
    {
        return scalar.Latexise();
    }
}