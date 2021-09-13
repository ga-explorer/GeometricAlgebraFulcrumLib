using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Variables
{
    public interface ISymbolicVariable : 
        ISymbolicExpressionAtomic
    {
        SymbolicHeadSpecsVariable VariableHeadSpecs { get; }
        
        void SetRhsExpressionValue(double number);
    }
}