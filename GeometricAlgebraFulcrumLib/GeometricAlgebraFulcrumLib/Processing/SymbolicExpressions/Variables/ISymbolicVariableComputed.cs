using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables
{
    public interface ISymbolicVariableComputed :
        ISymbolicVariable, ISymbolicExpressionComputed
    {
        IEnumerable<ISymbolicExpressionAtomic> RhsAtomicExpressions { get; }

        IEnumerable<ISymbolicExpressionAtomic> RhsNumbersAndParameters { get; }

        IEnumerable<ISymbolicNumber> RhsNumbers { get; }

        IEnumerable<ISymbolicVariable> RhsVariables { get; }

        HashSet<ISymbolicExpressionAtomic> RhsAtomicsCache { get; }

        IEnumerable<ISymbolicVariableParameter> RhsParameterVariables { get; }

        IEnumerable<ISymbolicVariableComputed> RhsComputedVariables { get; }

        IEnumerable<ISymbolicVariableComputed> RhsIntermediateVariables { get; }

        IEnumerable<ISymbolicVariableComputed> RhsOutputVariables { get; }

        bool IsReused { get; }

        int NameIndex { get; }

        int ComputationOrder { get; }

        bool IsFactoredSubExpression { get; }

        int SubExpressionUseCount { get; set; }

        IEnumerable<ISymbolicVariable> GetUsedVariables();

        IEnumerable<ISymbolicVariableParameter> GetUsedParameterVariables();

        IEnumerable<ISymbolicVariableComputed> GetUsedIntermediateVariables();

        void SetReuseInfo(bool isReused, int nameIndex);

        void SetComputationOrder(int computationOrder);

        int UpdateMaxComputationLevel();

        bool ReplaceRhsExpression(ISymbolicExpression oldRhsExpression, string newVariableName);

        bool ReplaceRhsVariable(string oldVariableName, string newVariableName);

        void SimplifyRhsExpression();

        void ResetRhsExpression(ISymbolicExpression newRhsExpression);
    }
}