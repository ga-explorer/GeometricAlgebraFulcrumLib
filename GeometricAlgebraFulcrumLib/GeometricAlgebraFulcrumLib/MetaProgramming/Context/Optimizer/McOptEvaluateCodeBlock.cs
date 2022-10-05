using System.Text;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Evaluation;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer
{
    internal sealed class McOptEvaluateCodeBlock : 
        MetaContextProcessorBase
    {
        internal static void Process(MetaContext context, MetaContextEvaluationData evaluationData)
        {
            var processor = new McOptEvaluateCodeBlock(context, evaluationData);

            processor.BeginProcessing();
        }


        private readonly MetaContextEvaluationData _evaluationData;


        private McOptEvaluateCodeBlock(MetaContext context, MetaContextEvaluationData evaluationData)
            : base(context)
        {
            _evaluationData = evaluationData;
        }


        private string ExpressionToString(IMetaExpression expr)
        {
            if (expr.IsAtomic)
                return expr.IsVariable
                    ? _evaluationData[expr.HeadText].ToString("G")
                    : expr.HeadText;

            var compositeExpr = (IMetaExpressionComposite) expr;

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
                    Context.SymbolicEvaluator.EvaluateToFloat64(
                        ExpressionToString(computedVar.RhsExpression)
                    );
            }
        }
    }
}
