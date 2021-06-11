using GeometricAlgebraLib.Symbolic.Processors;
using GeometricAlgebraLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraLib.Symbolic.Text
{
    public sealed class GaTextComposerMathematicaExpr
        : GaTextComposer<Expr>
    {
        public static GaTextComposerMathematicaExpr DefaultComposer { get; }
            = new();


        public GaTextComposerMathematicaExpr() 
            : base(GaScalarProcessorMathematicaExpr.DefaultProcessor)
        {
        }


        public override string GetScalarText(Expr scalar)
        {
            return scalar.ToString();
        }
    }
}