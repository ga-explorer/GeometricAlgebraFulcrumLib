using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators
{
    public interface IMetaExpressionEvaluator
    {
        MetaContext Context { get; }

        /// <summary>
        /// Try to simplify the given expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        IMetaExpression Simplify(IMetaExpression expr);

        /// <summary>
        /// Try to simplify the given expression text
        /// </summary>
        /// <param name="exprText"></param>
        /// <returns></returns>
        IMetaExpression Simplify(string exprText);

        /// <summary>
        /// Try to find the numerical value of the given expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        double EvaluateToFloat64(IMetaExpression expr);

        /// <summary>
        /// Try to find the numerical value of the given expression text
        /// </summary>
        /// <param name="exprText"></param>
        /// <returns></returns>
        double EvaluateToFloat64(string exprText);
    }

    public interface IMetaExpressionEvaluator<T> :
        IMetaExpressionEvaluator where T : class
    {
        ISymbolicFromMetaExpressionConverter<T> FromSymbolicExpressionConverter { get; }

        ISymbolicIntoMetaExpressionConverter<T> IntoSymbolicExpressionConverter { get; }
    }
}