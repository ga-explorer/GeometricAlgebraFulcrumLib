using GeometricAlgebraLib.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraLib.SymbolicExpressions.Variables
{
    public interface ISymbolicVariable : 
        ISymbolicExpressionAtomic
    {
        SymbolicHeadSpecsVariable VariableHeadSpecs { get; }
        
        void SetRhsExpressionValue(double number);
    }
}