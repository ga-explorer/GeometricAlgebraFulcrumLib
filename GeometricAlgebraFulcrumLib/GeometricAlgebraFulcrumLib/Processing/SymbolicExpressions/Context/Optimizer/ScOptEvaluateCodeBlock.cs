using System.Text;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context.Evaluation;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context.Optimizer
{
    internal sealed class ScOptEvaluateCodeBlock : 
        SymbolicContextProcessorBase
    {
        internal static void Process(SymbolicContext context, SymbolicContextEvaluationData evaluationData)
        {
            var processor = new ScOptEvaluateCodeBlock(context, evaluationData);

            processor.BeginProcessing();
        }


        private readonly SymbolicContextEvaluationData _evaluationData;


        private ScOptEvaluateCodeBlock(SymbolicContext context, SymbolicContextEvaluationData evaluationData)
            : base(context)
        {
            _evaluationData = evaluationData;
        }


        private string ExpressionToString(ISymbolicExpression expr)
        {
            if (expr.IsAtomic)
                return expr.IsVariable
                    ? _evaluationData[expr.HeadText].ToString("G")
                    : expr.HeadText;

            var compositeExpr = (ISymbolicExpressionComposite) expr;

            var s = new StringBuilder();

            s.Append(compositeExpr.HeadText)
                .Append("[");

            var i = 0;
            foreach (var argExpr in compositeExpr.Arguments)
            {
                if (i > 0)
                    s.Append(", ");
                
                s.Append(ExpressionToString(argExpr));

                i++;
            }

            s.Append("]");

            return s.ToString();
        }

        //private double ExpressionToFloat64(ISymbolicExpression expr)
        //{
        //    if (expr is ISymbolicNumber numberExpr)
        //        return numberExpr.RhsExpressionValue;

        //    if (expr is ISymbolicVariable)
        //        return _evaluationData[expr.HeadText];

        //    var compositeExpr = (ISymbolicExpressionComposite) expr;

        //    var s = new StringBuilder();

        //    s.Append(compositeExpr.HeadText)
        //        .Append("[");

        //    var i = 0;
        //    foreach (var argExpr in compositeExpr.Arguments)
        //    {
        //        if (i > 0)
        //            s.Append(", ");
                
        //        s.Append(ExpressionToString(argExpr));

        //        i++;
        //    }

        //    s.Append("]");

        //    return s.ToString();
        //}

        protected override void BeginProcessing()
        {
            foreach (var computedVar in Context.GetComputedVariables())
            {
                _evaluationData[computedVar.InternalName] = 
                    Context.ExpressionEvaluator.EvaluateToFloat64(
                        ExpressionToString(computedVar.RhsExpression)
                    );
            }
        }
    }
}
