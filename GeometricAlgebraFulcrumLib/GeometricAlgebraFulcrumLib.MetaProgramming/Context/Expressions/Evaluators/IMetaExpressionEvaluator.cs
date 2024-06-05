namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;

public interface IMetaExpressionEvaluator
{
    MetaContext Context { get; }

    /// <summary>
    /// True if the given expression is an affine combination of some variables
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public bool IsAffineCombination(IMetaExpression expr);

    /// <summary>
    /// Prepare input expression for code generation
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public IMetaExpression Enhance(IMetaExpression expr);

    /// <summary>
    /// Try to simplify the given expression
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    IMetaExpression Simplify(IMetaExpression expr);

    /// <summary>
    /// Try to simplify the given expression text
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exprText"></param>
    /// <returns></returns>
    IMetaExpression Simplify(MetaContext context, string exprText);

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

    /// <summary>
    /// Create a copy of this evaluator with a new context
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    IMetaExpressionEvaluator GetEvaluatorCopy(MetaContext context);
}

public interface IMetaExpressionEvaluator<T> :
    IMetaExpressionEvaluator where T : class
{
    ISymbolicFromMetaExpressionConverter<T> FromSymbolicExpressionConverter { get; }

    ISymbolicIntoMetaExpressionConverter<T> IntoSymbolicExpressionConverter { get; }
}