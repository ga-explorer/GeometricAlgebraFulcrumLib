using System.Runtime.CompilerServices;
using Wolfram.NETLink;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;

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