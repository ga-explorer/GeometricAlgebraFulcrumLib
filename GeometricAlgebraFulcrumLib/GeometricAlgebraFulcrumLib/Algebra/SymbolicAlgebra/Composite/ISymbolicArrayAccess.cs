using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Composite
{
    public interface ISymbolicArrayAccess : 
        ISymbolicExpressionComposite
    {
        SymbolicHeadSpecsArrayAccess ArrayAccessHeadSpecs { get; }
    }
}