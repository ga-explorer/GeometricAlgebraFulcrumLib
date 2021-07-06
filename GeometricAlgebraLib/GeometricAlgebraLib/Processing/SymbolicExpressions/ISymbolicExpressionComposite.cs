using System.Collections.Generic;
using GeometricAlgebraLib.Processing.SymbolicExpressions.HeadSpecs;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Numbers;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Variables;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions
{
    public interface ISymbolicExpressionComposite :
        ISymbolicExpressionComputed, IReadOnlyList<ISymbolicExpression>
    {
        ISymbolicHeadSpecsComposite CompositeHeadSpecs { get; }

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
        IEnumerable<ISymbolicExpression> Arguments { get; }

        /// <summary>
        /// The atomic arguments of this expression, if any
        /// </summary>
        IEnumerable<ISymbolicExpressionAtomic> AtomicArguments { get; }

        /// <summary>
        /// The composite arguments of this expression, if any
        /// </summary>
        IEnumerable<ISymbolicExpressionComposite> CompositeArguments { get; }

        /// <summary>
        /// The number arguments of this expression, if any
        /// </summary>
        IEnumerable<ISymbolicNumber> NumberArguments { get; }

        /// <summary>
        /// The variable arguments of this expression, if any
        /// </summary>
        IEnumerable<ISymbolicVariable> VariableArguments { get; }

        /// <summary>
        /// The variable parameter arguments of this expression, if any
        /// </summary>
        IEnumerable<ISymbolicVariableParameter> VariableParameterArguments { get; }

        /// <summary>
        /// The variable computed arguments of this expression, if any
        /// </summary>
        IEnumerable<ISymbolicVariableComputed> VariableComputedArguments { get; }

        /// <summary>
        /// The first argument of this expression
        /// </summary>
        ISymbolicExpression FirstArgument { get; }

        /// <summary>
        /// The second argument of this expression
        /// </summary>
        ISymbolicExpression SecondArgument { get; }

        /// <summary>
        /// The third argument of this expression
        /// </summary>
        ISymbolicExpression ThirdArgument { get; }

        /// <summary>
        /// The last argument of this expression
        /// </summary>
        ISymbolicExpression LastArgument { get; }

        ISymbolicExpressionComposite GetExpressionCopy(IEnumerable<ISymbolicExpression> argumentsList);
    }
}