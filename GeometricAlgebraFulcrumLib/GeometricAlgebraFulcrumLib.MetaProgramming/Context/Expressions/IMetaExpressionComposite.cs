using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

public interface IMetaExpressionComposite :
    IMetaExpressionComputed,
    IReadOnlyList<IMetaExpression>
{
    IMetaExpressionHeadSpecsComposite CompositeHeadSpecs { get; }

    /// <summary>
    /// True if this expression is aa operator without any arguments
    /// </summary>
    bool IsEmptyComposite { get; }

    /// <summary>
    /// True if this expression has a single argument
    /// </summary>
    bool IsUnaryComposite { get; }

    /// <summary>
    /// True if this expression has two arguments
    /// </summary>
    bool IsBinaryComposite { get; }

    /// <summary>
    /// True if this expression has 3 arguments
    /// </summary>
    bool IsTernaryComposite { get; }

    /// <summary>
    /// True for atomic expressions and empty composite expressions
    /// </summary>
    bool HasNoArguments { get; }

    /// <summary>
    /// True for non-empty composite expressions
    /// </summary>
    bool HasArguments { get; }

    /// <summary>
    /// The arguments of this expression, if any
    /// </summary>
    IEnumerable<IMetaExpression> Arguments { get; }

    /// <summary>
    /// The atomic arguments of this expression, if any
    /// </summary>
    IEnumerable<IMetaExpressionAtomic> AtomicArguments { get; }

    /// <summary>
    /// The composite arguments of this expression, if any
    /// </summary>
    IEnumerable<IMetaExpressionComposite> CompositeArguments { get; }

    /// <summary>
    /// The number arguments of this expression, if any
    /// </summary>
    IEnumerable<IMetaExpressionNumber> NumberArguments { get; }

    /// <summary>
    /// The variable arguments of this expression, if any
    /// </summary>
    IEnumerable<IMetaExpressionVariable> VariableArguments { get; }

    /// <summary>
    /// The variable parameter arguments of this expression, if any
    /// </summary>
    IEnumerable<IMetaExpressionVariableParameter> VariableParameterArguments { get; }

    /// <summary>
    /// The variable computed arguments of this expression, if any
    /// </summary>
    IEnumerable<IMetaExpressionVariableComputed> VariableComputedArguments { get; }

    /// <summary>
    /// The first argument of this expression
    /// </summary>
    IMetaExpression FirstArgument { get; }

    /// <summary>
    /// The second argument of this expression
    /// </summary>
    IMetaExpression SecondArgument { get; }

    /// <summary>
    /// The third argument of this expression
    /// </summary>
    IMetaExpression ThirdArgument { get; }

    /// <summary>
    /// The last argument of this expression
    /// </summary>
    IMetaExpression LastArgument { get; }

    IMetaExpressionComposite GetExpressionCopy(IEnumerable<IMetaExpression> argumentsList);
}