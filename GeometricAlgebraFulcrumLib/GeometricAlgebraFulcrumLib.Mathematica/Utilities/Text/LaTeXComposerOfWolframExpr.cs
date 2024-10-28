using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using Wolfram.NETLink;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra;

namespace GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;

public sealed class LaTeXComposerOfWolframExpr
    : LaTeXComposer<Expr>
{
    public static LaTeXComposerOfWolframExpr DefaultComposer { get; }
        = new LaTeXComposerOfWolframExpr();


    public LaTeXComposerOfWolframExpr()
        : base(ScalarProcessorOfWolframExpr.Instance)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetAngleText(LinFloat64Angle angle)
    {
        return $@"{GetScalarText(angle.DegreesValue.ToExpr())} \degree";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string GetScalarText(Expr scalar)
    {
        if (scalar is null)
            return "0";

        return MathematicaUtils.Cas.Connection.EvaluateToString(
            Mfs.EToString[Mfs.TeXForm[Mfs.HoldForm[scalar]]]
        );
    }

}