namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public interface ISymbolicHeadSpecsVariable :
        ISymbolicHeadSpecsAtomic
    {
        string VariableName { get; }
    }
}