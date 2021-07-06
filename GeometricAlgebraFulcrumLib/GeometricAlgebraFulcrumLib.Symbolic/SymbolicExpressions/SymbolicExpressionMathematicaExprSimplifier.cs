using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;

namespace GeometricAlgebraFulcrumLib.Symbolic.SymbolicExpressions
{
    public sealed class SymbolicExpressionMathematicaExprSimplifier :
        ISymbolicExpressionSimplifier
    {
        public SymbolicContext Context { get; }


        public SymbolicExpressionMathematicaExprSimplifier([NotNull] SymbolicContext context)
        {
            Context = context;
        }


        public ISymbolicExpression Simplify(ISymbolicExpression expr)
        {
            return MathematicaInterface
                .DefaultCas
                .Connection
                .EvaluateToExpr($"Simplify[{expr}]")
                .ToSymbolicExpression(Context);
        }

        public ISymbolicExpression Simplify(string exprText)
        {
            return MathematicaInterface
                .DefaultCas
                .Connection
                .EvaluateToExpr($"Simplify[{exprText}]")
                .ToSymbolicExpression(Context);
        }

        public double Evaluate(ISymbolicExpression expr)
        {
            return MathematicaInterface
                .DefaultCas
                .Connection
                .EvaluateToExpr($"{expr}")
                .ToNumber();
        }

        public double Evaluate(string exprText)
        {
            return MathematicaInterface
                .DefaultCas
                .Connection
                .EvaluateToExpr(exprText)
                .ToNumber();
        }
    }
}
