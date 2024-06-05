using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

public interface IMetaExpressionAtomic :
    IMetaExpression
{
    IMetaExpressionHeadSpecsAtomic AtomicHeadSpecs { get; }

    int AtomicExpressionId { get; }

    string InternalName { get; }

    string ExternalName { get; }

    IMetaExpression RhsExpression { get; }

    string RhsExpressionText { get; }

    double RhsExpressionValue { get; }

    bool HasDependingVariables { get; }

    /// <summary>
    /// True if this is an atomic expression (number or variable) used in a following computation
    /// </summary>
    bool IsOutputOrHasDependingVariables { get; }

    /// <summary>
    /// A set of computed variables depending on this atomic expression in their computations
    /// The dependence may be direct or indirect (i.e. through other intermediate variables)
    /// </summary>
    IEnumerable<IMetaExpressionVariableComputed> DependingVariables { get; }

    /// <summary>
    /// A set of computed variables directly depending on this atomic expression
    /// in their computations
    /// </summary>
    IEnumerable<IMetaExpressionVariableComputed> DirectDependingVariables { get; }

    /// <summary>
    /// A set of computed intermediate variables directly depending on this atomic
    /// expression in their computations
    /// </summary>
    IEnumerable<IMetaExpressionVariableComputed> DirectDependingIntermediateVariables { get; }

    /// <summary>
    /// The last computed variable using this rhs variable in its computation
    /// </summary>
    IMetaExpressionVariableComputed LastDependingVariable { get; }

    /// <summary>
    /// The computation order of the last computed variable using this rhs variable in its computation
    /// </summary>
    int LastDependingVariableComputationOrder { get; }

    int MaxComputationLevel { get; }

    void AddDependingVariable(IMetaExpressionVariableComputed computedVar);

    void ClearDependencyData();

    IMetaExpression GetScalarValue(bool useRhsScalarValue);

    string GetTextDescription();

    bool UpdateExternalName(string externalName);
}