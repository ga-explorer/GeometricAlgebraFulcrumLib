using GeometricAlgebraLib.Processing.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Composite
{
    public interface ISymbolicArrayAccess : 
        ISymbolicExpressionComposite
    {
        SymbolicHeadSpecsArrayAccess ArrayAccessHeadSpecs { get; }
    }
}