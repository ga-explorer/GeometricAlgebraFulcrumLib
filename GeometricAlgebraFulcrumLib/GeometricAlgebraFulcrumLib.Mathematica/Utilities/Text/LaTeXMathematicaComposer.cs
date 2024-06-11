using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;


//using GeometricAlgebraFulcrumLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;

public sealed class LaTeXMathematicaComposer
    : LaTeXComposer<Expr>
{
    public static LaTeXMathematicaComposer DefaultComposer { get; }
        = new LaTeXMathematicaComposer();


    public LaTeXMathematicaComposer()
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