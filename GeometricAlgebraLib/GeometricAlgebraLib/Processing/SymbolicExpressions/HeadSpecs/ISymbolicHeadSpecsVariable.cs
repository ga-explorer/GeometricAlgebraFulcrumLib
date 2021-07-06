namespace GeometricAlgebraLib.Processing.SymbolicExpressions.HeadSpecs
{
    public interface ISymbolicHeadSpecsVariable :
        ISymbolicHeadSpecsAtomic
    {
        string VariableName { get; }
    }
}