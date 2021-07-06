using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite
{
    public interface ISymbolicArrayAccess : 
        ISymbolicExpressionComposite
    {
        SymbolicHeadSpecsArrayAccess ArrayAccessHeadSpecs { get; }
    }
}