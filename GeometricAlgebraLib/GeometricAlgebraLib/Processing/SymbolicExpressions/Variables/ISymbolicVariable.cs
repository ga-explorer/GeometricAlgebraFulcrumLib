using GeometricAlgebraLib.Processing.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Variables
{
    public interface ISymbolicVariable : 
        ISymbolicExpressionAtomic
    {
        SymbolicHeadSpecsVariable VariableHeadSpecs { get; }
        
        void SetRhsExpressionValue(double number);
    }
}