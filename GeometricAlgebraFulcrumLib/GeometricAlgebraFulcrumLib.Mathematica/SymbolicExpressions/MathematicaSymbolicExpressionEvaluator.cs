using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Evaluators;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.SymbolicExpressions
{
    public sealed class MathematicaSymbolicExpressionEvaluator :
        ISymbolicExpressionEvaluator<Expr>
    {
        public SymbolicContext Context { get; }
        
        public IFromSymbolicExpressionConverter<Expr> FromSymbolicExpressionConverter { get; }
        
        public IIntoSymbolicExpressionConverter<Expr> IntoSymbolicExpressionConverter { get; }


        internal MathematicaSymbolicExpressionEvaluator([NotNull] SymbolicContext context)
        {
            Context = context;
            FromSymbolicExpressionConverter = new MathematicsFromSymbolicExpressionConverter(context);
            IntoSymbolicExpressionConverter = new MathematicsIntoSymbolicExpressionConverter(context);
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

        public double EvaluateToFloat64(ISymbolicExpression expr)
        {
            return MathematicaInterface
                .DefaultCas
                .Connection
                .EvaluateToExpr($"{expr}")
                .ToNumber();
        }

        public double EvaluateToFloat64(string exprText)
        {
            return MathematicaInterface
                .DefaultCas
                .Connection
                .EvaluateToExpr(exprText)
                .ToNumber();
        }
    }
}
