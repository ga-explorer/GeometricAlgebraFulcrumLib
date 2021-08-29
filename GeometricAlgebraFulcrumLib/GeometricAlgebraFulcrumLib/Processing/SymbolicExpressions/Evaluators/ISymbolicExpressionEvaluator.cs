using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Evaluators
{
    public interface ISymbolicExpressionEvaluator
    {
        SymbolicContext Context { get; }

        /// <summary>
        /// Try to simplify the given expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        ISymbolicExpression Simplify(ISymbolicExpression expr);

        /// <summary>
        /// Try to simplify the given expression text
        /// </summary>
        /// <param name="exprText"></param>
        /// <returns></returns>
        ISymbolicExpression Simplify(string exprText);

        /// <summary>
        /// Try to find the numerical value of the given expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        double EvaluateToFloat64(ISymbolicExpression expr);

        /// <summary>
        /// Try to find the numerical value of the given expression text
        /// </summary>
        /// <param name="exprText"></param>
        /// <returns></returns>
        double EvaluateToFloat64(string exprText);
    }

    public interface ISymbolicExpressionEvaluator<T> :
        ISymbolicExpressionEvaluator where T : class
    {
        IFromSymbolicExpressionConverter<T> FromSymbolicExpressionConverter { get; }

        IIntoSymbolicExpressionConverter<T> IntoSymbolicExpressionConverter { get; }
    }
}