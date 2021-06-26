using GeometricAlgebraLib.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraLib.SymbolicExpressions.Composite
{
    public interface ISymbolicArrayAccess : 
        ISymbolicExpressionComposite
    {
        SymbolicHeadSpecsArrayAccess ArrayAccessHeadSpecs { get; }
    }
}