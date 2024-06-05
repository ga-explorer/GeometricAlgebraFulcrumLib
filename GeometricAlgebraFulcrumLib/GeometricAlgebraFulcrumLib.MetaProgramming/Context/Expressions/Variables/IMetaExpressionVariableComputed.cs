using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

public interface IMetaExpressionVariableComputed :
    IMetaExpressionVariable,
    IMetaExpressionComputed
{
    IEnumerable<IMetaExpressionAtomic> RhsAtomicExpressions { get; }

    IEnumerable<IMetaExpressionAtomic> RhsNumbersAndParameters { get; }

    IEnumerable<IMetaExpressionNumber> RhsNumbers { get; }

    IEnumerable<IMetaExpressionVariable> RhsVariables { get; }

    HashSet<IMetaExpressionAtomic> RhsAtomicsCache { get; }

    IEnumerable<IMetaExpressionVariableParameter> RhsParameterVariables { get; }

    IEnumerable<IMetaExpressionVariableComputed> RhsComputedVariables { get; }

    IEnumerable<IMetaExpressionVariableComputed> RhsIntermediateVariables { get; }

    IEnumerable<IMetaExpressionVariableComputed> RhsOutputVariables { get; }

    string OutputExternalName { get; }

    bool IsReused { get; }

    int NameIndex { get; }

    int ComputationOrder { get; }

    bool IsFactoredSubExpression { get; }

    bool IsAffineCombination { get; }

    int SubExpressionUseCount { get; set; }

    IEnumerable<IMetaExpressionVariable> GetUsedVariables();

    IEnumerable<IMetaExpressionVariableParameter> GetUsedParameterVariables();

    IEnumerable<IMetaExpressionVariableComputed> GetUsedIntermediateVariables();

    void SetReuseInfo(bool isReused, int nameIndex);

    void SetComputationOrder(int computationOrder);

    int UpdateMaxComputationLevel();

    bool SetAsOutput(string externalName);

    bool ReplaceRhsExpression(IMetaExpression oldRhsExpression, string newVariableName);

    bool ReplaceRhsVariable(string oldVariableName, string newVariableName);

    bool ReplaceRhsVariable(string oldVariableName, IMetaExpression newExpr);

    void EnhanceRhsExpression();

    void SimplifyRhsExpression();

    void SetRhsExpression(IMetaExpression newRhsExpression);

    void SetStateFrom(IMetaExpressionVariableComputed variable);
}