namespace GeometricAlgebraLib.SymbolicExpressions.HeadSpecs
{
    public interface ISymbolicHeadSpecsVariable :
        ISymbolicHeadSpecsAtomic
    {
        string VariableName { get; }
    }
}