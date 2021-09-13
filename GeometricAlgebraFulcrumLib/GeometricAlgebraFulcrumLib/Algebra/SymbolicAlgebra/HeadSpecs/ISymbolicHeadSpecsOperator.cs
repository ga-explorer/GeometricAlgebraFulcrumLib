using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Composite;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs
{
    public interface ISymbolicHeadSpecsOperator :
        ISymbolicHeadSpecsFunction
    {
        string SymbolText { get; }

        SymbolicOperatorPosition Position { get; }
    }
}