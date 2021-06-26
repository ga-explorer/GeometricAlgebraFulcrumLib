using GeometricAlgebraLib.SymbolicExpressions.Composite;

namespace GeometricAlgebraLib.SymbolicExpressions.HeadSpecs
{
    public interface ISymbolicHeadSpecsOperator :
        ISymbolicHeadSpecsFunction
    {
        string SymbolText { get; }

        SymbolicOperatorPosition Position { get; }
    }
}