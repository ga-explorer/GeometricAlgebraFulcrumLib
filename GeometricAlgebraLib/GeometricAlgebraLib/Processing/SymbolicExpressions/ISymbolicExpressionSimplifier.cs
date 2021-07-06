using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions
{
    public interface ISymbolicExpressionSimplifier
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
        double Evaluate(ISymbolicExpression expr);

        /// <summary>
        /// Try to find the numerical value of the given expression text
        /// </summary>
        /// <param name="exprText"></param>
        /// <returns></returns>
        double Evaluate(string exprText);
    }
}