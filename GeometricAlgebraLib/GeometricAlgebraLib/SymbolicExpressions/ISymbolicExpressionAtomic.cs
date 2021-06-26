using System.Collections.Generic;
using GeometricAlgebraLib.SymbolicExpressions.HeadSpecs;
using GeometricAlgebraLib.SymbolicExpressions.Variables;

namespace GeometricAlgebraLib.SymbolicExpressions
{
    public interface ISymbolicExpressionAtomic : 
        ISymbolicExpression
    {
        ISymbolicHeadSpecsAtomic AtomicHeadSpecs { get; }

        int AtomicExpressionId { get; }

        string InternalName { get; }

        string ExternalName { get; set; }

        ISymbolicExpression RhsExpression { get; }

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
        IEnumerable<ISymbolicVariableComputed> DependingVariables { get; }

        /// <summary>
        /// The last computed variable using this rhs variable in its computation
        /// </summary>
        ISymbolicVariableComputed LastDependingVariable { get; }

        /// <summary>
        /// The computation order of the last computed variable using this rhs variable in its computation
        /// </summary>
        int LastDependingVariableComputationOrder { get; }

        int MaxComputationLevel { get; }
        
        void AddDependingVariable(ISymbolicVariableComputed computedVar);

        void ClearDependencyData();
 
        ISymbolicExpression GetScalarValue(bool useRhsScalarValue);

        string GetTextDescription();
    }
}