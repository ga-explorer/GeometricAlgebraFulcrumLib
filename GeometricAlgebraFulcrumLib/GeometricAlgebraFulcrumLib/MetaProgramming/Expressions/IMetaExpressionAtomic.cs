using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

public interface IMetaExpressionAtomic : 
    IMetaExpression
{
    IMetaExpressionHeadSpecsAtomic AtomicHeadSpecs { get; }

    int AtomicExpressionId { get; }

    string InternalName { get; }

    string ExternalName { get; set; }

    IMetaExpression RhsExpression { get; }

    string RhsExpressionText { get; }

    double RhsExpressionValue { get; }

    /// <summary>
    /// True if this is an atomic expression (number or variable) used in a following computation
    /// </summary>
    bool HasDependingVariables { get; }

    /// <summary>
    /// A set of computed variables depending on this variable in their computations
    /// The dependance may be direct or indirect (i.e. through other intermediate variables)
    /// </summary>
    IEnumerable<IMetaExpressionVariableComputed> DependingVariables { get; }

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
}