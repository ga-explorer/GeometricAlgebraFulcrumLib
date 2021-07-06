using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Symbolic.Mathematica.Expression
{
    public sealed class MathematicaPattern : MathematicaExpression
    {
        private MathematicaPattern(MathematicaInterface parentCas, Expr mathExpr)
            : base(parentCas, mathExpr)
        {
        }

    }
}
