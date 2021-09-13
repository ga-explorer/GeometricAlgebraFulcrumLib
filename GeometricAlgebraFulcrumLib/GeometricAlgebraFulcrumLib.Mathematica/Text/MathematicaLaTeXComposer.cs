using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Text
{
    public sealed class MathematicaLaTeXComposer
        : LaTeXComposer<Expr>
    {
        public static MathematicaLaTeXComposer DefaultComposer { get; }
            = new MathematicaLaTeXComposer();


        public MathematicaLaTeXComposer() 
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