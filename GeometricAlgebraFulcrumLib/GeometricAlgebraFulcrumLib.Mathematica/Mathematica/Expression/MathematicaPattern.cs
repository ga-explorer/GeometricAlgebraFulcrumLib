using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Mathematica.Expression;

public sealed class MathematicaPattern : MathematicaExpression
{
    private MathematicaPattern(MathematicaInterface parentCas, Expr mathExpr)
        : base(parentCas, mathExpr)
    {
    }

}