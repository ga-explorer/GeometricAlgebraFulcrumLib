using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Text
{
    public sealed class LaTeXMathematicaComposer
        : LaTeXComposer<Expr>
    {
        public static LaTeXMathematicaComposer DefaultComposer { get; }
            = new LaTeXMathematicaComposer();


        public LaTeXMathematicaComposer() 
            : base(ScalarAlgebraMathematicaProcessor.DefaultProcessor)
        {
        }

        public override string GetScalarText(Expr scalar)
        {
            return MathematicaUtils.Cas.Connection.EvaluateToString(
                Mfs.EToString[Mfs.TeXForm[scalar]]
            );
        }
    }
}