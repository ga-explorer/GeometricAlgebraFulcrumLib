using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Symbolic.Mathematica.Expression
{
    public sealed class MathematicaRule : MathematicaExpression
    {
        public static MathematicaRule Create(MathematicaInterface parentCas, Expr lhsMathExpr, Expr rhsMathExpr)
        {
            var e = parentCas[Mfs.Rule[lhsMathExpr, rhsMathExpr]];

            return new MathematicaRule(parentCas, e);
        }

        public static MathematicaRule CreateDelayed(MathematicaInterface parentCas, Expr lhsMathExpr, Expr rhsMathExpr)
        {
            var e = parentCas[Mfs.RuleDelayed[lhsMathExpr, rhsMathExpr]];

            return new MathematicaRule(parentCas, e);
        }


        public bool IsImmediate => Expression.Head.ToString() == Mfs.Rule.ToString();

        public bool IsDelayed => Expression.Head.ToString() == Mfs.RuleDelayed.ToString();

        public Expr LhsExpr => Expression.Args[0];

        public Expr RhsExpr => Expression.Args[1];


        private MathematicaRule(MathematicaInterface parentCas, Expr mathExpr)
            : base(parentCas, mathExpr)
        {
        }


    }
}
