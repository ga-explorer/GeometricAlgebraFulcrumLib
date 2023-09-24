using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.MathBase.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra
{
    public sealed class LaTeXComposerExpr
        : LaTeXComposer<Expr>
    {
        public static LaTeXComposerExpr DefaultComposer { get; }
            = new LaTeXComposerExpr();


        public LaTeXComposerExpr() 
            : base(ScalarProcessorOfWolframExpr.DefaultProcessor)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(Float64PlanarAngle angle)
        {
            return $@"{GetScalarText(angle.Degrees.ToExpr())} \degree";
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
}