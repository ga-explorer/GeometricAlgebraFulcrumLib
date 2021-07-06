using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Symbolic.Processors;
using GeometricAlgebraFulcrumLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Symbolic.Text
{
    public sealed class GaLaTeXComposerMathematicaExpr
        : GaLaTeXComposer<Expr>
    {
        public static GaLaTeXComposerMathematicaExpr DefaultComposer { get; }
            = new GaLaTeXComposerMathematicaExpr();


        public GaLaTeXComposerMathematicaExpr() 
            : base(GaScalarProcessorMathematicaExpr.DefaultProcessor)
        {
        }

        public override string GetScalarText(Expr scalar)
        {
            return GaSymbolicUtils.Cas.Connection.EvaluateToString(
                Mfs.EToString[Mfs.TeXForm[scalar]]
            );
        }
    }
}