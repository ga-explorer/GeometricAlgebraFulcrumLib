using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables
{
    public interface ISymbolicVariable : 
        ISymbolicExpressionAtomic
    {
        SymbolicHeadSpecsVariable VariableHeadSpecs { get; }
        
        void SetRhsExpressionValue(double number);
    }
}